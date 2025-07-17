using System;
using System.Xml;
using System.Collections.Generic;
using Gtk;
using Startfenster;

namespace AlbumBasis
{
	public partial class AlbumApp : Gtk.Window
	{
		/*
			Das Hauptprogramm und das Menü:
			'Hauptprogramm()' ruft auf:
			  '_ = new AlbumApp()'  ruft auf:
				  'AlbumApp()'
			Die 'Main()'-Methode befindet sich in der Klasse 'StartFenster'.
		*/
		// public static int Seitennr { get; set; }
		public static int[] DieDrehwinkel { get; set; }
		public static string[] Bildformate { set; get; }
		public static Gtk.MenuBar MenueBar { set; get; }
		public static XmlDocument XmlDocVorschau { set; get; }            // 'vorschaumasse.xml'
		static List<Gtk.MenuItem> Sprachenliste { set; get; }
		static AlbumSeite seite = new();
		static Gtk.Fixed layout = [];
		static readonly Gtk.Box vbox = new(Orientation.Vertical, 5);   // Übergeordnete Box;
		static readonly Gtk.Box hbox = new(Orientation.Horizontal, 5); // für das Menü und die Anzeige der Seitenzahl
		static readonly Gtk.Box hbox2 = new(Orientation.Horizontal, 5);// für den Eintrag ins Inhaltsverzeichnis;
		static readonly Gtk.Label seitenlb = new();
		static readonly Gtk.Label filllb = new();
		static readonly Gtk.Label eintraglb = new();
		public static readonly Gtk.Entry eintrag = new();
		static string Gesamtstr = string.Empty;
		public static int Seitenmax { set; get; }         // Index der letzte Seite


		public static void Hauptprogramm()
		{
			// Console.WriteLine("Nun wird das Hauptprogramm gestartet.");
			_ = new AlbumApp();
		}

		// [11]: "Bildauswahl: 'Drag and Drop' od. 'Dateimanager'" :
		public AlbumApp() : base(StartFenster.Localarray[11])
		{
			/* 
	       Initialisiert den Vorschaueditor:
	    */
			AlbumRead.LiesXmlData();                         // gesamtes Album
			Console.WriteLine("albumApp1 meldet: Das Album wurde eingelesen.");
			int gesamt = AlbumRead.Seitenliste.Count; //Seitenzahl
			int startseitenindex = 0;
			int startseitenzahl = startseitenindex + 1;
			StartFenster.Seitennr = startseitenzahl;
			Seitenmax = gesamt - 1;                    // Index der letzten Seite
			seite = AlbumRead.Seitenliste[startseitenindex];
			layout = seite.Layout;                  // Wertzuweisung
			string mystr = gesamt.ToString();
			Gesamtstr = "/" + mystr;
			seitenlb.Text = StartFenster.Localarray[0] + "1/" + gesamt.ToString();//[0]: "Seite "
																																						//Console.WriteLine("Auch diese Zwischenstation wurde erreicht.");
			eintraglb.Text = "Eintrag ins Inhaltsverzeichnis:";                   // Labeltext für das TextEntry: Die Einträge für das Inhaltsverzeichnis
			eintrag.Text = seite.EintragInhalt;
			/*
	      Die Vorgaben für das Vorschaufenster
	    */
			SetDefaultSize(850, 658);
			Resizable = true;
			SetPosition(Gtk.WindowPosition.Center);
			DeleteEvent += OnTerminated;
			AcceptFocus = true;
			OnDefaultActivated();

			Gtk.MenuBar bar = ErzeugeMenue();
			//Console.WriteLine("Das Menü ist geschafft.");
			MenueBar = bar;
			string[] bildformate = { "16x12", "16x09" }; // z.Z. nur diese beiden Formate
			Bildformate = bildformate;
			int[] diedrehwinkel = { 270, 90, 180 };
			DieDrehwinkel = diedrehwinkel;
			int[] dieDisplaygroessen = { 0, 1, 2 };
			//Console.WriteLine("albumApp1: DieDrehwinkel wurde aktualisiert");
			//Console.WriteLine("albumApp1: Bildformate wurde aktualisiert.");
			int formatindex = Array.IndexOf(bildformate, StartFenster.Bildformat);
			int winkelindex = Array.IndexOf(diedrehwinkel, StartFenster.Drehwinkel);
			int displayindex = Array.IndexOf(dieDisplaygroessen, StartFenster.DisplayDefault);
			eintrag.WidthChars = 38; // Bestimmt die Breite des TextEntry;
															 //Console.WriteLine("Der Formatindex lautet: {0}", formatindex);
															 //Console.WriteLine(StartFenster.CheckboxList[0]);
															 //Console.WriteLine("Der Winkelindex lautet: {0} der Drehwinkel beträgt {1}", winkelindex, StartFenster.Drehwinkel);
			eintrag.Changed += new EventHandler(OnEintragChanged);
			StartFenster.FotoformatCheckboxList[formatindex].Toggle(); // setze Checkbox aktiv
			StartFenster.WinkelCheckboxList[winkelindex].Toggle(); // setze Checkbox aktiv
			StartFenster.DisplayCheckboxList[displayindex].Toggle();
			Console.WriteLine("Die Checkbox für das Fotoformat wurde aktiviert.");
			Console.WriteLine("Die Checkbox für den Drehwinkel wurde aktiviert.");

			// Füge alles zusammen:
			hbox.PackStart(bar, false, false, 0);
			hbox.PackStart(filllb, true, true, 0);
			hbox.PackStart(seitenlb, false, false, 10);
			hbox2.PackStart(eintraglb, false, false, 10);
			hbox2.PackStart(eintrag, false, false, 0);
			vbox.PackStart(hbox, false, false, 0);
			vbox.PackStart(hbox2, false, false, 0);
			vbox.PackStart(layout, true, true, 0);

			Add(vbox);
			ShowAll();
		}
	}
}


