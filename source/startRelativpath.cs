using System;
using System.IO;
//using AlbumBasis;


namespace Startfenster
{
	public class RelativPaths
	{
		/*
			Die Methode:
			'public static void RelativePfade()'

			liefert:
			- StartFenster.AlbumRootPath – Grundordner des Programms
			- StartFenster.Wurzelpfad    – gemeinsamer Pfadabschnitt von s1 und s2
			- StartFenster.Ersatzstr     – rel. Pfad: Htmlseiten ↦ Bilddateien
			- StartFenster.Sep           – Directory Seperator
			- StartFenster.Rel           - "./"

			macht aus absoluten relative Pfade für die Bilder
			in den html-Seiten des Fotoalbums. 
			Die Methode wird aufgerufen von 'StartFenster';
		*/

		public static string RelPfad { set; get; }
		public static int BilderPfadLaenge { set; get; }
		public static int LaengeRelBilderOrdner { set; get; }
		public static bool AlternativOrdnerBool { set; get; }
		public static string AltDirectory { get; set; }// der eingegebene alternative BilderOrdner

		public static void RelativePfade()
		{
			char slash = Path.DirectorySeparatorChar;
			string sep = slash.ToString();
			StartFenster.Rel = "." + sep;
			int plattformkorr = (StartFenster.Plattform == 0) ? 2 : 0; // je nach Plattform: win 2; linux 1;
			Console.WriteLine("Current directory: {0}", Directory.GetCurrentDirectory());
			string currentDir = Directory.GetCurrentDirectory();
			int laenge = currentDir.Length;
			int position = currentDir.LastIndexOf("Album", laenge, laenge); // liefert den letzten Index von 'Album'
			/* 's2' wird später der Rootpfad des Programms. Hier zunächst der Ausgangswert: */
			string s2 = currentDir[plattformkorr..position] + "Album";
			string start = string.Empty;
			if (StartFenster.Plattform == 0) { start = currentDir[0..2]; } // Windows!
			Console.WriteLine("Als Root-Pfad für Album erhalte ich: {0} 'start' ist : {1}", s2, start);
			// 'AlternativOrdner' ist 'true', wenn ein Eintrag vorhanden und die Plattform Windows ist;
			// bool alternativOrdner = false;
			// alternativOrdner = AltDirectory != "" && StartFenster.Plattform == 0;
			// s1 - Pfad zum Bilderordner;
			// s2 - Pfad zum Albumordner, wo das Programm läuft
			// s4 - aktueller Ordner
			// der relative Pfad 'wechsel' ermöglicht den Wechsel in den Ordner 'Album'
			string s1;
			// Der Normalfall:
			s1 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			Console.WriteLine("Als Bilderpfad erkannt: {0}", s1);
			// s2: Hier erst mal die Rohfassung; Der Endwert ist plattformabhängig:
			s2 += sep;
			// if (StartFenster.Plattform == 0) { s2 = s2 + sep; } else { s2 = sep + s2 + sep; }
			// Bei Windows erwartet das Programm einen Laufwerksbuchstaben plus ":"
			s2 = StartFenster.Plattform == 1 ? s2 : start + s2;
			Console.WriteLine("Der Pfad s2 lautet jetzt nach der Bearbeitung: {0}", s2);
			Directory.SetCurrentDirectory(s2); // 'Album' wird das aktuelle Verzeichnis
			string s4 = Environment.CurrentDirectory;         // aktuelles Verzeichnis
			string ersatz = Path.GetRelativePath(s2, s1);     // vom Albumordner zum Bilderordner;
			string wechsel = Path.GetRelativePath(s4, s2);    // 
			Console.WriteLine("Als 'ersatz' erkannt: {0} und als 'wechsel: {1}", ersatz, wechsel);
			s1 += sep;
			ersatz += sep; // vom Albumordner zum Bilderordner
			LaengeRelBilderOrdner = ersatz.Length;
			Console.WriteLine("ersatz: {0} Länge des Relativpfads: {1}", ersatz, LaengeRelBilderOrdner);
			wechsel += sep;
			Console.WriteLine("'ersatz' vom Albumordner zum Bilderordner: {0}", ersatz);
			// Der relative Pfad 'wechsel' macht das Albumverzeichnis zum aktullen Verz.:
			// Damit kann das Albumprogramm auch über Links aufgerufen werden.
			Environment.CurrentDirectory = Path.Combine(Environment.CurrentDirectory, wechsel);
			Console.WriteLine("Bilderordner: {0}", s1);
			Console.WriteLine("Albumordner:  {0}", s2);
			string rootpath = Path.GetFullPath(s2); // der vollständige Pfad von 'Album';
			int startIndex = s1.LastIndexOf(sep);
			string bilderOrdner = s1[..startIndex];
			Console.WriteLine("Kontrolle Bilderordner: {0}", bilderOrdner);
			/* Ermittelt den Wert von 'HomeBilder' : */
			startIndex = bilderOrdner.LastIndexOf(sep) + 1; // Bilderordner: Index des letzten 'sep' + 1
			int bildpfadlaenge = s1.Length;                 // Die Pfadlänge des Bilderordners
			BilderPfadLaenge = bildpfadlaenge;
			bildpfadlaenge -= startIndex;
			string bilderOrdnerrechts = s1.Substring(startIndex, bildpfadlaenge); // Die rechte Seite des Bilderordners
			string homeBilder = s1[..startIndex];
			XMLDoc.HomeBilder = homeBilder; // z.B.:  'C:\Users\fbahr\'
			StartFenster.StartIndex = startIndex;
			Console.WriteLine("Die Wurzel des Pfads: {0} der Rest: {1}", homeBilder, bilderOrdnerrechts);
			Console.WriteLine("Stimmt der rootpath? {0}", rootpath);
			XMLDoc.Sep = sep;
			Console.WriteLine("AlternativOrdner steht auf: {0}", AlternativOrdnerBool);
			XMLDoc.AlbumRootPath = rootpath;       // für HtmlBild
																						 // '..'   - rel. Pfad ein Verzeichnis nach oben; nötig: 4x
																						 // 'ersatz' - rel. Pfad von den html-Seiten zu den Bilderdateien;
			RelPfad = ersatz;
			//ersatz = "." + sep + ".." + sep + ".." + sep + ".." + sep + ".." + sep;
			//StartFenster.Ersatzstr = ersatz; // für HtmlBild
			Console.WriteLine("RelPfad:    {0}", ersatz);
			Console.WriteLine("Wechsel:    {0}", wechsel);
			Console.WriteLine("Wurzelpfad: {0} Sep: {1}", s2, XMLDoc.Sep);
			Console.WriteLine("Durchmarsch beendet.");
		}
	}
}
