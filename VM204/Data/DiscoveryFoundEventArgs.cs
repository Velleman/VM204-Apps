using System;

namespace VM204
{
	public class DiscoveryFoundEventArgs : EventArgs
	{

		public readonly Discovery Card;

		public DiscoveryFoundEventArgs (Discovery _discovery)
		{
			Card = _discovery;
		}
	}
}

