using System;
using System.Xml;
using System.Collections.Generic;
using Startfenster;
using ModuleHtml;
using BasicClasses;

namespace AlbumBasis
{
	public static class AlbumSave
	{
		// 'StartIndex' wird bereitgestellt von 'AlbumBasics':
		// (für das Zuschneiden des Dateipfades der Bilder)
		public static int StartIndex { set; get; }
		public static List<string> InhaltSaveList { set; get; }

		public static void OnSichern(object sender, EventArgs args)
		{
			// Console.WriteLine( "Aktueller Stand wird gesichert" );
			Console.WriteLine("Das Bild für IV: {0}", HtmlBuild.InhaltBild);
			List<string> inhaltSaveList = new();
			string[] albumstart = {"<?xml version='1.0' encoding='UTF-8'?>",
												 "<album>"};
			string[] albumend = { "</album>" };
			string[] inhaltbildstart = { "<inhaltbild>" };
			string[] inhaltbildend = { "</inhaltbild>" };
			string[] seitestart = { "  <seite>" };
			string[] seiteend = { "  </seite>" };
			string[] formatstart = { "    <format>" };
			string[] formatend = { "</format>" };
			string[] breitestart = { "    <bildformat>" };
			string[] breiteend = { "</bildformat>" };
			string[] titelstart = { "    <titel>" };
			string[] titelend = { "</titel>" };
			string[] inhaltstart = { "    <inhalt>" };
			string[] inhaltend = { "</inhalt>" };
			string[] bilderstart = { "    <bilder>" };
			string[] bilderend = { "    </bilder>" };
			string[] klistestart = { "    <kommentarliste>" };
			string[] klisteend = { "    </kommentarliste>" };

			using (System.IO.StreamWriter file =
			 new(AlbumRead.Albumdatenpfad))
			{
				foreach (string line in albumstart)
				{
					file.WriteLine(line);
				}

				foreach (string line in inhaltbildstart)
				{
					file.WriteLine(inhaltbildstart[0] + HtmlBuild.InhaltBild + inhaltbildend[0]);
				}

				foreach (AlbumSeite seite in AlbumRead.Seitenliste)
				{
					Console.WriteLine("eine neue Seite beginnt mit Formatname: {0}", seite.Formatname);

					file.WriteLine(seitestart[0]);
					file.WriteLine(formatstart[0] + seite.Formatname + formatend[0]);
					file.WriteLine(breitestart[0] + seite.Breitehoehe + breiteend[0]);
					file.WriteLine(titelstart[0] + seite.Titel + titelend[0]);
					file.WriteLine(inhaltstart[0] + seite.EintragInhalt + string.Empty + inhaltend[0]);
					file.WriteLine(bilderstart[0]);

					/* 
						Erstelle eine Liste mit allen Einträgen fürs Inhaltsverzeichnis. 
						Auch die Seiten ohne Einträgen werden in die Liste aufgenommen:
					*/
					inhaltSaveList.Add(seite.EintragInhalt);

					foreach (AlbumBild bild in seite.Bilderliste)
					{
						string rumpfpfad = bild.Datei;
						file.WriteLine("        <bilddatei>" + rumpfpfad + "</bilddatei>");
					}

					file.WriteLine(bilderend[0]);
					file.WriteLine(klistestart[0]);

					foreach (string komm in seite.Kommentarliste)
					{
						file.WriteLine("        <kommentar>" + komm + "</kommentar>");
					}

					file.WriteLine(klisteend[0]);
					file.WriteLine(seiteend[0]);
				}

				foreach (string line in albumend)
				{
					file.WriteLine(line);
				}
				InhaltSaveList = inhaltSaveList;
			}

			// Sichert 'Default' in 'vorschaumasse.xml' und
			// 'Bildformat' in 'pfade.xml'; beide im Verzeichnis
			// './Baukasten/Werte/':
			string Sep = StartFenster.Sep;
			string filepathrumpf = "." + Sep + "Baukasten" + Sep + "Werte" + Sep;
			string filepath = filepathrumpf + "vorschaumasse.xml";
			XmlNode node = AlbumApp.XmlDocVorschau.SelectSingleNode("/vorschau/groesse");
			node.InnerText = StartFenster.DisplayDefault.ToString();
			Console.WriteLine("Als Wert für die Displaygröße gespeichert: {0}", node.InnerText);
			AlbumApp.XmlDocVorschau.Save(filepath);

			filepath = filepathrumpf + "DefaultWerte.xml";
			XmlDocument xmlDefaultWerte = new();
			xmlDefaultWerte.Load(filepath);
			XmlNode copyimages = xmlDefaultWerte.SelectSingleNode("/DefaultWerte/copyimages");
			XmlNode formatnode = xmlDefaultWerte.SelectSingleNode("/DefaultWerte/bildformat");
			XmlNode langnode = xmlDefaultWerte.SelectSingleNode("/DefaultWerte/lang_default");
			XmlNode drehwinkelnode = xmlDefaultWerte.SelectSingleNode("/DefaultWerte/drehwinkel");
			copyimages.InnerText = StartFenster.CopyImages;
			formatnode.InnerText = StartFenster.Bildformat;
			langnode.InnerText = StartFenster.LangDefault;
			drehwinkelnode.InnerText = StartFenster.Drehwinkel.ToString();
			xmlDefaultWerte.Save(filepath);

			Console.WriteLine("Sichern erfolgreich abgeschlossen");
		}
	}
}
