using DataTransfer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
	public partial class EditWindow : Window
	{
		private Contact Contact { get; set; }

		private void IntializeContact()
		{
			if (IsCompany()) {
				InitializeCompany();
			} else if (IsPerson()) {
				InitializePerson();
			} else {
				InitializeEmpty();
			}
		}

		private bool IsCompany()
		{
			return ((Contact.UID != null || Contact.Name != null) &&
				Contact.Title == null &&
				Contact.Forename == null &&
				Contact.Surname == null &&
				Contact.Suffix == null);
		}

		private bool IsPerson()
		{
			return (Contact.Title != null ||
				Contact.Forename != null ||
				Contact.Suffix != null ||
				Contact.Suffix != null);
		}

		private void InitializeCompany()
		{
			this.grpPerson.IsEnabled = false;
			this.txtCompanyUID.Text = Contact.UID;
			this.txtCompanyName.Text = Contact.Name;
		}

		private void InitializePerson()
		{
			this.grpCompany.IsEnabled = false;
			this.txtPersonTitle.Text = Contact.Title;
			this.txtPersonForename.Text = Contact.Forename;
			this.txtPersonSurname.Text = Contact.Surname;
			this.txtPersonSuffix.Text = Contact.Suffix;
		}

		private void InitializeEmpty()
		{
		}
	}
}
