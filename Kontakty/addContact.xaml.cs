using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kontakty
{
	/// <summary>
	/// Interaction logic for addContact.xaml
	/// </summary>
	public partial class addContact : Window
	{
		private ContactManager contactManager;
		private enum Modes
		{
			Add, Edit
		}
		private Modes mode;
		private Contact editingContact;
		public addContact(ContactManager contactManager)
		{
			InitializeComponent();
			this.contactManager = contactManager;
			mode = Modes.Add;
		}
		public addContact(ContactManager contactManager, Contact editingContact)
		{
			InitializeComponent();
			Title = "Upravit kontakt";
			this.contactManager = contactManager;
			this.editingContact = editingContact;
			firstnameInp.Text = editingContact.Firstname;
			surnameInp.Text = editingContact.Surname;
			telInp.Text = editingContact.Tel;
			emailInp.Text = editingContact.Email;
			mode = Modes.Edit;
		}

		private void confirmBut_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if(mode == Modes.Add)
				{
					contactManager.Add(firstnameInp.Text, surnameInp.Text, telInp.Text.Replace(" ", ""), emailInp.Text);
				}
				else if(mode == Modes.Edit)
				{
					contactManager.Modify(editingContact, firstnameInp.Text, surnameInp.Text, formatTel(telInp.Text), emailInp.Text);
				}
				Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Kontakty", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}
		private string formatTel(string val)
		{
			string tel = val.Replace(" ", "");
			if (tel.Length == 9)
			{
				tel = tel.Substring(0, 3) + " " + tel.Substring(3, 3) + " " + tel.Substring(6, 3);
			}
			else if (tel.Length > 9)
			{
				if (tel.StartsWith("+") || tel.StartsWith("00"))
				{
					int prefixLen = tel.Length - 9;
					tel = tel.Substring(0, prefixLen) + " " + tel.Substring(prefixLen, 3) + " " + tel.Substring(prefixLen + 3, 3) + " " + tel.Substring(prefixLen + 6, 3);
				}
			}
			return tel;
		}
		private void telInp_LostFocus(object sender, RoutedEventArgs e)
		{
			string tel = formatTel(telInp.Text);
			telInp.Text = tel;
		}
		private void telInp_GotFocus(object sender, RoutedEventArgs e)
		{
			telInp.Text = telInp.Text.Replace(" ", "");
		}
	}
}
