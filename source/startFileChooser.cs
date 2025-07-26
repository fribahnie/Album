using Gtk;
using System;
using System.IO;
using System.Linq;

namespace Startfenster
{
	/*
		Lizenzbedingungen:

		AlbumEditor zur Erstellung eines digitalen Fotoalbums aus HTML-Seiten.
		Copyright(C) 2025 
		Frieder Bahret

		This program is free software; you can redistribute it and/or modify it
		under the terms of the GNU General Public License as published by the
		Free Software Foundation; either version 3 of the License, 
		or(at your option) any later version.

		This program is distributed in the hope that it will be useful, 
		but WITHOUT ANY WARRANTY; without even the implied warranty of
		MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
		See the GNU General Public License for more details.

		You should have received a copy of the GNU General Public License
		along with this program; if not, see<http://www.gnu.org/licenses/>.
	*/
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
