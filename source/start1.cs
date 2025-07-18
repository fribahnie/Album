using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Gtk;
using ModulePlattform;
using ModuleSprachen;
using Startfenster;

namespace Startfenster
{
	public partial class StartFenster
	{
		/*
			Das gesamte Programm umfasst drei Teile: 

			1. Eben dieses StartFenster mit 'Main'. Es liefert ein
				 Auswahlfenster vor dem eigentlichen Programmstart.
				 Der Nutzer wählt hier u.a., welches Fotoalbum bearbeitet
				 werden soll. Darauf startet der VorschauEditor.
				 namespace 'StartFenster';
			2. Der VorschauEditor ermöglicht die Auswahl unter 16
				 Layouts für die Albumseite.  Er unterstützt die
				 Bildformate 16x12 und 16x09.  Per Drag and Drop
				 können die Defaultbilder des Vorschaueditors
				 durch die gewünschten Bilder ersetzt werden.
				 Die Seite kann mit einer Überschrift und zwei
				 Textfeldern beschriftet werden.
				 Die Source-Dateien dafür beginnen mit 'album…'.
				 namespace 'VorschauEditor';
			3. Der Programmteil, der die Datei 'albumdaten.xml' 
				 auswertet und daraus html-Seiten generiert.
				 Die Source-Dateien dafür beginnen mit 'html…'.
				 namespace 'ModuleHtml';

			4. Die Pfadangaben:
				 .../Album/Fotoalben/<Albumname>/Albumseiten/vierk/seite0001.html
				 ––––––––> AlbumRootPath
				 ––––––––––––––––––> FotoalbenPath
				 –––––––––––––––––––––––––––––––> AlbumnamePath
			5. Die Größenangaben:
					"mittel" = ; gross = 1K = 1920x1080; vierk = 4K = 3840x2160
					Am intensivsten habe ich 4K getestet.		
			6. Die Ausgestaltung der Html-Seiten übernehmen css-Dateien. Sie 
					werden von dem Ruby-Programm 'tausendsassa' generiert. Zu finden
					in dem Verzeichnis 'Create-CSS'. Gespeichert sind die css-Dateien
					unter 'Baukasten/css-Dateien'.	
			7. Die Methoden dieser 'partial class' von StartFenster:

					'main()' liest ein:
						'BestimmePlattform()'
				  	'AlbumBasics.RelativePfade()'
						'LiesDefaultWerteEin()'	
						'SpracheLaden(LangDefault)'	
						'Application.Init()'
		 */
		public static string[] Bereiche { set; get; }// { "mittel", "gross", "vierk" };
		public static string AlbumnamePath { set; get; }// Pfad zu den Dateien des Fotoalbums
		public static int DisplayDefault { set; get; }// Größe für die html-Ausgabe
		public static int Plattform { set; get; }// 0 = win, 1 = linux, 2 = OSX
		public static string Rel { set; get; }// "./" in relativen Dateipfaden
		public static string Startpfad { set; get; }// Pfadabschnitt zum Starterset
		public static string[] Localarray { set; get; }
		public static int[] DrehwinkelArray { set; get; }
		public static int StartIndex { set; get; }
		public static int Seitennr { set; get; }
		public static List<Gtk.CheckMenuItem> FotoformatCheckboxList { get; set; }
		public static List<Gtk.CheckMenuItem> WinkelCheckboxList { get; set; }
		public static List<Gtk.CheckMenuItem> DisplayCheckboxList { get; set; }
		public static List<Gtk.CheckMenuItem> CopyCheckboxList { get; set; }
		public static List<Gtk.MenuItem> FormateList { get; set; }

		public static Gtk.Window myWin;        // public: wird später von 'AlbumRead()' gelöscht 
		static Gtk.Entry entry1;
		static Gtk.Label myLabel;
		static Gtk.RadioButton radiobutton1;
		static Gtk.RadioButton radiobutton2;
		static Gtk.RadioButton radiobutton3;
		static Gtk.RadioButton radiobutton4;
		static Gtk.RadioButton radiobutton5;
		static Gtk.RadioButton radiobutton6;
		static Gtk.RadioButton radiobutton7;
		static Gtk.Button tb2;
		static Gtk.ProgressBar pbar;

