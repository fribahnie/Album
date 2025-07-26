using System;
using System.IO;
using System.Xml;
using System.Collections;
using Startfenster;
using AlbumBasis;
using System.Collections.Generic;

namespace ModuleHtml
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
	public class HtmlBuild
	{
		/*
			Die Methoden der Klasse:

			'OnBaueHtml()'
			ruft auf:
				'AlbumSave.OnSichern()'
				'ErzeugeInhaltsString(AlbumSave.InhaltSaveList)'
				'HtmlEinlesen()'
				'ErstelleInhalt(inhaltstr)'
				  'HtmlEinlesen()' ruft auf:
					   'static Hashtable MasseEinlesen()'
					   'HtmlSeite.ErzeugeSeite(vorschauseite);'
		 */

		public static int Groessezahl { set; get; } // 0 - 2: je nach Bildschirmaufl.
		public static string Groesse { set; get; } // "mittel", "gross" oder "vierk"
		public static string AS_Sichern { set; get; } // '@"/Albumseiten" + "/qqgroesse" + "/seite";'
		public static string ES_Sichern { set; get; } // Pfadabschnitt mit Größenauswahl
		public static string Templatepfad { set; get; } // Pfad zu den Vorlagen
		public static string InhaltBild { set; get; } // die Datei mit dem Bild für das Inhaltsverzeichnis

		public static void OnBaueHtml(object sender, EventArgs args)
		{
			HtmlSeite.Seitenzaehler = 1; // Seitenzaehler zurückgesetzt
			HtmlBild.Bilderzaehler = 1; // Bilderzaehler zurückgesetzt 
			AlbumSave.OnSichern(sender, args); // sichert akt. Stand
			string inhaltstr = ErzeugeInhaltsString(AlbumSave.InhaltSaveList);
			Console.WriteLine(inhaltstr);// 'inhaltstr' wird später in die html-Seite 'seite0000.html' eingesetzt.
			HtmlEinlesen();              // Die HtmlSeiten werden erzeugt
			ErstelleInhalt(inhaltstr);   // Erstellt die Seite 'seite0000.html' mit dem Inhaltsverzeichnis
		}

		/* Erstellt aus der Liste 'inhaltList' die Links im Inhaltsverzeichnis: */
		static string ErzeugeInhaltsString(List<string> inhaltList)
		{
			string inhaltstr = string.Empty;
			int seitenzaehler = 1;
			foreach (string str in inhaltList)
			{
				var item = str ?? string.Empty;
				var laenge = inhaltList.Count;
				Console.WriteLine("Die Elemente der Inhaltsliste: {0}; Ihre Länge: {1}", item, laenge);
				string formatstring = string.Format("{0,4:0000}", seitenzaehler);
				string inhaltitem = string.Empty;
				// Zielformat:	  <li>  <a href="./seite010.html">Kirschblüte (10)</a> </li>
				if (item.Length != 0)
				{
					inhaltitem = string.Format(@"<li> <a href=""./seite{0}.html"">{1} ({2})<a/></li>", formatstring, str, seitenzaehler);
					inhaltitem += "\n";
				}
				inhaltstr += inhaltitem;
				seitenzaehler++;
			}
			return inhaltstr;
		}

		/* Erstellt das Inhaltsverzeichnis für das Album als Seite 'seite0000.html' */
		static void ErstelleInhalt(string inhaltstr)
		{
			/*
				Das Template für das Inhaltsverzeichnis wird eingelesen und bearbeitet: 
			*/
			string htmlVorlage = Templatepfad + "/inhalt.html";
			string htmltext = File.ReadAllText(htmlVorlage);

			// Der Suchstring für die Größenangabe ('vierk', 'gross', 'mittel'):
			string suchgroesse = "qqqgroesse";
			htmltext = htmltext.Replace(suchgroesse, Groesse);

			// Der Suchstring für das Bild im Inhaltsverzeichnis:
			string suchinhaltbild = "qqqIVBild";
			htmltext = htmltext.Replace(suchinhaltbild, InhaltBild);

			// Der Suchstring für die Größe des Navigationsbildes:
			string suchnavwidth = "qqqnavwidth";
			htmltext = htmltext.Replace(suchnavwidth, HtmlEinzelseite.Knopfbreite);

			// Der Suchstring für das Inhaltsverzeichnis:
			string suchinhalt = "qqqinhalt";
			htmltext = htmltext.Replace(suchinhalt, inhaltstr);
			string dateipfad = StartFenster.AlbumnamePath + AS_Sichern + "0000.html";
			File.WriteAllText(dateipfad, htmltext, System.Text.Encoding.UTF8);
		}


		/* aufgerufen von 'OnBaueHtml()'  */
		static void HtmlEinlesen()
		{
			string path = Directory.GetCurrentDirectory();
			Templatepfad = @path + "/Baukasten/templates";

			int bildermax = 0;                    // Anzahl der Bilder insgesamt; wichtig für Einzelseiten
			HtmlSeite.Seitenmax = AlbumApp.Seitenmax + 1; // Gesamtzahl der Seiten
			for (int i = 0; i < AlbumRead.Seitenliste.Count; i++)
			{
				AlbumSeite item = AlbumRead.Seitenliste[i];
				int bilderzaehler = item.Bilderliste.Count;
				bildermax += bilderzaehler;
			}
			Console.WriteLine("Gesamtzahl der Bilder im Album: {0}", bildermax);
			HtmlEinzelseite.Bildermax = bildermax;

			/*
	      Die einheitliche html-Vorlage für die Großansicht der Bilder
	      wird eingelesen:
	    */
			HtmlEinzelseite.HtmlEinzVorl = File.ReadAllText(Templatepfad + @"/einzeln.html");

			/* 
	       die Werte in 'htmlmasse.xml' werden gelesen 
	       und in einer Hashtable gespeichert:
	    */

			Console.WriteLine("Nun wird eine Hashtable erstellt.");
			Hashtable ht = new();
			ht = MasseEinlesen();
			// Auslesen der ersten beiden Werte aus der Hashtable:
			string knopfbreite = ht["qqknopf"].ToString();
			string conthoehe = ht["qq_bbreite"].ToString();
			HtmlEinzelseite.Knopfbreite = knopfbreite;
			HtmlEinzelseite.Conthoehe = conthoehe;

			// Erstellt eine Collection der Schlüssel der Hashtable:
			ICollection key = ht.Keys;
			HtmlSeite.Key = key;                 // Wertzuweisung
			HtmlSeite.Ht = ht;                  // Wertzuweisung
			Console.WriteLine("Bildbreite16x12: " + ht["qq16x12Q"].ToString());
			Console.WriteLine("Bildbreite16x09: " + ht["qq16x09Q"].ToString());
			Console.WriteLine("Bildbreite16x12: " + ht["qq16x12H"].ToString());
			Console.WriteLine("Bildbreite16x09: " + ht["qq16x09H"].ToString());
			HtmlSeite.Bildbreite16x12Q = ht["qq16x12Q"].ToString();
			HtmlSeite.Bildbreite16x09Q = ht["qq16x09Q"].ToString();
			HtmlSeite.Bildbreite16x12H = ht["qq16x12H"].ToString();
			HtmlSeite.Bildbreite16x09H = ht["qq16x09H"].ToString();

			/*
				Lösche die bisherigen Dateien in zwei Durchgängen:
				1. Die Albumseiten, 2. Die Einzelseiten;
				Wichtig für den Fall, dass das neue Album 
				kleiner ist als das alte. Ansonsten bestehen
				alte Albumseiten und alte Einzelseiten weiter.
			*/

			// Array für die beiden Durchläufe:
			string[] durchlauf = { "/Albumseiten/", "/Einzelseiten/" };

			// Die beiden Durchläufe:
			for (int i = 0; i < 2; i++)
			{
				try
				{
					// Verzeichnis mit den zu löschenden html-Dateien:
					string dir = StartFenster.AlbumnamePath + durchlauf[i] + Groesse;
					var htmlFiles = Directory.EnumerateFiles(dir, "*.html");
					if (htmlFiles != null)
					{
						foreach (string currentFile in htmlFiles)
						{
							File.Delete(currentFile);
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
			}

			// Die große Schleife: Alle Albumseiten werden durchlaufen:
			foreach (AlbumBasis.AlbumSeite vorschauseite in AlbumRead.Seitenliste)
			{
				Console.WriteLine("In der großen Schleife");
				HtmlSeite.ErzeugeSeite(vorschauseite);
			}
			Console.WriteLine("Alle Html-Seiten erstellt!");
			Console.Beep();
		}


		/* aufgerufen von 'HtmlEinlesen()'  */
		static Hashtable MasseEinlesen()
		{
			/* 
	       Einlesen der Htmlmaße, die dann in die Vorlage eingefügt werden: 
	       schneidet aus der xml-Datei mit den Maßen den angegebenen 
	       Bereich für die weitere Auswertung heraus:
	       bereiche[0]- mittel
	       bereiche[1]- gross
	       bereiche[2]- vierk
	    */
			XmlDocument xmlmassangaben = new();
			xmlmassangaben.Load("./Baukasten/Werte/htmlmasse.xml");

			/*
	      Ich wähle zur Zeit die große Bildschirmgröße (3840x2160):
	      Groessezahl = 2. Sie kann per Menü oder
	      über den Wert in './Album/Baukasten/Werte/vorschaumasse.xml' geändert werden.
	    */
			Groessezahl = StartFenster.DisplayDefault;

			Console.WriteLine("Die Größezahl lautet nun: {0} ", Groessezahl);

			/*
	      'as_sichern' und 'es_sichern' sind Pfadangaben, die je nach
	      gewählter Bildschirmgröße variieren. In der folgenden
	      foreach-Schleife werden die Werte 'mittel', 'gross' oder
	      'vierk' in die Pfadangabe eingesetzt:
	    */
			string groesse = string.Empty; // "mittel", "gross" oder "vierk"
			string bereich = string.Empty; // mittel, gross oder vierk
			string[] bereiche = StartFenster.Bereiche;
			string as_sichern = @"/Albumseiten" + "/qqgroesse" + "/seite";
			string es_sichern = @"/Einzelseiten" + "/qqgroesse" + "/einzel";

			XmlNodeList xnList = xmlmassangaben.SelectNodes("/werte");
			foreach (XmlNode xn in xnList)
			{
				bereich = xn[bereiche[Groessezahl]].OuterXml;
				groesse = xn[bereiche[Groessezahl]].Name;
				as_sichern = as_sichern.Replace("qqgroesse", groesse);
				es_sichern = es_sichern.Replace("qqgroesse", groesse);
			}
			Groesse = groesse;
			AS_Sichern = as_sichern;
			ES_Sichern = es_sichern;

			/* 
	       Lädt den ausgewählten Bereich: 
	    */
			XmlDocument xmlbereich = new();
			xmlbereich.LoadXml(bereich);

			Console.WriteLine("Der Bereich ist erfolgreich erstellt. Er lautet: {0}", xmlbereich);

			/*
	      Speichert die Keys und die Werte für die Maße 
	      in einer Hashtable:
	    */
			Hashtable ht = new();
			XmlNode root = xmlbereich.FirstChild;
			if (root.HasChildNodes)
			{
				for (int i = 0; i < root.ChildNodes.Count; i++)
				{
					Console.WriteLine("Die Anzahl der ChildNodes ist: {0}", root.ChildNodes.Count);
					ht.Add(root.ChildNodes[i].Name, root.ChildNodes[i].InnerText);
					Console.WriteLine("Name: {0} Text: {1}",
								root.ChildNodes[i].Name,
								root.ChildNodes[i].InnerText);
				}
			}
			Console.WriteLine("Wir sind jetzt kurz vor dem return.");
			return ht;
		}
	}
}
