using System.IO;
using System.Collections;
using Startfenster;
using AlbumBasis;

namespace ModuleHtml
{
	public class HtmlSeite
	{
		/*
			Die Klasse 'HtmlSeite' bietet die Methode 'ErzeugeSeite()'.
			Mit Hilfe der Daten der 'vorschauseite' 
			erstellt sie die zugehörige Htmlseite mit Bildern, Kommentaren
			und der Überschrift.
		*/

		public static int Seitenzaehler { set; get; }
		public static int Seitenmax { set; get; }
		public static ICollection Key { set; get; } // key-Sammlung von Ht
		public static Hashtable Ht { set; get; }
		public static string Bildbreite16x12Q { set; get; }
		public static string Bildbreite16x09Q { set; get; }
		public static string Bildbreite16x12H { set; get; }
		public static string Bildbreite16x09H { set; get; }

		/*
			'ErzeugeSeite()' wird aufgerufen von der
			Methode 'HtmlEinlesen()' der Klasse 'HtmlBuild'.
		 */

		public static void ErzeugeSeite(AlbumSeite vorschauseite)
		{
			int seitenzaehler = Seitenzaehler;
			string formatname = vorschauseite.Formatname;
			string htmlVorlage = HtmlBuild.Templatepfad + "/" + vorschauseite.Breitehoehe + "_" + formatname + ".html";
			string htmltext = File.ReadAllText(htmlVorlage);

			// Trägt die Maße für Knöpfe, Rahmen,
			// Bilder und ggf. Openermaße in die Htmlseite ein:
			foreach (string k in Key)
			{
				string ersatz = Ht[k].ToString();
				htmltext = htmltext.Replace(k, ersatz);
			}

			string breitehoehevalue = vorschauseite.Breitehoehe;

			// Der Suchstring für die Bildbreite:
			/* Querformat  */
			string suchbildbreite = "qqimageQ";
			if (breitehoehevalue == "16x12")
				htmltext = htmltext.Replace(suchbildbreite, Bildbreite16x12Q);
			if (breitehoehevalue == "16x09")
				htmltext = htmltext.Replace(suchbildbreite, Bildbreite16x09Q);
			/* Hochformat */
			suchbildbreite = "qqimageH";
			if (breitehoehevalue == "16x12")
				htmltext = htmltext.Replace(suchbildbreite, Bildbreite16x12H);
			if (breitehoehevalue == "16x09")
				htmltext = htmltext.Replace(suchbildbreite, Bildbreite16x09H);

			// Der Suchstring für den Titel:
			string suchtitel = "qqtitel";
			htmltext = htmltext.Replace(suchtitel, XMLDoc.Albumname);

			// Der Suchstring für die Seitenangabe:
			string suchseite = "qqseite";
			string max = Seitenmax.ToString();
			//[0]: "Seite "
			string seitenangabe = StartFenster.Localarray[0] + seitenzaehler + "/" + max;

			htmltext = htmltext.Replace(suchseite, seitenangabe);

			// Die Suchstrings für die Kommentare:
			string[] suchkomm = { "qqkommentar1", "qqkommentar2" };

			// Der Suchstring für die Überschrift:
			string suchueber = "qqueber";

			// Fügt die Überschrift ein:
			htmltext = htmltext.Replace(suchueber, vorschauseite.Titel);

			/*
			Die folgende Methode 'Bildbearbeitung()' fügt die Dateipfade der Bilder 
			in die Htmlseite 'htmltext' ein und erstellt zu jedem Bild 
			eine Einzelseite, damit das Bild auch groß betrachtet werden
			kann. Die Methode gibt die Html-Vorlage 'htmltext' zurück, 
			die weiter bearbeitet werden muss:
			*/
			HtmlBild.Text = htmltext; // Werteübergabe für die Methode 'Bildbearbeitung';
			htmltext = HtmlBild.Bildbearbeitung(vorschauseite);

			/*
			Die html-Vorlage wird mit Suchen und 
			Ersetzen weiter bearbeitet:
			*/
			/* Fügt die Kommentare ein: */
			for (int i = 0; i < vorschauseite.Kommentarliste.Count; i++)
			{
				string komm = vorschauseite.Kommentarliste[i];
				htmltext = htmltext.Replace(suchkomm[i], komm);
			}

			/* Fügt die Navigation ein: */
			int back = seitenzaehler - 1;                   // eine Seite zurück
			if (back == 0)                                  // Sonderfall: Seite 1
			{
				back = Seitenmax;
			}

			string zahlenstr = string.Format("{0,4:0000}", back);
			htmltext = htmltext.Replace("qqback", zahlenstr); // Seite zurück

			int vor = seitenzaehler + 1;                      // eine Seite vor 
			if (vor == Seitenmax + 1)                         // Sonderfall:
			{                                                 // max erreicht!
				vor = 1;
			}
			zahlenstr = string.Format("{0,4:0000}", vor);
			htmltext = htmltext.Replace("qqvor", zahlenstr);  // Seite vor

			// Aus doppelten Anführungeszeichen werden einfache: 
			htmltext = htmltext.Replace("\"\"", "\"");

			// Speichert die fertige html-Seite:
			string endung = string.Format("{0,4:0000}", seitenzaehler);
			string dateipfad = StartFenster.AlbumnamePath + HtmlBuild.AS_Sichern + endung + ".html";
			System.IO.File.WriteAllText(dateipfad, htmltext, System.Text.Encoding.UTF8);
			seitenzaehler++;
			Seitenzaehler = seitenzaehler;
		}
	}
}