		public static void Main()
		{
			/*
	      Ermittelt die Werte von
	       – Plattform
	       – AlbumRootPath
	       – Ersatzstr
	       – Sep
	       – Bereiche
	    */
			DiePlattform.BestimmePlattform();
			Bereiche = ["mittel", "gross", "vierk"];
			RelativPaths.RelativePfade();
			/*
	      Nun wird die Datei 'DefaultWerte.xml' eingelesen. Sie liefert
	       – 'Aktiv' 
	       – 'Bildformat'
				 - 'Drehwinkel'
	       – 'LangDefault'
	       – 'FotoalbenPath'
	       – 'DefaultName'
	       – 'Albumname' 
	       – 'Startpfad'
				 - 'AlbumBasics.AltDirectory'

				Wir verwenden relative Pfade bei den Bildern. Dafür ist
				'Aktiv' auf 'true' gesetzt. Das ist nützlich, wenn die 
				Bilder auf einem gemeinsamen Server gespeichert sind. 
				 Der Wert von 'fotoalben' wird zur Zeit nicht ausgewertet.
	    */
			XMLDoc.MainXMLDoc();
			Console.WriteLine("Ich bin zurück. Pfade sind eingelesen.");
			Sprachdateien.SpracheLaden(XMLDoc.LangDefault);
			int[] drehwinkelarray = [270, 90];
			DrehwinkelArray = (int[])drehwinkelarray.Clone();

			Gtk.Application.Init();
			Console.WriteLine("Nun soll das Startfenster erstellt werden.");
			//Erstelle das StartFenster:
			//[37]: "Erstelle dein Fotoalbum!"
			myWin = new Window(Localarray[37]);
			myWin.Resize(500, 150);
			myWin.DeleteEvent += new DeleteEventHandler(Window_Delete);
			Gtk.Label copyLabel = new("Bilder kopieren?");

			Box vbox = new(Orientation.Vertical, 5);   // Gesamtbox
			Box vbox1 = new(Orientation.Vertical, 5);   // Box für die drei ersten Radiobuttons
			Box hbox1 = new(Orientation.Horizontal, 0); // Box für vbox1
			Box hbox2 = new(Orientation.Horizontal, 0); // Box für Label und Entry (homogen)
			Box hbox3 = new(Orientation.Horizontal, 0); // Box für die Buttons (nicht homogen)
			Box hbox4 = new(Orientation.Horizontal, 5); // Box für ein weiteres Radio
			Box hbox5 = new(Orientation.Horizontal, 5); // Box für ein weiteres Radio
			Box hbox6 = new(Orientation.Horizontal, 0); // HBox für die beiden Radios
			Box hbox7 = new(Orientation.Horizontal, 0); // HBox für die Kopiereinstellung;

			// radiobutton1 = new RadioButton(null, "Das aktuelle Album bearbeiten: Drücke 'Go'");
			// radiobutton2 = new RadioButton(radiobutton1, "Ein bestehendes Album bearbeiten");
			// radiobutton3 = new RadioButton(radiobutton1, "Ein neues Album erstellen");
			radiobutton1 = new RadioButton(null, Localarray[5]);
			radiobutton2 = new RadioButton(radiobutton1, Localarray[6]);
			radiobutton3 = new RadioButton(radiobutton1, Localarray[7]);
			radiobutton4 = new RadioButton(null, "Drehwinkel  90 (Foto)");
			radiobutton5 = new RadioButton(radiobutton4, "Drehwinkel 270 (Handy)");
			if (XMLDoc.Drehwinkel == 90) radiobutton4.Active = true;
			else radiobutton5.Active = true;
			// radiobutton5.Active = true;
			radiobutton6 = new RadioButton(null, "ja");
			radiobutton7 = new RadioButton(radiobutton6, "nein (Default)");
			string wertstr = XMLDoc.CopyImagesBool ? "true" : "false";
			Console.WriteLine("CopyImagesBool hat den Wert {0}", wertstr);
			radiobutton6.Active = !XMLDoc.CopyImagesBool;
			radiobutton7.Active = !XMLDoc.CopyImagesBool;

			hbox6.PackStart(radiobutton4, false, false, 0);
			hbox6.PackStart(radiobutton5, false, false, 0);
			hbox7.PackStart(copyLabel, false, false, 0);
			hbox7.PackStart(radiobutton6, false, false, 10);
			hbox7.PackStart(radiobutton7, false, false, 10);

			vbox1.PackStart(radiobutton1, false, false, 10);
			vbox1.PackStart(radiobutton2, false, false, 10);
			vbox1.PackStart(radiobutton3, false, false, 10);
			vbox1.PackStart(hbox6, false, false, 10);
			vbox1.PackStart(hbox7, false, false, 10);

			// Gtk.Button tb1 = new("Abbruch");
			// tb2 = new Gtk.Button("    Go!    ");
			Button tb1 = new(Localarray[8]);
			tb2 = new Button(Localarray[9]);

			pbar = new Gtk.ProgressBar
			{
				Fraction = 0.0     // Startwert
			};
			vbox1.PackStart(pbar, false, false, 10);
			pbar.Show();

			entry1 = new Gtk.Entry();
			entry1.IsEditable = false;
			entry1.Changed += new EventHandler(OnEntryChanged);

			radiobutton1.Clicked += new EventHandler(OnRadio1Clicked);
			radiobutton2.Clicked += new EventHandler(OnRadio2Clicked);
			radiobutton3.Clicked += new EventHandler(OnRadio3Clicked);
			radiobutton4.Clicked += new EventHandler(OnRadio4Clicked);
			radiobutton5.Clicked += new EventHandler(OnRadio5Clicked);
			radiobutton6.Clicked += new EventHandler(OnRadio6Clicked);
			radiobutton7.Clicked += new EventHandler(OnRadio7Clicked);

			tb1.Clicked += new EventHandler(OnTb1Clicked);
			tb2.Clicked += new EventHandler(OnTb2Clicked);

			// Ordner mit dem konkreten einzelnen Fotoalbum:
			AlbumnamePath = Path.Combine(XMLDoc.FotoalbenPath, XMLDoc.Albumname);
			Console.WriteLine("Fotoalbum: {0}", AlbumnamePath);

			myLabel = new Label
			{
				Text = Localarray[10],
				Halign = Align.End
			};
			Label fillLabel = new();

			entry1.Text = XMLDoc.Albumname;

			hbox1.PackStart(vbox1, false, false, 10);
			hbox2.PackStart(myLabel, true, true, 10);
			hbox2.PackStart(entry1, true, true, 10);
			hbox3.PackStart(tb1, false, false, 10);
			hbox3.PackStart(fillLabel, true, true, 0);
			hbox3.PackStart(tb2, false, false, 10);
			vbox.PackStart(hbox1, false, false, 2); // die Radiobuttons
			vbox.PackStart(hbox2, false, false, 2); // Label und Entry
			vbox.PackStart(hbox3, false, false, 2); // die Buttons

			//Add the vbox to the window
			myWin.Add(vbox);

			//Show Everything
			myWin.ShowAll();

			Gtk.Application.Run();
		}
	}
}




