using System;
using System.Xml;
using System.Collections.Generic;
using Gtk;
using Startfenster;
using BasicClasses;

namespace AlbumBasis
{
	/*
		Das XmlDocument 'xmlseite' wird hier eingelesen und
		ausgewertet.  Die Methode 'BuildSeite' baut daraus eine komplette
		Albumseite zusammen.  Sie greift dabei mit der Methode
		'HoleEckpunkte' auf die Formate zurück, die von 'AlbumFormate'
		bereitgestellt werden. Die Methode stellt das Feld
		'AlbumFormate.VorschauSeite' mit den Eckpunkten für die Bilder
		und die TextViews, 'Bilderzahl' und den String 'Ausrichtung' bereit. 
	*/
	public static class AlbumBuildSeite
	{
		public static string HomeBilder { set; get; }
		public static string Hintergrundimagepfad { set; get; }
		public static Gtk.Window WinTv { set; get; }
		public static Gtk.Window WinTv1 { set; get; }
		public static Gtk.Window WinTv2 { set; get; }
		public static Gtk.Button WinBtn1 { set; get; }
		public static Gtk.Button WinBtn2 { set; get; }
		public static Gtk.Button FixBtn1 { set; get; }
		public static Gtk.Button FixBtn2 { set; get; }
		public static int SeitenListenIndex { set; get; }
		public static List<string> LocalKommList { set; get; }
		public static bool Toggle1 { set; get; }
		public static bool Toggle2 { set; get; }
		public static bool[] ToggleArray { set; get; }

		public static AlbumSeite BuildSeite(XmlDocument xmlseite,
								 string format,
								 string breitehoehe,
								 string titel,
								 string inhalt)
		{
			Hintergrundimagepfad = StartFenster.AlbumRootPath +
				"Baukasten" + StartFenster.Sep +
				"RahmenBilder" + StartFenster.Sep +
				"Hintergrund.png";                    // Pfad für den farbigen Hintergrund mit dem Rahmen
			Console.WriteLine("Der Hintergrundpfad: {0}", Hintergrundimagepfad);
			XmlNodeList xnDateiList;                 // NodeList der Bilder
			XmlNodeList kommList;                    // NodeList der Kommentare

			AlbumSeite seite = new();                // eine neue Vorschauseite
			Gtk.TextBuffer buffer1 = new(new TextTagTable());
			Gtk.TextBuffer buffer2 = new(new TextTagTable());

			Gtk.Fixed layout = new();
			Gtk.Entry entry = new();
			List<AlbumBild> bilderliste = new();     // die Bilder
			List<Gtk.EventBox> eboxliste = new();    // die eboxes
			List<Gtk.TextBuffer> bufferlist = new(); // fürs Sichern
			List<Gtk.Window> wintvList = new();
			Gtk.Image hintergrundimage = new(Hintergrundimagepfad);
			layout.Put(hintergrundimage, 11, 41);

			seite.Formatname = format;               // Wertzuweisung aus Methodenaufruf von BuildSeite
			seite.Breitehoehe = breitehoehe;         // Wertzuweisung aus Methodenaufruf von BuildSeite
			seite.Titel = titel;                     // Wertzuweisung aus Methodenaufruf von BuildSeite
			seite.EintragInhalt = inhalt;            // Wertzuweisung vom Einlesen

			// Console.WriteLine("Bis hierher ging alles gut!");

			// Liest die Eckpunkte der Seite ein -
			// Bildereckpunkte und TextVieweckpunkte:

			AlbumFormate.HoleEckpunkte(format);
			List<AlbumPunkt> bilderEckpunkte = AlbumFormate.VorschauSeite.B_Ecken;
			List<AlbumPunkt> tviewEckpunkte = AlbumFormate.VorschauSeite.T_Ecken;
			string ausrichtung = AlbumFormate.VorschauSeite.Ausrichtung;

			// erzeugt die NodeList der Bilder:
			xnDateiList = xmlseite.SelectNodes("seite/bilder/bilddatei");
			int zaehler = 0;
			string bilddatei = string.Empty;
			foreach (XmlNode xn in xnDateiList)  // wertet alle Bilddateien aus
			{                                    // erzeugt und platziert die
				AlbumBild bild = new();            // Vorschaubilder;
				string relativbilddatei = xn.InnerText;   // die einzelne Bilddatei
				if (relativbilddatei[0].Equals(".")) // wenn es sich um ein Bild im 'Baukasten' handelt.
				{
					bilddatei = HomeBilder + relativbilddatei; // 'HomeBilder' kommt von 'AlbumBasics'
				}
				else
				{
					bilddatei = relativbilddatei;
				}
				Console.WriteLine("Die Bilddatei für die weitere Verarbeitung: {0}", bilddatei);
				string richtung = ausrichtung[zaehler].ToString();
				bild.Richtung = richtung;

				// Skaliert das Original zum Vorschaubild:
				using Gtk.Image image = AlbumNeuesBild.SkaliereBild(bilddatei, richtung);
				seite = AlbumNeuesBild.SetzeBildEin(seite,
							 image,
							 bilderEckpunkte,
							 zaehler,
							 layout,
							 eboxliste);
				bild.Datei = bilddatei;                    // Wertzuweisung
				bild.Richtung = richtung;
				using (bild.Bild = image)                  // Wertzuweisung
				{
					bild.EckPunkt = bilderEckpunkte[zaehler];
					bilderliste.Add(bild);                   // die Bilder einer Seite
					zaehler++;
				}
			}
			;
			seite.Eboxliste = eboxliste;                   // Wertezuweisung

			//AlbumRead.Seitenliste[seitenListeIndex] = seite;
			// erzeugt die NodeList der Kommentare:
			kommList = xmlseite.SelectNodes("seite/kommentarliste/kommentar");
			List<string> dieKommentare = new();
			for (int index = 0; index <= 1; index++)
			{
				XmlNode komm = kommList[index];
				string kommentar = komm.InnerText;           // der einzelne Kommentar
				dieKommentare.Add(kommentar);
			}
			LocalKommList = dieKommentare;
			bufferlist.Add(buffer1);
			bufferlist.Add(buffer2);

			// Die beiden neuen Windows mit den TextViews:
			WinBtn1 = TextfeldFenster(0, LocalKommList, bufferlist);
			WinBtn2 = TextfeldFenster(1, LocalKommList, bufferlist);
			wintvList.Add(WinTv1);
			wintvList.Add(WinTv2);

			WinBtn1.Clicked += OnWinBtn1Clicked;
			WinBtn2.Clicked += OnWinBtn2Clicked;

			// Die beiden Buttons im Vorschaueditor:
			bool toggle1 = true;
			bool toggle2 = true;
			bool[] toggleArray = { toggle1, toggle2 };
			Toggle1 = toggle1;
			Toggle2 = toggle2;
			ToggleArray = toggleArray;
			FixBtn1 = CreateFixButton(0, layout, tviewEckpunkte);
			FixBtn2 = CreateFixButton(1, layout, tviewEckpunkte);

			FixBtn1.Clicked += OnFixBtn1Clicked;
			FixBtn2.Clicked += OnFixBtn2Clicked;

			buffer1.Changed += new EventHandler(AlbumApp.OnBuffer0Changed);
			buffer2.Changed += new EventHandler(AlbumApp.OnBuffer1Changed);

			// Die Seitenüberschrift:
			entry.Text = seite.Titel;                      // Liest die Überschrift ein
			entry.WidthChars = 61;                         // die angezeigte Breite in Chars
			entry.MaxLength = 850;
			entry.Xalign = 0.0f;                           // platziert den Text links
			layout.Put(entry, 10, 0);                      // Platziert die Entry
			entry.Changed += new EventHandler(AlbumApp.OnEntryChanged);

			seite = AlbumNeuesBild.SetzeHandler(seite);

			seite.WinTVList = wintvList;
			seite.BufferList = bufferlist;
			seite.Bilderliste = bilderliste;               // Wertzuweisung
			seite.Kommentarliste = LocalKommList;          // Wertzuweisung
			seite.Layout = layout;                         // Wertzuweisung
			return seite;                                  // die Seite ist fertig
		}

