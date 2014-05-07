using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.ViewModel
{
	class CompanyViewModel : ViewModel
	{
		public CompanyViewModel(int id, string uid, string name)
		{
			ID = id;
			UID = uid;
			Name = name;
		}

		private string _uid = null;
		public string UID
		{
			get { return _uid; }
			set
			{
				if (_uid != value) {
					_uid = value;
					OnPropertyChanged("UID");
				}
			}
		}

		private string _name = null;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value) {
					_name = value;
					OnPropertyChanged("Name");
				}
			}
		}

		private int _id = -1;
		public int ID
		{
			get { return _id; }
			private set
			{
				if (_id != value) {
					_id = value;
					OnPropertyChanged("ID");
				}
			}
		}

		public override string ToString()
		{
			string uid = "";
			string name = "";

			if (_uid != null) {
				uid = _uid;
			}

			if (_name != null) {
				name = _name;
			}

			return (uid + " <> " + name);
		}
	}
}
