using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Timers;
using System.Diagnostics;
using Xamarin;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VM204
{
	public class DiscoveryScanner : INotifyPropertyChanged
	{
		public event EventHandler<DiscoveryFoundEventArgs> DiscoveryFound;

		private bool _isScanning;

		public bool isScanning {
			get { 
				return _isScanning;
			}
			set {
				if (value != _isScanning) {
					_isScanning = value;
					OnPropertyChanged ();
				}
			}
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		public DiscoveryScanner ()
		{
			isScanning = false;
		}

		public void Scan ()
		{
			Task.Run (async () => {
				this.isScanning = true;
				try {
					using (var udpClient = new UdpClient (30303)) {

						string loggingEvent = "";
						var bytes = System.Text.Encoding.UTF8.GetBytes ("VM204,Knock Knock\r\n");
						udpClient.EnableBroadcast = true;
						var broadcastEndPoint = new IPEndPoint (IPAddress.Broadcast, 30303);
						udpClient.Send (bytes, bytes.Length, broadcastEndPoint);
						Stopwatch stopwatch = new Stopwatch ();
						stopwatch.Start ();
						//Creates an IPEndPoint to record the IP Address and port number of the sender.  
						// The IPEndPoint will allow you to read datagrams sent from any source.
						IPEndPoint RemoteIpEndPoint = new IPEndPoint (IPAddress.Any, 0);
						while (stopwatch.ElapsedMilliseconds <= 3000) {
							if (udpClient.Available > 0) {
								//IPEndPoint object will allow us to read datagrams sent from any source.
								var receivedResults = udpClient.Receive (ref RemoteIpEndPoint);
								var result = Encoding.UTF8.GetString (receivedResults);
								var discovery = CreateDiscovery (result, RemoteIpEndPoint);
								if (discovery != null) {
									Device.BeginInvokeOnMainThread(()=>{
									OnDiscoveryFound (new DiscoveryFoundEventArgs (discovery));
									});
								}
								
							}
						}
						stopwatch.Stop ();
						udpClient.Close();
					}
				} catch (Exception e) {
					Insights.Report (e);

				}
				this.isScanning = false;
			});
		}

		protected virtual void OnDiscoveryFound (DiscoveryFoundEventArgs e)
		{
			if (DiscoveryFound != null)
				DiscoveryFound (this, e);
		}

		private Discovery CreateDiscovery (string s, IPEndPoint endPoint)
		{
			Discovery discovery = new Discovery ();
			StringReader sr = new StringReader (s);
			var yes = sr.ReadLine ();
			if (yes == "Yes?") {
				discovery.IP = endPoint.Address.ToString ();
				discovery.WebPort = Convert.ToInt32 (sr.ReadLine ());
				discovery.Name = sr.ReadLine ();
				discovery.MacAddress = sr.ReadLine ();
				discovery.Version = sr.ReadLine ();
				return discovery;
			} else
				return null;
		}

		void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}


	}
}

