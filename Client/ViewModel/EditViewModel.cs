using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{
	internal class EditViewModel : ViewModel
	{
		public int ID { get; private set; }

		public EditViewModel(Window window)
		{
			ID = -1;
			UID = null;
			Name = null;
			Title = null;
			Forename = null;
			Surname = null;
			Suffix = null;
			Birthday = null;
			Company = null;
			Street = null;
			StreetNumber = null;
			ZIP = null;
			City = null;

			CreateCommands(window);
		}

		public EditViewModel(Window window, Contact contact)
		{
			EditWindow edit = window as EditWindow;

			ID = contact.ID;
			UID = contact.UID;
			Name = contact.Name;
			Title = contact.Title;
			Forename = contact.Forename;
			Surname = contact.Surname;
			Suffix = contact.Suffix;
			Birthday = contact.BirthDate;
			//Company = contact.Company;
			Street = contact.Street;
			StreetNumber = contact.StreetNumber;
			ZIP = contact.PostalCode;
			City = contact.City;

			CreateCommands(window);
			Search.Execute(contact.Company);
		}

		#region Editable
		public bool? IsCompany
		{
			get
			{
				if (!(string.IsNullOrEmpty(UID) && string.IsNullOrEmpty(Name))) {
					return true;
				} else if (!(
					string.IsNullOrEmpty(Title) && 
					string.IsNullOrEmpty(Forename) && 
					string.IsNullOrEmpty(Surname) && 
					string.IsNullOrEmpty(Suffix) &&
					Birthday == null && Checked == false)) {
					
					return false;
				}

				return null;
			}
		}

		public bool CanEditPerson
		{
			get
			{
				return (IsCompany == null || IsCompany == false);
			}
		}

		public bool CanEditCompany
		{
			get
			{
				return (IsCompany == null || IsCompany == true);
			}
		}
		#endregion

		#region Company
		private string _uid = null;
		public string UID
		{
			get { return _uid; }
			set
			{
				if (_uid != value) {
					_uid = value;
					OnPropertyChanged("UID");
					NotifyStateChanged();
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
					NotifyStateChanged();
				}
			}
		}
		#endregion

		#region Person
		private string _title = null;
		public string Title
		{
			get { return _title; }
			set 
			{
				if (_title != value) {
					_title = value;
					OnPropertyChanged("Title");
					NotifyStateChanged();
				}
			}
		}

		private string _forename = null;
		public string Forename
		{
			get { return _forename; }
			set 
			{
				if (_forename != value) {
					_forename = value;
					OnPropertyChanged("Forename");
					NotifyStateChanged();
				}
			}
		}

		private string _surname = null;
		public string Surname
		{
			get { return _surname; }
			set 
			{
				if (_surname != value) {
					_surname = value;
					OnPropertyChanged("Surname");
					NotifyStateChanged();
				}
			}
		}

		private string _suffix = null;
		public string Suffix
		{
			get { return _suffix; }
			set 
			{
				if (_suffix != value) {
					_suffix = value;
					OnPropertyChanged("Suffix");
					NotifyStateChanged();
				}
			}
		}

		private DateTime? _birthday = null;
		public DateTime? Birthday
		{
			get { return _birthday; }
			set 
			{
				if (_birthday != value) {
					_birthday = value;
					OnPropertyChanged("Birthday");
					NotifyStateChanged();
				}
			}
		}

		private bool _checked = false;
		public bool Checked
		{
			get { return _checked; }
			set
			{
				if (_checked != value) {
					_checked = value;
					OnPropertyChanged("Checked");

					if (!_checked) {
						Delete.Execute(null);
					}
				}
			}
		}

		private CompanyViewModel _company = null;
		public CompanyViewModel Company
		{
			get { return _company; }
			set
			{
				if (_company != value) {
					_company = value;
					OnPropertyChanged("Company");
					NotifyStateChanged();
				}
			}
		}
		#endregion

		#region Address
		private string _street = null;
		public string Street
		{
			get { return _street; }
			set
			{
				if (_street != value) {
					_street = value;
					OnPropertyChanged("Street");
					NotifyStateChanged();
				}
			}
		}

		private string _streetNumber = null;
		public string StreetNumber
		{
			get { return _streetNumber; }
			set
			{
				if (_streetNumber != value) {
					_streetNumber = value;
					OnPropertyChanged("StreetNumber");
					NotifyStateChanged();
				}
			}
		}

		private string _postalcode = null;
		public string ZIP
		{
			get { return _postalcode; }
			set
			{
				if (_postalcode != value) {
					_postalcode = value;
					OnPropertyChanged("ZIP");
					NotifyStateChanged();
				}
			}
		}

		private string _city = null;
		public string City
		{
			get { return _city; }
			set
			{
				if (_city != value) {
					_city = value;
					OnPropertyChanged("City");
					NotifyStateChanged();
				}
			}
		}
		#endregion

		public ICommand Cancel { get; private set; }
		public ICommand Save { get; private set; }
		public ICommand Search { get; private set; }
		public ICommand Delete { get; private set; }

		private void CreateCommands(Window window)
		{
			Cancel = new Command.ContactCancelCommand(window, this);
			Save = new Command.ContactSaveCommand(window, this);
			Search = new Command.PersonSearchCommand(window, this);
			Delete = new Command.PersonDeleteCommand(window, this);
		}

		private void NotifyStateChanged()
		{
			OnPropertyChanged("IsCompany");
			OnPropertyChanged("CanEditCompany");
			OnPropertyChanged("CanEditPerson");
		}
	}
}
