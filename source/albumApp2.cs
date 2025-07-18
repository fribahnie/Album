using System;
using System.Xml;
using Startfenster;

namespace AlbumBasis
{
	public partial class AlbumApp : Gtk.Window
	{
		/*
			Verarbeitet die verschiedenen Events aus dem Menü des 
			Vorschaueditors:

				'OnBlaetternVor()'
				'OnBlaetternRueck()'
				'OnSeiteNeu()'
				'OnLoeschen()'
				'OnDisplayGroesse()'
				'OnSprachauswahl'
				'OnBuffer0Changed()' // Textbuffer0
				'OnBuffer1Changed()' // Textbuffer1
				'OnEntryChanged()    // Die Überschrift einer Seite
		*/

		void OnBlaetternVor(object sender, EventArgs args)
		{
			Blaettern(true);
		}

		void OnBlaetternRueck(object sender, EventArgs args)
		{
			Blaettern(false);
		}

		void Blaettern(bool arg)
		{
			int index = AlbumRead.Seitenliste.IndexOf(seite);

			if (arg)
				index = index < Seitenmax ? ++index : 0;           // Vorwärtsblättern: prüft, ob letzte Seite
			else
				index = index > 0 ? --index : Seitenmax;           // Rückwärtsblättern: prüft, ob erste Seite

			StartFenster.Seitennr = index + 1;                                // für 'seitenlb.Text'
			/* Gesamt   = Seitenmax + 1; */                      // für 'seitenlb.Text'
			seite = AlbumRead.Seitenliste[index];
			AlbumBuildSeite.SeitenListenIndex = index;
			vbox.Remove(layout);                                 // entfernt das alte Layout
			layout = seite.Layout;                               // erhält das aktuelle Layout
			seitenlb.Text = StartFenster.Localarray[0] + StartFenster.Seitennr + Gesamtstr;   // Text für Seitelabel
			eintrag.Text = seite.EintragInhalt;
			var buffer1 = seite.BufferList[0];
			var buffer2 = seite.BufferList[1];

			buffer1.Text = seite.Kommentarliste[0];
			buffer2.Text = seite.Kommentarliste[1];
			AlbumNeuesBild.Seite = seite;                       // aktualisiert die Seite für 'Drag and Drop'
			Console.WriteLine("Kommentare: {0} {1}", buffer1.Text, buffer2.Text);
			vbox.PackStart(layout, true, true, 0);               // verwendet das aktuelle L.
			vbox.Show();
			ShowAll();
		}

		public static void OnSeiteNeu(object sender, EventArgs args)
		{
			int aktuell = AlbumRead.Seitenliste.IndexOf(seite); /* Index akt. Seite */
			vbox.Remove(layout);                                 /* entfernt das alte Layout */

			/* Typumwandlung: Wer sendet? 'senderindex' –> was wurde geklickt?: */
			Gtk.MenuItem testitem = (Gtk.MenuItem)sender;
			int senderindex = StartFenster.FormateList.IndexOf(testitem);
			AlbumBuildSeite.SeitenListenIndex = aktuell + 1;
			Console.WriteLine("Als Index der Seite wird geliefert: {0}", AlbumBuildSeite.SeitenListenIndex);
			/* erstellt das Layout der neuen Seite: */
			seite = AlbumSeite.NeueSeite(senderindex);        /* 'senderindex' liefert 'formatnr' */
			AlbumRead.Seitenliste.Insert(aktuell + 1, seite); /* integriert die neue Seite */
			Aktualisieren(aktuell, 2);
			layout = seite.Layout;                            /* verwendet das neue Layout */
			vbox.PackStart(layout, true, true, 0);
			vbox.Show();
			vbox.ShowAll();
		}

		void OnLoeschen(object sender, EventArgs args)
		{
			int aktuell = AlbumRead.Seitenliste.IndexOf(seite);/* Index der aktuellen Seite */
			OnBlaetternVor(sender, args);
			AlbumRead.Seitenliste.RemoveAt(aktuell);
			aktuell = aktuell == Seitenmax ? 0 : aktuell;     /* letzte Seite löschen */
			Aktualisieren(aktuell, 1);
		}


		static void Aktualisieren(int aktuell, int arg)
		{
			Seitenmax = AlbumRead.Seitenliste.Count - 1;      /*  aktualisiert Seitenmax */
			Gesamtstr = "/" + (Seitenmax + 1).ToString();
			StartFenster.Seitennr = aktuell + arg;
			seitenlb.Text = StartFenster.Localarray[0] + StartFenster.Seitennr + Gesamtstr; /* Labeltext */
		}


		void OnSprachauswahl(object sender, EventArgs args)
		{
			Gtk.MenuItem testitem = (Gtk.MenuItem)sender;     /* Typumwandlung: Wer sendet? */
			int zwischenwert = Sprachenliste.IndexOf(testitem); /* der Index verrät es. */
			string[] sprache = { "local_en.txt", "local_de.txt" };

			/* Sichern in 'pfade.xml': */
			XmlDocument xmlpfade = new();
			string Sep = XMLDoc.Sep;
			string filepath = "." + Sep + "Baukasten" + Sep + "Werte" + Sep + "pfade.xml";
			xmlpfade.Load(filepath);
			XmlNode lang_default = xmlpfade.SelectSingleNode("/pfade/lang_default");
			lang_default.InnerText = sprache[zwischenwert];
			xmlpfade.Save(filepath);
		}


		/* Wenn die Überschrift verändert wird: */
		public static void OnEntryChanged(object obj, EventArgs args)
		{
			Gtk.Entry entry = (Gtk.Entry)obj;
			seite.Titel = entry.GetChars(0, -1);              /* speichert die Überschrift */
		}

		/* Wenn der Eintrag für das Inhaltsverzeichnis geändert wird: */
		public static void OnEintragChanged(object obj, EventArgs args)
		{
			Gtk.Entry eintrag = (Gtk.Entry)obj;
			seite.EintragInhalt = eintrag.GetChars(0, -1);    /* speichert den Eintrag im Inhaltsverzeichnis */
		}


		/*
			Der geänderte Text wird gespeichert:
		*/
		public static void OnBuffer0Changed(object obj, EventArgs args)
		{
			Gtk.TextBuffer buffer = (Gtk.TextBuffer)obj;
			seite.Kommentarliste[0] = buffer.Text;
		}

		public static void OnBuffer1Changed(object obj, EventArgs args)
		{
			Gtk.TextBuffer buffer = (Gtk.TextBuffer)obj;
			seite.Kommentarliste[1] = buffer.Text;
		}


		void OnTerminated(object sender, EventArgs args)
		{
			Gtk.Application.Quit();
		}
	}
}

