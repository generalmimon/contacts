using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontakty
{
	public class Contact : INotifyPropertyChanged
	{
		public Contact(string firstname, string surname, string tel, string email)
		{
			this.Firstname = firstname;
			this.Surname = surname;
			this.Tel = tel;
			this.Email = email;
		}
		private string firstname;
		private string surname;
		private string tel;
		private string email;
		public string Firstname {
			get { return firstname; }
			set
			{
				if(firstname != value)
				{
					firstname = value;
					NotifyPropertyChanged("Firstname");
					NotifyPropertyChanged("Fullname");
				}
			}
		}
		public string Surname
		{
			get { return surname; }
			set
			{
				if (surname != value)
				{
					surname = value;
					NotifyPropertyChanged("Surname");
					NotifyPropertyChanged("Fullname");
				}
			}
		}
		public string Fullname
		{
			get {
				List<string> names = new List<string>();
				if (Firstname != "") names.Add(Firstname);
				if (Surname != "") names.Add(Surname);
				return String.Join(" ", names);
			}
		}
		public string Tel
		{
			get { return tel; }
			set
			{
				if (tel != value)
				{
					tel = value;
					NotifyPropertyChanged("Tel");
				}
			}
		}
		public string Email
		{
			get { return email; }
			set
			{
				if (email != value)
				{
					email = value;
					NotifyPropertyChanged("Email");
				}
			}
		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyPropertyChanged(string propName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
			}
		}
	}
}
