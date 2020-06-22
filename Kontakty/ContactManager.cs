using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontakty
{
	public class ContactManager
	{
		private ContactStorage contactStorage;
		public ObservableCollection<Contact> Contacts { get; set; }
		public void Add(string firstname, string surname, string tel, string email)
		{
			if (firstname == "" && surname == "") throw new ArgumentException("Jméno nesmí být prázdné.");
			Contact newContact = new Contact(firstname, surname, tel, email);
			Contacts.Add(newContact);
			contactStorage.Save(Contacts);
		}
		public void Modify(Contact contact, string firstname, string surname, string tel, string email)
		{
			if (firstname == "" && surname == "") throw new ArgumentException("Jméno nesmí být prázdné.");
			contact.Firstname = firstname;
			contact.Surname = surname;
			contact.Tel = tel;
			contact.Email = email;
			contactStorage.Save(Contacts);
		}
		public void Remove(Contact contact)
		{
			Contacts.Remove(contact);
			contactStorage.Save(Contacts);
		}
		public void RemoveAll()
		{
			for (int i = Contacts.Count - 1; i >= 0; i--)
			{
				Contacts.RemoveAt(i);
			}
			contactStorage.Save(Contacts);
		}
		public void ImportFile(string path, bool overwriteFlag)
		{
			if(overwriteFlag)
				RemoveAll();
			ObservableCollection<Contact> newContacts = contactStorage.Load(path);
			foreach(Contact contact in newContacts)
			{
				Contacts.Add(contact);
			}
			contactStorage.Save(Contacts);
		}
		public void ExportFile(string path)
		{
			contactStorage.Save(Contacts, path);
		}
		public ContactManager(string pathname, string filename)
		{
			contactStorage = new ContactStorage(pathname, filename);
			Contacts = contactStorage.Load();
		}
	}
}
