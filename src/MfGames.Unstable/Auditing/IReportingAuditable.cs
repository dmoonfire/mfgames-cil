#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
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

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Defines the interface of an IAuditable object that also have events exposed
	/// for reporting changes.
	/// </summary>
	public interface IReportingAuditable : IAuditable
	{
		/// <summary>
		/// This event is triggered when a message is changed in the
		/// system. The given severity will be Severity.None if it is
		/// removed.
		/// </summary>
		event EventHandler<AuditMessageEventArgs> AuditMessageChanged;

		/// <summary>
		/// This event is triggered when the severity changes. This
		/// will not be called if the message changes, but the
		/// severity does not.
		/// </summary>
		event EventHandler<SeverityEventArgs> AuditSeverityChanged;
	}
}