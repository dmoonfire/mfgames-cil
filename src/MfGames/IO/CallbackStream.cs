// <copyright file="CallbackStream.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Remoting;

namespace MfGames.IO
{
	/// <summary>
	/// Implements a stream wrapper that provides events for various methods.
	/// </summary>
	/// <typeparam name="TStream">
	/// The type of the stream.
	/// </typeparam>
	public class CallbackStream<TStream> : Stream
		where TStream : Stream
	{
		#region Fields

		/// <summary>
		/// Contains the underlying stream.
		/// </summary>
		private TStream underlyingStream;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CallbackStream{TStream}"/> class.
		/// </summary>
		/// <param name="underlyingStream">
		/// The underlying stream.
		/// </param>
		public CallbackStream(TStream underlyingStream)
		{
			Contract.Requires(underlyingStream != null);

			this.underlyingStream = underlyingStream;
		}

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when the stream is closed.
		/// </summary>
		public event EventHandler<CalbackStreamEventArgs<TStream>> Closed;

		#endregion

		#region Public Properties

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports reading.
		/// </summary>
		public override bool CanRead { get { return underlyingStream.CanRead; } }

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports seeking.
		/// </summary>
		public override bool CanSeek { get { return underlyingStream.CanSeek; } }

		/// <summary>
		/// Gets a value that determines whether the current stream can time out.
		/// </summary>
		public override bool CanTimeout
		{
			get { return underlyingStream.CanTimeout; }
		}

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether the current stream supports writing.
		/// </summary>
		public override bool CanWrite { get { return underlyingStream.CanWrite; } }

		/// <summary>
		/// When overridden in a derived class, gets the length in bytes of the stream.
		/// </summary>
		public override long Length { get { return underlyingStream.Length; } }

		/// <summary>
		/// When overridden in a derived class, gets or sets the position within the current stream.
		/// </summary>
		public override long Position
		{
			get { return underlyingStream.Position; }
			set { underlyingStream.Position = value; }
		}

		/// <summary>
		/// Gets or sets a value, in miliseconds, that determines how long the stream will attempt to read before timing out.
		/// </summary>
		public override int ReadTimeout
		{
			get { return underlyingStream.ReadTimeout; }
			set { underlyingStream.ReadTimeout = value; }
		}

		/// <summary>
		/// Gets the underlying stream.
		/// </summary>
		/// <value>
		/// The underlying stream.
		/// </value>
		public TStream UnderlyingStream
		{
			get { return underlyingStream; }
			private set { underlyingStream = value; }
		}

		/// <summary>
		/// Gets or sets a value, in miliseconds, that determines how long the stream will attempt to write before timing out.
		/// </summary>
		public override int WriteTimeout
		{
			get { return underlyingStream.WriteTimeout; }
			set { underlyingStream.WriteTimeout = value; }
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Begins an asynchronous read operation.
		/// </summary>
		/// <param name="buffer">
		/// The buffer to read the data into.
		/// </param>
		/// <param name="offset">
		/// The byte offset in <paramref name="buffer"/> at which to begin writing data read from the stream.
		/// </param>
		/// <param name="count">
		/// The maximum number of bytes to read.
		/// </param>
		/// <param name="callback">
		/// An optional asynchronous callback, to be called when the read is complete.
		/// </param>
		/// <param name="state">
		/// A user-provided object that distinguishes this particular asynchronous read request from other requests.
		/// </param>
		/// <returns>
		/// An <see cref="T:System.IAsyncResult"/> that represents the asynchronous read, which could still be pending.
		/// </returns>
		public override IAsyncResult BeginRead(
			byte[] buffer,
			int offset,
			int count,
			AsyncCallback callback,
			object state)
		{
			return underlyingStream.BeginRead(
				buffer,
				offset,
				count,
				callback,
				state);
		}

		/// <summary>
		/// Begins an asynchronous write operation.
		/// </summary>
		/// <param name="buffer">
		/// The buffer to write data from.
		/// </param>
		/// <param name="offset">
		/// The byte offset in <paramref name="buffer"/> from which to begin writing.
		/// </param>
		/// <param name="count">
		/// The maximum number of bytes to write.
		/// </param>
		/// <param name="callback">
		/// An optional asynchronous callback, to be called when the write is complete.
		/// </param>
		/// <param name="state">
		/// A user-provided object that distinguishes this particular asynchronous write request from other requests.
		/// </param>
		/// <returns>
		/// An IAsyncResult that represents the asynchronous write, which could still be pending.
		/// </returns>
		public override IAsyncResult BeginWrite(
			byte[] buffer,
			int offset,
			int count,
			AsyncCallback callback,
			object state)
		{
			return underlyingStream.BeginWrite(
				buffer,
				offset,
				count,
				callback,
				state);
		}

		/// <summary>
		/// Closes the current stream and releases any resources (such as sockets and file handles) associated with the current stream.
		/// </summary>
		public override void Close()
		{
			underlyingStream.Close();

			EventHandler<CalbackStreamEventArgs<TStream>> listeners =
				Closed;

			if (listeners != null)
			{
				listeners(
					this,
					new CalbackStreamEventArgs<TStream>(this));
			}
		}

		/// <summary>
		/// Creates an object that contains all the relevant information required to generate a proxy used to communicate with a remote object.
		/// </summary>
		/// <param name="requestedType">
		/// The <see cref="T:System.Type"/> of the object that the new <see cref="T:System.Runtime.Remoting.ObjRef"/> will reference.
		/// </param>
		/// <returns>
		/// Information required to generate a proxy.
		/// </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, public overrideKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure"/>
		/// </PermissionSet>
		public override ObjRef CreateObjRef(Type requestedType)
		{
			return underlyingStream.CreateObjRef(requestedType);
		}

		/// <summary>
		/// Waits for the pending asynchronous read to complete.
		/// </summary>
		/// <param name="asyncResult">
		/// The reference to the pending asynchronous request to finish.
		/// </param>
		/// <returns>
		/// The number of bytes read from the stream, between zero (0) and the number of bytes you requested. Streams return zero (0) only at the end of the stream, otherwise, they should block until at least one byte is available.
		/// </returns>
		public override int EndRead(IAsyncResult asyncResult)
		{
			return underlyingStream.EndRead(asyncResult);
		}

		/// <summary>
		/// Ends an asynchronous write operation.
		/// </summary>
		/// <param name="asyncResult">
		/// A reference to the outstanding asynchronous I/O request.
		/// </param>
		public override void EndWrite(IAsyncResult asyncResult)
		{
			underlyingStream.EndWrite(asyncResult);
		}

		/// <summary>
		/// When overridden in a derived class, clears all buffers for this stream and causes any buffered data to be written to the underlying device.
		/// </summary>
		public override void Flush()
		{
			underlyingStream.Flush();
		}

		/// <summary>
		/// Obtains a lifetime service object to control the lifetime policy for this instance.
		/// </summary>
		/// <returns>
		/// An object of type <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> used to control the lifetime policy for this instance. This is the current lifetime service object for this instance if one exists; otherwise, a new lifetime service object initialized to the value of the <see cref="P:System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseManagerPollTime" /> property.
		/// </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, public overrideKeyToken=b77a5c561934e089" version="1" Flags="RemotingConfiguration, Infrastructure" />
		/// </PermissionSet>
		public override object InitializeLifetimeService()
		{
			return underlyingStream.InitializeLifetimeService();
		}

		/// <summary>
		/// When overridden in a derived class, reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
		/// </summary>
		/// <param name="buffer">
		/// An array of bytes. When this method returns, the buffer contains the specified byte array with the values between <paramref name="offset"/> and (<paramref name="offset"/> + <paramref name="count"/> - 1) replaced by the bytes read from the current source.
		/// </param>
		/// <param name="offset">
		/// The zero-based byte offset in <paramref name="buffer"/> at which to begin storing the data read from the current stream.
		/// </param>
		/// <param name="count">
		/// The maximum number of bytes to be read from the current stream.
		/// </param>
		/// <returns>
		/// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
		/// </returns>
		public override int Read(
			byte[] buffer,
			int offset,
			int count)
		{
			return underlyingStream.Read(
				buffer,
				offset,
				count);
		}

		/// <summary>
		/// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
		/// </summary>
		/// <returns>
		/// The unsigned byte cast to an Int32, or -1 if at the end of the stream.
		/// </returns>
		public override int ReadByte()
		{
			return underlyingStream.ReadByte();
		}

		/// <summary>
		/// When overridden in a derived class, sets the position within the current stream.
		/// </summary>
		/// <param name="offset">
		/// A byte offset relative to the <paramref name="origin"/> parameter.
		/// </param>
		/// <param name="origin">
		/// A value of type <see cref="T:System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.
		/// </param>
		/// <returns>
		/// The new position within the current stream.
		/// </returns>
		public override long Seek(
			long offset,
			SeekOrigin origin)
		{
			return underlyingStream.Seek(
				offset,
				origin);
		}

		/// <summary>
		/// When overridden in a derived class, sets the length of the current stream.
		/// </summary>
		/// <param name="value">
		/// The desired length of the current stream in bytes.
		/// </param>
		public override void SetLength(long value)
		{
			underlyingStream.SetLength(value);
		}

		/// <summary>
		/// When overridden in a derived class, writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
		/// </summary>
		/// <param name="buffer">
		/// An array of bytes. This method copies <paramref name="count"/> bytes from <paramref name="buffer"/> to the current stream.
		/// </param>
		/// <param name="offset">
		/// The zero-based byte offset in <paramref name="buffer"/> at which to begin copying bytes to the current stream.
		/// </param>
		/// <param name="count">
		/// The number of bytes to be written to the current stream.
		/// </param>
		public override void Write(
			byte[] buffer,
			int offset,
			int count)
		{
			underlyingStream.Write(
				buffer,
				offset,
				count);
		}

		/// <summary>
		/// Writes a byte to the current position in the stream and advances the position within the stream by one byte.
		/// </summary>
		/// <param name="value">
		/// The byte to write to the stream.
		/// </param>
		public override void WriteByte(byte value)
		{
			underlyingStream.WriteByte(value);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.IO.Stream"/> and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">
		/// true to release both managed and unmanaged resources; false to release only unmanaged resources.
		/// </param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion
	}
}
