using System;
using SQLite;
using Xamarin.Forms;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace VM204
{
	[Table("RelayCards")]
	public class RelayCard : INotifyPropertyChanged
	{
		public RelayCard ()
		{
			ID = 0;
			_localIp = "";
			_localPort = "";
			_externalIp = "";
			_externalPort = "";
			_connectLocal = true;
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		[PrimaryKey, AutoIncrement]
		public int ID {
			get; 
			set;
		}

		private string _name;

		public string Name {
			get{ return _name; }
			set {
				if (value != _name) {
					_name = value;
					OnPropertyChanged ();
				}
			}
		}

		private string _macAddress;

		public string MacAddress{
			get{ return _macAddress;}
			set {
				if (value != _macAddress) {
					_macAddress = value;
					OnPropertyChanged ();
				}
			}
		}

		private string _localIp;

		public string LocalIp {
			get{ return _localIp; }
			set {
				if (value != _localIp) {
					_localIp = value;
					OnPropertyChanged ();
				}

			}
		}

		private string _localPort;

		public string LocalPort {
			get{ return _localPort; }
			set {
				if (value.Equals (_localPort, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_localPort = value;
				OnPropertyChanged ();
			}
		}

		private string _externalIp;

		public string ExternalIp {
			get{ return _externalIp; }
			set {
				if (value.Equals (_externalIp, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_externalIp = value;
				OnPropertyChanged ();
			}
		}

		private string _externalPort;

		public string ExternalPort {
			get{ return _externalPort; }
			set {
				if (value.Equals (_externalPort, StringComparison.Ordinal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_externalPort = value;
				OnPropertyChanged ();
			}
		}

		private bool _connectLocal;

		public bool ConnectLocal {
			get{ return _connectLocal; }
			set {
				if (value.Equals (_connectLocal)) {
					// Nothing to do - the value hasn't changed;
					return;
				}
				_connectLocal = value;
				OnPropertyChanged ();
			}
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

