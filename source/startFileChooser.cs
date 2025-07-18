using Gtk;
using System;
using System.IO;
using System.Linq;

namespace Startfenster
{
	class StartFensterChooser
	{
		public static string RufeFileChooserAuf()
		{
			//[41]: "Fotoalbum auswählen"
			//[47]:  "Okay"
			//[8]:  "  Abbruch "
			FileChooserDialog fcd = new(StartFenster.Localarray[41],
								 StartFenster.myWin,
								 FileChooserAction.SelectFolder,
								 StartFenster.Localarray[47],
								 ResponseType.Ok,
								 StartFenster.Localarray[8],
								 ResponseType.Close
								 )
			{
				SelectMultiple = false,
				DefaultWidth = 1200,
				DefaultHeight = 500
			};

			string mydir = string.Empty;
			fcd.SetFilename(XMLDoc.FotoalbenPath + "/*");
			if (fcd.Run() == (int)Gtk.ResponseType.Ok)
			{
				string mystring = fcd.CurrentFolder;
				// Console.WriteLine( "Der Ordnername: {0}", mystring );
				mystring = mystring.Replace("\\", "/");
				XMLDoc.FotoalbenPath = XMLDoc.FotoalbenPath.Replace("\\", "/");
				// Console.WriteLine( mystring );
				// Console.WriteLine( "FotoalbenPath: {0}", StartFenster.FotoalbenPath );
				if (mystring == XMLDoc.FotoalbenPath)
				{
					mydir = Path.GetFileName(fcd.Filename);
					// Console.WriteLine( "File: {0}", mydir );
					if (!Directory.EnumerateFileSystemEntries(fcd.Filename).Any()) // leerer Ordner
					{
						// Console.WriteLine( "Da ist nichts drin!" );
					}
				}
				else
				{
					Console.WriteLine("Das war wohl nichts");
				}
			}
			fcd.Destroy();
			return mydir;
		}
	}
}
