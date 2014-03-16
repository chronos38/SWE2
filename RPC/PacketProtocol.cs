using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
	class PacketProtocol
	{
		byte[] _lengthBuffer;
		byte[] _dataBuffer;
		int _bytesReceived;
		int _maxMessageSize;

		Queue _messageQueue = new Queue();

		/// <summary>
		/// Complete messages processed by DataReceived end up here.
		/// </summary>
		public Queue Messages
		{
			get { return _messageQueue; }
		}
		/// <summary>
		/// Initialize a new PacketProtocol.
		/// </summary>
		/// <param name="maxMessageSize">Limits messages to the given size.</param>
		public PacketProtocol(int maxMessageSize)
		{
			_lengthBuffer = new byte[sizeof(int)];
			_maxMessageSize = maxMessageSize;
		}

		/// <summary>
		/// Prefix a message with its length.
		/// </summary>
		/// <param name="message">The message to prefix</param>
		/// <returns>The message with its prefixed Length</returns>
		public static byte[] WrapMessage(byte[] message)
		{
			byte[] lengthPrefix = BitConverter.GetBytes(message.Length);
			byte[] ret = new byte[message.Length + lengthPrefix.Length];
			lengthPrefix.CopyTo(ret, 0);
			message.CopyTo(ret, lengthPrefix.Length);

			return ret;
		}

		/// <summary>
		/// Data that was previously prefixed with WrapMessage can be unwrapped using this function.
		/// </summary>
		/// <param name="data">Data received from a NetworkStream. Not null</param>
		public void DataReceived(byte[] data)
		{
			int i = 0;
			while(data.Length != i)
			{
				int bytesAvailable = data.Length - i;
				if(_dataBuffer != null)
				{
					// Data
					int bytesRequested = _dataBuffer.Length - _bytesReceived;
					int bytesTransferred = Math.Min(bytesRequested, bytesAvailable);
					Array.Copy(data, i, _dataBuffer, _bytesReceived, bytesTransferred);
					i += bytesTransferred;
					ReadCompleted(bytesTransferred);
				}
				else
				{
					// Length Prefix
					int bytesRequested = _lengthBuffer.Length - _bytesReceived;
					int bytesTransferred = Math.Min(bytesRequested, bytesAvailable);
					Array.Copy(data, i, _lengthBuffer, _bytesReceived, bytesTransferred);
					i += bytesTransferred;
					ReadCompleted(bytesTransferred);
				}
			}
		}

		/// <summary>
		/// Called by DataReceived when a read completes.
		/// </summary>
		/// <param name="count">The number of read bytes.</param>
		private void ReadCompleted(int count)
		{
			_bytesReceived += count;
			if(_dataBuffer == null)
			{
				// Handel message length.
				if(_bytesReceived != sizeof(int))
				{
					//Not all data has arrived yet.
				}
				else
				{
					int length = BitConverter.ToInt32(_lengthBuffer, 0);
					if(length < 0)
					{
						throw new System.Net.ProtocolViolationException("Message length is smaller than zero");
					}

					if((_maxMessageSize > 0) && (length > _maxMessageSize))
					{
						throw new System.Net.ProtocolViolationException("Message is to big");
					}

					if(length == 0)
					{
						// zero length could be used as keepalive
						_bytesReceived = 0;
					}
					else
					{
						_dataBuffer = new byte[length];
						_bytesReceived = 0;
					}
				}
			}
			else
			{
				if(_bytesReceived != _dataBuffer.Length)
				{
					// Not all data has arrived yet.
				}
				else
				{
					_messageQueue.Enqueue(_dataBuffer);

					//reset and start reading message length
					_dataBuffer = null;
					_bytesReceived = 0;
				}

			}
		}
	}
}

