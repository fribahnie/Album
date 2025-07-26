using Startfenster;

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
	public class HtmlEinzelseite
	{
		/*
			Erstellt die html-Einzelseite mit dem Bild im
			Großformat. Wird aufgerufen von HtmlBild.
		*/
		public static int Bildermax { set; get; }
		public static string HtmlEinzVorl { set; get; }
		public static string Einzelpfad { set; get; }
		public static string Knopfbreite { set; get; }
		public static string Conthoehe { set; get; }

		public static void ErstelleEinzelseite()
		{
			int bildzaehler = HtmlBild.Bilderzaehler;

			int backzahl = bildzaehler - 1;               // für ein Bild zurück
			if (backzahl == 0)                            // Sonderfall: 1. Bild
			{
				backzahl = Bildermax;                       // Sprung zur letzten Seite
			}

			int vorzahl = bildzaehler + 1;                // für ein Bild vor

			if (vorzahl == Bildermax + 1)                 // Sonderfall: letztes Bild
			{
				vorzahl = 1;                                // Sprung zur ersten Seite
			}
			/*
  			strback, strnext und zahlenstr werden erstellt.
  			String.Format sorgt dafür, dass die Htmlseiten
  			der Reihe nach im Ordner aufgelistet werden.
			*/
			string zahlenstr = string.Format("{0,4:0000}", backzahl);
			string strback = "einzel" + zahlenstr + ".html";
			zahlenstr = string.Format("{0,4:0000}", vorzahl);
			string strnext = "einzel" + zahlenstr + ".html";
			zahlenstr = string.Format("{0,4:0000}", HtmlSeite.Seitenzaehler);

			/*
  			Der Pfad für den Link von der Einzelbild-Htmlseite
  			wieder zurück zum Album:
			*/
			string albumpfad = "../.." + "/Albumseiten/" + HtmlBuild.Groesse + "/seite" +
													zahlenstr + ".html";

			/*
	      Die Vorlage für Einzelseiten wird mit Suchen
	      und Ersetzen bearbeitet:
	    */
			string seitenangabe = "Seite " + bildzaehler + "/" + Bildermax;
			string etext = HtmlEinzVorl;
			etext = etext.Replace("qqtitel", XMLDoc.Albumname);
			etext = etext.Replace("qqgroesse", HtmlBuild.Groesse); // Größenauswahl
			etext = etext.Replace("qqbild", HtmlBild.Pfad);        // Bilddateipfad
			etext = etext.Replace("qq_bbreite", Conthoehe);        // Containerhöhe
			etext = etext.Replace("qqknopf", Knopfbreite);         // Knopfbreite
			etext = etext.Replace("qqlinkback", strback);          // Link S. zurück
			etext = etext.Replace("qqlinknext", strnext);          // Link S. vor
			etext = etext.Replace("qqlinkalbum", albumpfad);       // zur Albumseite
			etext = etext.Replace("qqseite", seitenangabe);        // Seitenangabe

			/*
	      Die fertige Einzelseite wird gespeichert,
	      ebenso der Bildzähler für den nächsten Aufruf.
	    */
			System.IO.File.WriteAllText(Einzelpfad, etext, System.Text.Encoding.UTF8);
			bildzaehler++;
			HtmlBild.Bilderzaehler = bildzaehler;
		}
	}
}
