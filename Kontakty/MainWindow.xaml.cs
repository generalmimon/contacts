using Microsoft.Win32;
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
using System.Windows.Navigation;
using System.IO;

namespace Kontakty
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ContactManager contactManager;
		public MainWindow()
		{
			InitializeComponent();
			contactManager = new ContactManager("Kontakty", "kontakty.csv");

			contactsListBox.ItemsSource = contactManager.Contacts;
		}
		private void addContactBut_Click(object sender, RoutedEventArgs e)
		{
			addContact addContactWindow = new addContact(contactManager);
			addContactWindow.Owner = this;
			addContactWindow.ShowDialog();
		}
		
		private void contactsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (contactsListBox.SelectedItem != null)
			{
				editContactBut.IsEnabled = removeContactBut.IsEnabled = true;
			} else
			{
				editContactBut.IsEnabled = removeContactBut.IsEnabled = false;
			}
		}
		private void editContactBut_Click(object sender, RoutedEventArgs e)
		{
			if (contactsListBox.SelectedItem != null)
			{
				Contact contact = contactsListBox.SelectedItem as Contact;
				addContact addContactWindow = new addContact(contactManager, contact);
				addContactWindow.Owner = this;
				addContactWindow.ShowDialog();
			}
		}
		private void removeContactBut_Click(object sender, RoutedEventArgs e)
		{
			if(contactsListBox.SelectedItem != null)
			{
				Contact contact = contactsListBox.SelectedItem as Contact;
				contactManager.Remove(contact);
			}
		}

		private void importItem_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Soubor kontaktů (*.csv)|*.csv";
			openFileDialog.FileName = "kontakty.csv";
			if (openFileDialog.ShowDialog() == true)
			{
				string path = openFileDialog.FileName;
				MessageBoxResult overwriteDialog = MessageBox.Show(
					"Importovali jste soubor kontaktů " + Path.GetFileName(path) + ".\n" +
					"Chcete všechny kontakty nahradit nově importovanými (Ano), nebo chcete nové kontakty přidat (Ne)?",
					"Kontakty",
					MessageBoxButton.YesNoCancel,
					MessageBoxImage.Question
				);
				switch(overwriteDialog)
				{
					case MessageBoxResult.Yes:
						contactManager.ImportFile(path, true);
						break;
					case MessageBoxResult.No:
						contactManager.ImportFile(path, false);
						break;
				}
			}
		}
		private void exportItem_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Soubor kontaktů (*.csv)|*.csv";
			saveFileDialog.FileName = "kontakty.csv";
			if (saveFileDialog.ShowDialog() == true)
			{
				string path = saveFileDialog.FileName;
				contactManager.ExportFile(path);
			}

		}

		private void removeAllItem_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult confirmDialog = MessageBox.Show("Opravdu chcete všechny kontakty smazat? Tento krok je nevratný!", "Kontakty", MessageBoxButton.YesNo);
			if(confirmDialog == MessageBoxResult.Yes)
				contactManager.RemoveAll();
		}
	}
}