		public static Gtk.Button TextfeldFenster(
			int index, List<string> kommList, List<Gtk.TextBuffer> bufferlist)
		{
			string[] Fenstertitel = { "Textfeld 1", "Textfeld 2" }; // abhängig von 'index'
			string kommentar = kommList[index];          // der einzelne Kommentar          
			bufferlist[index].Text = kommentar;          // Wertzuweisung an buffer für Textviews

			// Das Fenster:
			WinTv = new(Fenstertitel[index]);
			WinTv.SetDefaultSize(400, 300);
			Gtk.Box boxWin = new(Orientation.Vertical, 10);
			Gtk.TextView textView = new(bufferlist[index]);
			textView.WrapMode = (WrapMode)2;
			Gtk.Button winbtn = new("Hide");
			boxWin.PackStart(textView, true, true, 5);
			boxWin.PackStart(winbtn, false, false, 5);
			WinTv.Add(boxWin);
			if (index == 0)
			{
				WinTv1 = WinTv;
				LocalKommList[0] = kommentar;
			}
			if (index == 1)
			{
				WinTv2 = WinTv;
				LocalKommList[1] = kommentar;
			}
			return winbtn;
		}

		public static Gtk.Button CreateFixButton(int index, Gtk.Fixed layout, List<AlbumPunkt> tviewEckpunkte)
		{
			Gtk.Label label = new("Show/Hide");
			// Ein Button für das Vorschaufenster:
			Gtk.Button fixbtn = new(label);
			Gtk.Box boxbtn = new(Orientation.Vertical, 0)
			{
				fixbtn
			};
			layout.Put(boxbtn, tviewEckpunkte[index].XWert, tviewEckpunkte[index].YWert);
			return fixbtn;
		}

		public static void OnFixBtn1Clicked(object sender, EventArgs a)
		{
			ShowHide(SeitenListenIndex, 0);
		}

		public static void OnFixBtn2Clicked(object sender, EventArgs a)
		{
			ShowHide(SeitenListenIndex, 1);
		}

		static void ShowHide(int SeitenListenIndex, int nr)
		{
			var seite = AlbumRead.Seitenliste[SeitenListenIndex];
			if (ToggleArray[nr] == true)
			{
				seite.WinTVList[nr].ShowAll();
				ToggleArray[nr] = false;
			}
			else
			{
				seite.WinTVList[nr].Hide();
				ToggleArray[nr] = true;
			}
		}

		public static void OnWinBtn1Clicked(object sender, EventArgs a)
		{
			Hide(0);
		}
		public static void OnWinBtn2Clicked(object sender, EventArgs a)
		{
			Hide(1);
		}

		static void Hide(int nr)
		{
			var seite = AlbumRead.Seitenliste[SeitenListenIndex];
			if (!ToggleArray[nr])
			{
				seite.WinTVList[nr].Hide();
				ToggleArray[nr] = true;
			}
		}
	}
}
