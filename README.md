# Picross

This is a solo project developed in one week by Michael Mazur during project week at Coding Dojo Chicago.
It is a website to allow users to create accounts and login to make, share, and solve Picross puzzles.

## What is picross?

Picross puzzles, also known as nonograms, are grid based number and logic based puzzles.
[This article](https://en.wikipedia.org/wiki/Nonogram) from Wikipedia explains the concept fairly well.

## What technologies does it use?

This project was built with ASP.NET Core v2.2. Web pages are created as razor files and all the
front end puzzle logic was created with vanilla javascript.

## Is there a public URL?

Not now. I want to rebuild this project with a React front end so when that happens, I will consider
hosting the site somewhere.

## How can I download this and run it myself?

Make sure you're using ASP.NET Core 2.2. Then cd into the main project directory and run:
```bash
touch appsettings.json
dotnet restore
```
or if you're using PowerShell
```bash
New-Item appsettings.json
dotnet restore
```

This will install all the necessary dependencies. Make sure you also have MySQL installed for the next part.
Open appsettings.json and insert the following:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "DBInfo":
  {
      "Name": "MySQLconnect",
      "ConnectionString": "server=localhost;userid=root;password=root;port=3306;database=mydb;SslMode=None"
  }
}
```
Make sure to change relevant information in "ConnectionString" to match your specific database.
Run the following command in the project root:

```bash
dotnet watch run
```

Now go to [this address](http://localhost:5000/) in your browser.
When you're playing a puzzle, left click fills in a cell. Right click greys out a cell.

## Questions???
Feel free to send me an email with any questions about this project:
mazurasdf at gmail.com