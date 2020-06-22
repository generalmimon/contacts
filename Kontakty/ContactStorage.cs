using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Kontakty
{
	public class ContactStorage
	{
		private string fullPath;
		public ContactStorage(string pathname, string filename)
		{
			preparePath(pathname, filename);
		}
		private void preparePath(string pathName, string fileName)
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), pathName);
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
			fullPath = Path.Combine(path, fileName);
		}
		private List<string> parseLine(string line)
		{
			List<string> values = new List<string>();
			string currentValue = "";
			bool isInQuotes = false;
			for(int i = 0; i < line.Length; i++)
			{
				char ch = line[i];
				switch(ch)
				{
					case ';':
						if (!isInQuotes)
						{
							values.Add(currentValue);
							currentValue = "";
						}
						else {
							currentValue += ch;
						}
						break;
					case '"':
						if (!isInQuotes) isInQuotes = true;
						else
						{
							if(i + 1 < line.Length && line[i + 1] == '"')
							{
								currentValue += ch;
								i++;
							} else
							{
								isInQuotes = false;
							}
						}
						break;
					default:
						currentValue += ch;
						break;
				}
			}
			values.Add(currentValue);
			return values;
		}
		private string getLine(List<string> values)
		{
			List<string> escapedValues = new List<string>();
			string escapedValue = "";
			foreach(string value in values)
			{
				escapedValue = value;
				if(value.Contains('"'))
				{
					escapedValue = escapedValue.Replace("\"", "\"\"");
				}
				if(value.Contains('"') || value.Contains(';'))
				{
					escapedValue = '"' + escapedValue + '"';
				}
				escapedValues.Add(escapedValue);
			}
			return string.Join(";", escapedValues);
		}
		public ObservableCollection<Contact> Load(string path = "")
		{
			ObservableCollection<Contact> contactList = new ObservableCollection<Contact>();
			if(path == "") path = fullPath;
			if(File.Exists(path)) {
				using(StreamReader sr = new StreamReader(path))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						List<string> values = parseLine(line);
						Contact newContact = new Contact(values[0], values[1], values[2], values[3]);
						contactList.Add(newContact);
					}
				}
			}
			return contactList;
		}
		public void Save(ObservableCollection<Contact> contactList, string path = "")
		{
			if (path == "") path = fullPath;
			using (StreamWriter sw = new StreamWriter(path))
			{
				foreach (Contact contact in contactList)
				{
					List<string> values = new List<string> {contact.Firstname, contact.Surname, contact.Tel, contact.Email};
					string line = getLine(values);
					sw.WriteLine(line);
				}
			}
		}
	}
}
