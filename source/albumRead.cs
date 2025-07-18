using System.Xml;
using System.IO;
using System.Collections.Generic;
using Startfenster;
using ModuleHtml;
using System;
using System.Linq;

/*
  Die Datei 'albumdaten.xml' wird hier eingelesen und ausgewertet.
  Zwei Menüeinträge lassen sich mit den Methoden der Klasse bearbeitet:
  'OnSichern()'
  'OnBaueHtml()'
*/

namespace AlbumBasis
{

	public static class AlbumRead                     // lies das gesammte Album 
	{
		public static List<AlbumSeite> Seitenliste { set; get; } // die Seiten des Albums
		public static string Albumdatenpfad { set; get; } // xml-Datei mit den A.Daten

		public static int[] Vorschauarray { set; get; }
		public static XmlNodeList SeitenList { set; get; }

		/*
			Die folgende Methode liest die Maße ein, die in
			'vorschaumasse.xml' gespeichert sind. Sie
			enthalten die Breite und Höhe der Vorschaubilder,
			yoffset und den verwendeten Abstand und speichert 
			sie in dem Int-Array 'Vorschauarray', einem Feld
			der Klasse Album.
		*/
		public static void VorschauMasse()
		{
			XmlDocument xmlDocVorschau = new();
			string xmlpfad = XMLDoc.AlbumRootPath + "/Baukasten/Werte/vorschaumasse.xml";
			//Console.WriteLine("Der xml-Pfad: {0}", xmlpfad);
			xmlDocVorschau.Load(xmlpfad);

			XmlNode vorschau = xmlDocVorschau.SelectSingleNode("vorschau");
			if (vorschau.HasChildNodes)
			{
				int[] vorschauarray = new int[5];
				for (int i = 0; i < vorschau.ChildNodes.Count; i++)
				{
					vorschauarray[i] = int.Parse(vorschau.ChildNodes[i].InnerText);
				}
				Vorschauarray = (int[])vorschauarray.Clone();
			}
			AlbumApp.XmlDocVorschau = xmlDocVorschau;
		}


		public static void LiesXmlData()
		{
			VorschauMasse();                          // holt wichtige Maße für das Fenster
																								// File 'albumdaten.xml' mit den Daten des Fotoalbums:
			Albumdatenpfad = Path.Join(XMLDoc.FotoalbenPath, XMLDoc.Albumname);
			Albumdatenpfad = Path.Join(Albumdatenpfad, "albumdaten.xml");
			//Console.WriteLine("Albumdaten: {0}", Albumdaten);
			var fi = new FileInfo(Albumdatenpfad);

			/*
	      Diese Methode liest die gespeicherten Albumdaten 
	      aus 'albumdaten.xml' ein und erzeugt daraus das Album für
	      das Erstellungsprogramm und gibt das Ergebnis an die 
	      aufrufende Instanz zurück. Dazu ruft es weitere 
	      Methoden auf, die die eingelesene Datei analysieren.
	      Das Album liegt dann vor als 'List<AlbumSeite> seitenliste'.
	    */
			XmlDocument xmlalbum = new();                   // 'albumdaten.xml'
			xmlalbum.Load(fi.FullName);                     // wird eingelesen;
			UntersucheAlbum(xmlalbum);                      // ruft die nächste Stufe
																											// der Analyse auf
		}


		static void UntersucheAlbum(XmlDocument xmlalbum)
		{
			/*
	      diese Methode erzeugt das Album. Dafür werden 
	      weitere Methoden aufgerufen.
	    */
			List<AlbumSeite> seitenliste = new();           // alle Seiten des Albums
			XmlDocument xmlseite = new();                   // die einzelne Seite als xml-Doc
			XmlNodeList formatList = xmlalbum.GetElementsByTagName("format");
			XmlNodeList bildformatList = xmlalbum.GetElementsByTagName("bildformat");
			XmlNodeList titelList = xmlalbum.GetElementsByTagName("titel");
			XmlNodeList inhaltList = xmlalbum.GetElementsByTagName("inhalt");
			SeitenList = xmlalbum.GetElementsByTagName("seite");
			XmlNodeList inhaltbildList = xmlalbum.GetElementsByTagName("inhaltbild");
			string inhaltbild = inhaltbildList[0].InnerText;
			HtmlBuild.InhaltBild = inhaltbild;

			for (int j = 0; j < SeitenList.Count; j++)      // erzeuge alle Albumseiten
			{
				string seitentext = SeitenList[j].OuterXml;
				xmlseite.LoadXml(seitentext);                 // String wird eingelesen
																											// die nächste Stufe der Analyse:

				AlbumSeite seite = new();
				string format = formatList[j].InnerText;      // Das gewählte Format
				string breitehoehe = bildformatList[j].InnerText; // Das Bildformat 16x12 od. ...
				string titel = titelList[j].InnerText;        // Titel der Seite
				string inhalt = inhaltList[j].InnerText; // Der Eintrag für das Inhaltsverzeichnis der Seite

				/* Methodenaufruf liefert die fertige Vorschauseite: */

				// Console.WriteLine("Als Format wurde eingelesen: {0}", format);
				seite = AlbumBuildSeite.BuildSeite(xmlseite, format, breitehoehe, titel, inhalt); // analysiere Seite
				seitenliste.Add(seite);                       // listet die Seiten des Albums
																											// AlbumNeueBilder.Seite = seite;                // speichert die fertige Seite;
				AlbumSeite testseite = seitenliste[j];
				int testEboxLaenge = testseite.Eboxliste.Count;
				Console.WriteLine("Die Länge der Eboxliste von Seite {0} ist {1}", j + 1, testEboxLaenge);
				StartFenster.ZeigeFortschritt();              // lässt den Fortschrittsbalken zunehmen
			}
			Seitenliste = seitenliste;            // Wertezuweisung = das ganze Album mit allen Seiten ist hier gespeichert
																						//funktioniert nicht mehr: SystemSounds.Asterisk.Play();        
																						// Einlesen ist abgeschlossen.
			Console.WriteLine("Die Länge der Seitenliste ist {0}", Seitenliste.Count);
			StartFenster.myWin.Destroy();
		}
	}
}
