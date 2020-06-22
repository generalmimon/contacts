# Contacts

Contacts is a simple desktop app for Windows written in C# WPF. It can be used for saving contacts on your local drive. It has only the basic funcionality: add, edit and remove a single contact/remove all contacts.

All contacts are stored in a CSV file without header (semicolon delimited). The file is located at `%APPDATA%/Kontakty/kontakty.csv`. They can be imported and exported in this format.

Every line in the CSV file has the following format:

```
First name;Surname;Phone number;E-mail address
```

The program uses a custom CSV reader and writer, which can handle even the strings containing the `;` character used to delimit the fields (it wraps the string in double quotes: `"`). The user input can also contain semicolons (`;`) and double quotes (`"`) at the same time. In that case, the string is wrapped in double quotes because of the presence of `;`, and every occurence of `"` is doubled `""` so that the application can distinguish normal quotes and the final quote denoting the end of the field.

For example, a contact with the first name _John_, the surname _D;o"e_, phone number _123 456 789_ and email _john@doe.com_ will be saved like this:

```
John;"D;o""e";123 456 789;john@doe.com
```

