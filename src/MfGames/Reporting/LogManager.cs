#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Threading;

using MfGames.Locking;

#endregion

namespace MfGames.Reporting
{
    /// <summary>
    /// Implements a low-profile logging sender for the MfGames and related libraries. To
    /// connect to the logging interface, the code just needs to attach to the Log event.
    /// </summary>
    public static class LogManager
    {
        #region Constructors

        /// <summary>
        /// Initializes the <see cref="LogManager"/> class.
        /// </summary>
        static LogManager()
        {
            threadLock =
                new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
        }

        #endregion

        #region Eventing

        private static readonly ReaderWriterLockSlim threadLock;

        /// <summary>
        /// Occurs when a message is logged. The sender can be <see langword="null"/>
        /// with events, if the logging interface does not give a context.
        /// </summary>
        public static event EventHandler<SeverityMessageEventArgs> Logged
        {
            add
            {
                using (new WriteLock(threadLock))
                {
                    logged += value;
                }
            }

            remove
            {
                using (new WriteLock(threadLock))
                {
                    logged -= value;
                }
            }
        }

        private static event EventHandler<SeverityMessageEventArgs> logged;

        #endregion

        #region Logging

        /// <summary>
        /// Logs the message to any class listening to the manager.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Log(SeverityMessage message)
        {
            Log(null, message);
        }

        /// <summary>
        /// Logs the specified sender.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="message">The message.</param>
        public static void Log(
            object sender,
            SeverityMessage message)
        {
            // Check the listeners outside of a lock. If we have anything, then
            // return as fast as we can.
            if (logged == null)
            {
                return;
            }

            // Get the listeners at the point of reading the log.
            EventHandler<SeverityMessageEventArgs> listeners;

            using (new ReadLock(threadLock))
            {
                listeners = logged;
            }

            // If we don't have listeners, then just break out.
            if (listeners == null)
            {
                return;
            }

            // Create an event handler and invoke it.
            var args = new SeverityMessageEventArgs(message);
            listeners(sender, args);
        }

        #endregion
    }
}