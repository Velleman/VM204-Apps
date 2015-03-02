using System;
using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VM204
{
	public class Discovery : INotifyPropertyChanged
	{
		string _IP;

		public string IP {
			get{ return _IP; }
			set {
				if (value.Equals (_IP, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_IP = value;
				OnPropertyChanged ();
			}
		}

		int _WebPort;

		public int WebPort {
			get{ return _WebPort; }
			set {
				if (value.Equals (_WebPort)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_WebPort = value;
				OnPropertyChanged ();
			}
		}

		string _Name;

		public string Name {
			get{ return _Name; }
			set {
				if (value.Equals (_Name, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_Name = value;
				OnPropertyChanged ();
			}
		}

		string _Mac;

		public string MacAddress {
			get{ return _Mac; }
			set {
				if (value.Equals (_Mac, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_Mac = value;
				OnPropertyChanged ();
			}
		}

		string _Version;

		public string Version {
			get{ return _Version; }
			set {
				if (value.Equals (_Version, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_Version = value;
				OnPropertyChanged ();
			}
		}

		public Discovery ()
		{
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		public RelayCard ConvertToRelayCard ()
		{
			RelayCard card = new RelayCard ();
			card.Name = Name;
			card.MacAddress = MacAddress;
			card.LocalIp = IP;
			card.LocalPort = WebPort.ToString ();
			card.ConnectLocal = true;
			return card;
		}

	}
}

