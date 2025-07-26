using System;
using Startfenster;
using AlbumBasis;

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
	public class HtmlBild
	{
		/*
			Mit 'Bildbearbeitung()' werden hier in die Html-Vorlage die
			Dateipfade der Bilder und die Pfade zu den Html-Einzelseiten
			eingefügt.
		 */

		/*
			Wir verwenden relative Pfade: Dafür werden die
			absoluten Pfade der Bilddateien in relative umgewandelt.
			Voraussetzung ist, dass der Wert in 'pfade.xml' auf
			'true' gesetzt ist. Ist inzwischen Standard und darf nicht
			geändert werden.
		*/

		/* 
			'Bilderzaehler' liefert für die HtmlSeiten einen Baustein 
			für den Dateinamen der Einzelseiten																		
		  und für den Link zu ihnen.
		*/
		public static int Bilderzaehler { set; get; }
		public static string Pfad { set; get; } // Bilddatei
		public static string Text { set; get; } // unfertige html-Seite

		public static string Bildbearbeitung(AlbumSeite vorschauseite)
		{
			string htmltext = Text;               // unfertige html-Seite
			string refLink = @"../..";            // Teilpfad für Links

			// Die Suchstrings für die Bilder:
			string[] suchstrings = { "qqbild1", "qqbild2", "qqbild3", "qqbild4" };

			// Die Suchstrings für die Links zu den Einzelbildern:
			string[] suchref = { "refbild1", "refbild2", "refbild3", "refbild4" };
			// Die Plattformen:         windows    -   linux   -   osx:
			string[] plattformArray = { @"file:\\\", @"file://", @"file://" };
			string orig = plattformArray[StartFenster.Plattform];
			Console.WriteLine("Protokollteil: {0}", orig);

			/* Bearbeite alle Bilder der Seite: */
			for (int i = 0; i < vorschauseite.Bilderliste.Count; i++) // alle Tags <bilddatei>
			{
				/*
					'pfad' enthält den Dateinamen und den relativen Pfad des
					Bildes.  Wird von 'htmlEinzelseite' benötigt:
				 */

				string pfad = vorschauseite.Bilderliste[i].Datei;
				_ = vorschauseite.Bilderliste[i].BaukastenBild;

				pfad = pfad.Replace("c:", "C:");

				string sep = XMLDoc.Sep;        // "/" od. "\" je nach Plattform

				pfad = Bilderpfad.PfadArbeiten(pfad); /* Berücksichtige die unterschiedlichen Plattformen */

				string zahlenstr = string.Format("{0,4:0000}", Bilderzaehler);
				// Link zum EB:
				string einzelbild = refLink + HtmlBuild.ES_Sichern + zahlenstr + ".html";
				// Speicherpfad für EB:
				string einzelpfad = XMLDoc.FotoalbenPath + sep +
										 XMLDoc.Albumname +
										 HtmlBuild.ES_Sichern +
										 zahlenstr + ".html";

				// setze Bild ein:
				htmltext = htmltext.Replace(suchstrings[i], pfad);
				// setze Link zum EB ein:
				htmltext = htmltext.Replace(suchref[i], einzelbild);
				// setze 'Groesse' als Teil des Links ein:
				htmltext = htmltext.Replace("qqgroesse", HtmlBuild.Groesse);

				Pfad = pfad;                   // Wertzuweisung für die Einzelbilder
				Console.WriteLine("Einzelpfad: {0}", einzelpfad);
				HtmlEinzelseite.Einzelpfad = einzelpfad;
				HtmlEinzelseite.ErstelleEinzelseite();
			}
			return htmltext;
		}
	}
}
