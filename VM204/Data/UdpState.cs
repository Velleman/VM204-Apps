using System;
using System.Net;
using System.Net.Sockets;

namespace VM204
{
	public class UdpState
	{
		public IPEndPoint E { get; set;}

		public UdpClient C { get; set;}

		public UdpState ()
		{
		}
	}
}

