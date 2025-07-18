using System;
using System.Collections;
using Startfenster;

namespace ModuleSprachen
{
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