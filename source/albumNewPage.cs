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
	  Die Klasse 'AlbumSeite' enthält die Methode 'NeueSeite()' mit 
		weiteren davon abhängigen Methoden.
	  'NeueSeite()' legt für das Album eine neue Vorschauseite an.  
		Die Methode wird aufgerufen von der Klasse 'AlbumApp' in 
		'albumApp2.cs'.
	  Mit der Methode 'Formenname()' der Klasse 'AlbumFormennamen'
	  wird jedem Wert von 'senderindex' ein 'formenname'
	  zugeordnet.
	  'formenname' bestimmt das konkrete Layout für die Vorschauseite.
	  Das Layout wird definiert durch den Aufruf von
	  'AlbumFormate.HoleEckpunkte(formenname)'.
*/

using System;
using System.IO;
using System.Collections.Generic;
using Startfenster;
using BasicClasses;

namespace AlbumBasis
{
	public class AlbumSeite                                    // die einzelne Seite
	{
		public int Senderindex { set; get; } // identifiziert den Menüpunkt
		public string Formatname { set; get; } // dient zum Sichern der Albumseite
		public string Breitehoehe { set; get; } // Seitenverhältnis der Bilder
		public string Titel { set; get; } // Seitenüberschrift
		public string EintragInhalt { set; get; } // Der Text für das Inhaltsverzeichnis
		public Gtk.Fixed Layout { set; get; } // erlaubt die Platzierung
		public List<Gtk.EventBox> Eboxliste { set; get; } // nötig für Events
		public List<string> Kommentarliste { set; get; } // Texte der Seite
		public List<AlbumBild> Bilderliste { set; get; } // alle Bilder der Seite
		public List<Gtk.TextBuffer> BufferList { set; get; }
		static Gtk.Window WinTV { set; get; }
		static Gtk.Window WinTV1 { set; get; }
		static Gtk.Window WinTV2 { set; get; }
		public List<Gtk.Window> WinTVList { set; get; }



