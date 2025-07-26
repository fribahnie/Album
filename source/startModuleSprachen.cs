using System;
using System.Collections;
using Startfenster;

namespace ModuleSprachen
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
	public class Sprachdateien
	{
		public static void SpracheLaden(string Sprachdatei)
		{
			Console.WriteLine("Der Startpfad für die Sprachtabellen wird erstellt.");
			// Erstelle 'Startpfad':
			Hashtable sprachtabelle = new()
			{
				{ "local_de.txt", "Start_de" },
				{ "local_en.txt", "Start_en" }
			};
			StartFenster.Startpfad = (string)sprachtabelle[Sprachdatei]; // Typumwandlung
			Console.WriteLine("Der Startpfad für die Sprachdatei: {0}", StartFenster.Startpfad);

			// Erstelle 'StartFenster.Localarray':
			string sep = XMLDoc.Sep;
			string filepfad = XMLDoc.AlbumRootPath + "Baukasten" + sep + "local" + sep + Sprachdatei;
			Console.WriteLine("In SpracheLaden ist der filepfad {0}", filepfad);
			string ergstr = System.IO.File.ReadAllText(filepfad);
			ergstr = ergstr.Replace("\n", string.Empty);
			//Console.WriteLine("SpracheLaden: Der String: {0}", ergstr);
			string[] localarr = ergstr.Split(',');
			//Console.WriteLine("Die Länge des localarray: {0}", localarr.Length);
			//Console.WriteLine(localarr[^1]);
			StartFenster.Localarray = (string[])localarr.Clone();
		}
	}
}