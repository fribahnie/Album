using System;
using System.IO;
using Gtk;
using AlbumBasis;

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

	/*
		Wertet die Eingaben des Benutzers aus. Mit "Go" wird dann
		der VorschauEditor gestartet und dieses Fenster geschlossen.
	 */
	public partial class StartFenster
	{
		// Das aktuelle Album öffnen:
		static void OnRadio1Clicked(object obj, EventArgs args)
		{
			entry1.IsEditable = false;
			//[38]: "Nichts zu tun; drücke 'Go'"
			// orig: myLabel.Text = StartFenster.Localarray[38];
			myLabel.Text = "Nichts zu tun; drücke 'Go'";
			if (XMLDoc.Albumname == "")
			{
				tb2.Sensitive = false;
				//[39]: "Kein Fotoalbum. Wähle 'Abbruch'!"
				// orig: myLabel.Text = StartFenster.Localarray[39];
				myLabel.Text = "Kein Fotoalbum. Wähle 'Abbruch'!";
			}
		}


		// Ein bestehendes Album auswählen:
		static void OnRadio2Clicked(object obj, EventArgs args)
		{
			XMLDoc.Albumname = StartFensterChooser.RufeFileChooserAuf();
			entry1.Text = XMLDoc.Albumname;
			//[40]: "Name korrekt? – 'Abbruch'/'Go'"
			myLabel.Text = Localarray[40];
			if (XMLDoc.Albumname == "")
			{
				tb2.Sensitive = false;
				//[39]: "Kein Fotoalbum. Wähle 'Abbruch'!"
				myLabel.Text = Localarray[39];
			}
		}


		// Ein neues Album erstellen:
		static void OnRadio3Clicked(object obj, EventArgs args)
		{
			XMLDoc.Albumname = StartFensterChooser.RufeFileChooserAuf();
			entry1.Text = XMLDoc.Albumname;
			//[40]: "Name korrekt? –  'Abbruch'/'Go'"
			myLabel.Text = Localarray[40];
			if (XMLDoc.Albumname == "")
			{
				tb2.Sensitive = false;
				//[39]: "Kein Fotoalbum. Wähle 'Abbruch'!"
				myLabel.Text = Localarray[39];
			}
		}

		// Drehung auf 90 Grad:
		static void OnRadio4Clicked(object obj, EventArgs args)
		{
			XMLDoc.Drehwinkel = 90;
		}

		// Drehung auf 270 Grad:
		static void OnRadio5Clicked(object obj, EventArgs args)
		{
			XMLDoc.Drehwinkel = 270;
		}

		// Die Bilder ins Fotoalbum kopieren?
		static void OnRadio6Clicked(object obj, EventArgs args)
		{
			// CopyImagesBool = radiobutton6.Active ? false : true;
			XMLDoc.CopyImages = "true";
			XMLDoc.CopyImagesBool = true;
			Console.WriteLine("CopyImages ist jetzt: {0}", XMLDoc.CopyImages);
		}

		static void OnRadio7Clicked(object obj, EventArgs args)
		{
			XMLDoc.CopyImagesBool = false;
			XMLDoc.CopyImages = "false";
			Console.WriteLine("CopyImages ist jetzt: {0}", XMLDoc.CopyImages);
		}

		// Das Eingabefeld wurde geändert:
		static void OnEntryChanged(object obj, EventArgs args)
		{
			/*
	      Überprüfe zuerst, ob das Verzeichnis existiert.
	      Dann erst wird der 'Go'-Button freigegeben.
	    */
			Gtk.Entry entry = (Gtk.Entry)obj;
			string name = entry.GetChars(0, -1);
			tb2.Sensitive = false;
			if (radiobutton1.Active && name != "")
			{
				tb2.Sensitive = true;
			}
			if (radiobutton1.Active && name == "")
			{
				tb2.Sensitive = false;
				//[39]: "Kein Fotoalbum. Wähle 'Abbruch'!"
				myLabel.Text = Localarray[39];
			}

			if (radiobutton2.Active && name != "")
			{
				tb2.Sensitive = false;
				AlbumnamePath = XMLDoc.FotoRootDir + XMLDoc.Sep + name;
				DirectoryInfo dir = new(AlbumnamePath);
				if (dir.Exists)
				{
					tb2.Sensitive = true;
				}
			}

			if (radiobutton3.Active && name != "")
			{
				tb2.Sensitive = true;
			}
		}


		// Abbruch gewählt:
		static void OnTb1Clicked(object obj, EventArgs args)
		{
			OnTerminated(obj, args);
		}

		//****************************************************
		// 'Go' gewählt:
		//****************************************************
		static void OnTb2Clicked(object obj, EventArgs args)
		{
			// Das aktuelle Album öffnen:
			if (radiobutton1.Active)
			{
				entry1.Text = XMLDoc.Albumname;
				entry1.IsEditable = false;
				string fotoRootFullPath = XMLDoc.FotoalbenPath;
				AlbumnamePath = Path.Combine(fotoRootFullPath, XMLDoc.Albumname);  // Pfad des Fotoalbums

				string sourceDirName = AlbumnamePath;
				DirectoryInfo dir = new(sourceDirName);

				// Sonderfall, weil z.B. das Verzeichnis gelöscht wurde:
				if (!dir.Exists)
				{
					Console.WriteLine(
					 "Source directory does not exist or could not be found: "
					 + sourceDirName);
					// Default als Ersatz:
					XMLDoc.Albumname = XMLDoc.DefaultName;
					AlbumnamePath = Path.Combine(XMLDoc.FotoalbenPath, XMLDoc.Albumname);
					string path = Path.Combine(XMLDoc.FotoalbenPath, "albumname.txt");
					Console.WriteLine("Das ist mein Pfad: {0}", @path);
					File.WriteAllText(@path, XMLDoc.DefaultName, System.Text.Encoding.UTF8);
				}
			}

			// Ein bestehendes Album öffnen
			if (radiobutton2.Active)
			{
				XMLDoc.Albumname = entry1.Text;
				string path = Path.Combine(XMLDoc.FotoalbenPath, "albumname.txt");
				File.WriteAllText(@path, XMLDoc.Albumname, System.Text.Encoding.UTF8);
				PruefProgramm();
			}

			// Ein neues Album anlegen:
			// Für ein neues Album müssen erst die Startdateien kopiert
			// und die neuen Verzeichnisse angelegt werden.
			// Das wird über das 'PruefProgramm' gesteuert.
			if (radiobutton3.Active)
			{
				XMLDoc.Albumname = entry1.Text;
				PruefProgramm();
			}

			if (XMLDoc.Albumname != "Ungültiger Albumname")
			{
				/*
					Startet des Hauptprogramm mit dem VorschauEditor als Fenster!
				 */
				AlbumApp.Hauptprogramm();
			}
			else
			{
				Console.WriteLine("Es gibt kein solches Album!!!");
				Gtk.Application.Quit();
			}
		}


		private static void PruefProgramm()
		{
			string fotoRootFullPath = XMLDoc.FotoalbenPath;
			Console.WriteLine("FotoalbenPath: {0}", XMLDoc.FotoalbenPath);
			AlbumnamePath = Path.Combine(fotoRootFullPath, XMLDoc.Albumname);
			Console.WriteLine("Fotoalbum im Prüfprogramm: {0}", AlbumnamePath);
			DirectoryInfo dir = new(AlbumnamePath);
			DirectoryInfo[] dir1 = dir.GetDirectories();

			if (!dir.Exists)
			{
				Console.WriteLine("Der Albumname ist nicht bekannt. Schreibfehler?");
			}

			if (dir.Exists)
			{
				if (dir1.Length == 0)
				{
					Console.WriteLine("in diesem Verzeichnis ist nichts drin");
					// Schreibe den Albumnamen ins Verzeichnis
					string myfile = Path.Combine(AlbumnamePath, "albumname.txt");
					File.WriteAllText(myfile, XMLDoc.Albumname, System.Text.Encoding.UTF8);

					// Copy from the current directory, include subdirectories.
					DirectoryCopy(Rel + "Baukasten" +
						XMLDoc.Sep + Startpfad +
						XMLDoc.Sep + "Starterset",
						AlbumnamePath, true);
				}
			}
		}


		private static void DirectoryCopy(string sourceDirName,
							string destDirName,
							bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
				"Source directory does not exist or could not be found: "
										 + sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.       
			Directory.CreateDirectory(destDirName);

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string tempPath = Path.Combine(destDirName, file.Name);
				file.CopyTo(tempPath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string tempPath = Path.Combine(destDirName, subdir.Name);
					// Console.WriteLine( "Kopiere: {0}", tempPath );
					DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
				}
			}
		}


		static void Window_Delete(object obj, DeleteEventArgs args)
		{
			Application.Quit();
			args.RetVal = true;
		}


		static void OnTerminated(object sender, EventArgs args)
		{
			Gtk.Application.Quit();
		}

		public static void ZeigeFortschritt()            // steuert den Fortschrittsbalken
		{
			double augm = 1.0 / AlbumRead.SeitenList.Count; // 1.0 : Seitenzahl
			double new_val = pbar.Fraction + augm;
			if (new_val <= 1)
				pbar.Fraction = new_val;
			else
			{
				pbar.Fraction = augm;
			}
			// Der Fortschrittsbalken funktioniert leider nicht mehr.
			// pbar.GdkWindow.ProcessUpdates(true); // macht den Fortschritt sichtbar 
			//pbar.Window.ProcessUpdates(true);
			//pbar.ShowAll();
		}
	}
}