		public static AlbumSeite NeueSeite(int senderindex)
		{
			/*
	      Legt, je nach Menüpunkt, eine neue Vorschauseite an für 
				Bilder entweder im 16x12 oder im 16x9 Format. 
				Zunächst werden Defaultbilder angezeigt, die dann 
				durch Drag and Drop durch die endgültigen Bilder ersetzt 
				werden müssen. Der gewählte Menüpunkt für die 16 
				unterschiedlichen Formen wird mit 'senderindex' 
				identifiziert.
	    */

			/* Pfad zu den Default-Bildern und die Dateinamen der Bilder: */
			string mypfad = "./Baukasten/DefaultBilder/";
			string[] myarray = {
				"P545.JPG",       "P547.JPG",       "P548.JPG",       "P559.JPG",      // 16x12 Querformat
				"Q16x09_1.JPG",   "Q16x09_2.JPG",   "Q16x09_3.JPG",   "Q16x09_4.JPG",  // 16x09 Querformat
				"HP1210037.JPG",  "HP1210118.JPG",  "HP1210134.JPG",  "HP1210155.JPG", // 12x16 Hochformat
				"HQIMG_1525.jpg", "HQIMG_1526.jpg", "HQIMG_1528.jpg", "HQIMG_1534.jpg" // 09x16 Hochformat
	    };

			int myindex = -1; /* myindex zeigt das gewählte Bildformat: 0 = "16x12" od. 1 = "16x09"; */
			foreach (Gtk.CheckMenuItem eintrag in StartFenster.FotoformatCheckboxList)
			{
				if (eintrag.Active)
				{
					myindex = StartFenster.FotoformatCheckboxList.IndexOf(eintrag);
				}
			}

			/* Die neue Vorschauseite: */

			AlbumSeite seite = new();

			/*
	      Die Vorschauseite unterscheiden sich optisch nur
	      marginal bei den Bildformaten 4x3 oder 16x9,
	      nämlich nur in der Breite bzw. Höhe der
	      Vorschaubilder. Für beide Bildformate wird aus 
				Gründen der Vereinfachung im Vorschaueditor 
				dasselbe Layout verwendet.
	    */

			string breitehoehe = AlbumApp.Bildformate[myindex];
			string formenname = AlbumFormennamen.Formenname(senderindex); /* werte dictionary 'formennamen' aus */
			seite.Breitehoehe = breitehoehe;
			XMLDoc.Bildformat = breitehoehe; /* wird als Defaultwert gesichert */

			List<AlbumPunkt> bilderEckpunkte = [];
			List<AlbumPunkt> tviewEckpunkte = [];
			List<Gtk.EventBox> eboxliste = [];
			List<AlbumBild> bilderliste = [];       // die neue Bilderliste
			List<string> LocalKommList = [];       // die neue Kommentarliste
			Gtk.Entry entry = new();                   // Seitenüberschrift
			Gtk.TextBuffer buffer1 = new(new Gtk.TextTagTable());
			Gtk.TextBuffer buffer2 = new(new Gtk.TextTagTable());
			List<Gtk.TextBuffer> bufferlist = [buffer1, buffer2];
			Gtk.TextView tview1 = new(buffer1);        // die beiden Textfelder
			Gtk.TextView tview2 = new(buffer2);
			Gtk.Fixed layout = [];                  // erlaubt Platzierung
			Gtk.Image hintergrundimage = new(AlbumBuildSeite.Hintergrundimagepfad);
			layout.Put(hintergrundimage, 11, 41);

			//[34]: "Erstes Textfeld"                             // Default-Text
			buffer1.Text = StartFenster.Localarray[34];
			LocalKommList.Add(buffer1.Text);

			//[35]: "Zweites Textfeld"                            // Default-Text
			buffer2.Text = StartFenster.Localarray[35];           // Default-Text
			LocalKommList.Add(buffer2.Text);
			buffer1.Changed += new EventHandler(AlbumApp.OnBuffer0Changed);
			buffer2.Changed += new EventHandler(AlbumApp.OnBuffer1Changed);
			seite.Senderindex = senderindex;        /* Parameterübergabe für HtmlSeite */
			seite.Formatname = formenname;

			/* holt die Eckpunkte der Bilder und der TextViews anhand von 'formenname': */
			AlbumFormate.HoleEckpunkte(formenname);
			bilderEckpunkte = AlbumFormate.VorschauSeite.B_Ecken;
			tviewEckpunkte = AlbumFormate.VorschauSeite.T_Ecken;

			/*
	      'bilderzahl': die Anzahl der Bilder, die
	     dargestellt werden sollen.  Abrufbar sind bis
	     zu vier Bilder; mit jedem Schleifendurchlauf
	     wird ein Dateipfad ausgelesen.
	    */

			int bilderzahl = AlbumFormate.VorschauSeite.Bilderzahl;
			Console.WriteLine("Die 'bilderzahl' lautet {0}", bilderzahl);
			string ausrichtung = AlbumFormate.VorschauSeite.Ausrichtung;
			int korrekturwert = 0; // später: Querformat ( = 0) oder Hochkant ( = 8)

			/*
	      In 'myarray' sind die Dateinamen der
	      Default-Bilder gespeichert. 8 Bilder im Format 16x12 und 8 Bilder 16x09;
	      deswegen beträgt der mögliche 'korrekturwert' 8;
	     */

			/*
				Untersuche anhand des Strings 'ausrichhtung' für 
				jedes der Bilder einer Vorschauseite, ob das  
			  Bild im Hochformat oder Querformat ausgegeben werden soll.
				Der Korrekturwert (0 oder 8) sorgt dann dafür, dass aus den
				Default-Bildern ein Bild im Querformat oder Hochformat 
				ausgewählt und angezeigt wird. 
			*/
			for (int i = 0; i < bilderzahl; i++)
			{
				if (ausrichtung[i].CompareTo('H') == 0)
				{
					korrekturwert = 8; /* = Hochformat */
				}

				if (ausrichtung[i].CompareTo('Q') == 0)
				{
					korrekturwert = 0; /* = Querformat */
				}

				AlbumBild bild = new();
				string datei = mypfad + myarray[(4 * myindex) + i + korrekturwert];
				bild.Richtung = (korrekturwert == 8) ? "H" : "Q";
				bild.Datei = datei;
				Gtk.Image image = new();
				try
				{
					image = AlbumNeuesBild.SkaliereBild(datei, bild.Richtung);
					seite = AlbumNeuesBild.SetzeBildEin(seite, image, bilderEckpunkte, i, layout, eboxliste);
					bild.Bild = image;                  // Wertzuweisung zum Speichern
					bild.EckPunkt = bilderEckpunkte[i]; // Wertzuweisung zum Speichern
					bilderliste.Add(bild);
				}
				catch (IOException e)
				{
					Console.WriteLine(e.Message + "Kann die Bilddatei nicht einlesen.");
				}
				bild.Datei = datei;
			}

			// Create Buttons. Sie rufen die neuen Windows auf:
			var FixBtn1 = AlbumBuildSeite.CreateFixButton(0, layout, tviewEckpunkte);
			var FixBtn2 = AlbumBuildSeite.CreateFixButton(1, layout, tviewEckpunkte);
			FixBtn1.Clicked += AlbumBuildSeite.OnFixBtn1Clicked;
			FixBtn2.Clicked += AlbumBuildSeite.OnFixBtn2Clicked;
			// Die beiden neuen Windows mit den TextViews:
			var WinBtn1 = TextfeldFenster(0, bufferlist);
			var WinBtn2 = TextfeldFenster(1, bufferlist);
			WinBtn1.Clicked += AlbumBuildSeite.OnWinBtn1Clicked;
			WinBtn2.Clicked += AlbumBuildSeite.OnWinBtn2Clicked;
			List<Gtk.Window> LocalWinTVList =
			[
				WinTV1,
				WinTV2
			];

			/* [36]: "Deine Überschrift";                        Default-Überschrift            */
			entry.Text = StartFenster.Localarray[36];
			entry.WidthChars = 62;                            /* die angezeigte Breite in Chars */
			entry.Xalign = 0.5f;                              /* platziert den Text mittig      */
			layout.Put(entry, 10, 0);                         /* Platziert die Entry oben       */
			layout.ShowAll();

			//seite.EintragInhalt = AlbumApp.eintrag.Text;      /* Der Text der Albumseite für das Inhaltsverzeichnis */
			AlbumApp.eintrag.Text = string.Empty;
			seite.Kommentarliste = LocalKommList;
			entry.Changed += new EventHandler(AlbumApp.OnEntryChanged);
			seite.Eboxliste = eboxliste;
			seite.Bilderliste = bilderliste;
			seite.WinTVList = LocalWinTVList;
			seite.BufferList = bufferlist;
			seite.Layout = layout;
			seite = AlbumNeuesBild.SetzeHandler(seite);
			seite.Titel = entry.Text;
			return seite;
		}

		public static Gtk.Button TextfeldFenster(
			int index, List<Gtk.TextBuffer> bufferlist)
		{
			string[] Fenstertitel = { "Textfeld 1", "Textfeld 2" }; // abhängig von 'index'
																															// Das Fenster:
			WinTV = new(Fenstertitel[index]);
			WinTV.SetDefaultSize(400, 300);
			Gtk.Box boxWin = new(Gtk.Orientation.Vertical, 10);
			Gtk.TextView textView = new(bufferlist[index]);
			Gtk.Button winbtn = new("Hide");
			boxWin.PackStart(textView, true, true, 5);
			boxWin.PackStart(winbtn, false, false, 5);
			WinTV.Add(boxWin);
			if (index == 0) { WinTV1 = WinTV; }
			if (index == 1) { WinTV2 = WinTV; }
			return winbtn;
		}
	}
}
