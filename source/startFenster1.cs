using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Gtk;

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
		public static string AlbumRootPath { set; get; }// Pfad zu dem Programm
		public static string FotoalbenPath { set; get; }// Pfad zu den Fotoalben
		public static string AlbumnamePath { set; get; }// Pfad zu den Dateien des Fotoalbums
		public static string FotoRootDir { set; get; }// Fotoalben-Verzeichnis
		public static string Albumname { set; get; }// Name des Albums
		public static string DefaultName { set; get; }// Defaultname des Albums
		public static string Bildformat { set; get; }// z.B. '16x12' für 'AlbumMenu'
		public static string HomeBilder { set; get; }// z.B.:  'C:\Users\fbahr\'
		public static int DisplayDefault { set; get; }// Größe für die html-Ausgabe
		public static int Plattform { set; get; }// 0 = win, 1 = linux, 2 = OSX
		public static string Sep { set; get; }// Seperator in Dateipfaden
		public static string Rel { set; get; }// "./" in relativen Dateipfaden
		public static string LangDefault { get; set; }// Sprache der Textbausteine
		public static string CopyImages { set; get; }// 'false' oder 'true' in pfade.xml
		public static bool CopyImagesBool { set; get; } // sollen die Bilder ins Album kopiert werden?
		public static string Aktiv { set; get; }// 'false' oder 'true' in pfade.xml
		public static string Startpfad { set; get; }// Pfadabschnitt zum Starterset
		public static string[] Localarray { set; get; }
		public static int Drehwinkel { set; get; }
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
			BestimmePlattform();
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
			LiesDefaultWerteEin();
			Console.WriteLine("Ich bin zurück. Pfade sind eingelesen.");
			SpracheLaden(LangDefault);
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
			radiobutton5.Active = true;
			radiobutton6 = new RadioButton(null, "ja");
			radiobutton7 = new RadioButton(radiobutton6, "nein (Default)");
			string wertstr = CopyImagesBool ? "true" : "false";
			Console.WriteLine("CopyImagesBool hat den Wert {0}", wertstr);
			radiobutton6.Active = !CopyImagesBool;
			radiobutton7.Active = !CopyImagesBool;

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
			AlbumnamePath = Path.Combine(FotoalbenPath, Albumname);
			Console.WriteLine("Fotoalbum: {0}", AlbumnamePath);

			myLabel = new Label
			{
				Text = Localarray[10],
				Halign = Align.End
			};
			Label fillLabel = new();

			entry1.Text = Albumname;

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

		static void BestimmePlattform()
		{
			Console.WriteLine("Wir beginnen damit, die Plattform zu bestimmen.");
			// ermittelt die verwendete Plattform:
			int plattform = -1;
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				plattform = 0;
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				plattform = 1;
				Console.WriteLine("Wir schaffen mit Linux.");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				// orig = "file://";
				plattform = 2;
			}
			Plattform = plattform;                  // Wertzuweisung 0, 1 od. 2
			Console.WriteLine("Der Plattform wurde der Wert {0} zugewiesen", plattform);
			//AlbumBasics.RelativePfade();            //–>'AlbumRootPath', 'Ersatzstr'
			//Rel = "." + Sep;                        // und 'Sep'
			string[] bereiche = { "mittel", "gross", "vierk" };
			Bereiche = bereiche;
			Console.WriteLine("Wir verlassen die Bestimmung der Plattform.");
		}

		static void LiesDefaultWerteEin()
		{
			XmlDocument xmldefault = new();
			string zwischenstr = "Baukasten/Werte/DefaultWerte.xml";
			zwischenstr = zwischenstr.Replace("/", Sep);
			string wertepfad = AlbumRootPath + zwischenstr;
			Console.WriteLine("der Wertepfad: {0}", wertepfad);
			xmldefault.Load(wertepfad);
			XmlNode copyimages = xmldefault.SelectSingleNode("DefaultWerte/copyimages");
			XmlNode aktiv = xmldefault.SelectSingleNode("DefaultWerte/aktiv");
			XmlNode bildformat = xmldefault.SelectSingleNode("DefaultWerte/bildformat");
			XmlNode drehwinkel = xmldefault.SelectSingleNode("DefaultWerte/drehwinkel");
			XmlNode lang_default = xmldefault.SelectSingleNode("DefaultWerte/lang_default");
			XmlNode alternatdirectory = xmldefault.SelectSingleNode("DefaultWerte/alternatdirectory");
			CopyImages = copyimages.InnerText; // sollen die Bilder ins Album kopiert werden?
			CopyImagesBool = CopyImages == "true"; // je nach Einstellung: true oder false

			Aktiv = aktiv.InnerText;         // absolute od. relat. Pfade
			Bildformat = bildformat.InnerText;    // '16x12' od. '16x09'
			Drehwinkel = int.Parse(drehwinkel.InnerText); // 270 oder 90 Grad
			LangDefault = lang_default.InnerText;  // Vorgabe Sprache
			RelativPaths.AltDirectory = alternatdirectory.InnerText;
			if (RelativPaths.AltDirectory != string.Empty && Plattform == 0)
			{
				HomeBilder = RelativPaths.AltDirectory;
				RelativPaths.AlternativOrdnerBool = true;
			}
			else
			{
				RelativPaths.AlternativOrdnerBool = false;
			}
			FotoRootDir = "Fotoalben";                // Verzeichnis der Fotoalben
			Console.WriteLine("AlbumRootPath: {0}; FotoRootDir: {1};", AlbumRootPath, FotoRootDir);
			FotoalbenPath = Path.Join(AlbumRootPath, Sep, FotoRootDir);
			FotoalbenPath = Path.GetFullPath(FotoalbenPath);// zu den Fotoalben
			Console.WriteLine("Das Fotodir: {0}", FotoalbenPath);
			DefaultName = @"Meine_Hunde";
			// ???? FotoalbenPath = Path.GetFullPath(FotoRootDir);
			string fileName = Path.Combine(FotoalbenPath, "albumname.txt");
			Console.WriteLine("Lies das File: {0}", fileName);
			try
			{
				Console.WriteLine("Der Albumname: {0}", fileName);
				Albumname = File.ReadAllText(fileName);   // der gespeicherte Albumname
				Console.WriteLine("Der gespeicherte Albumname: {0}", Albumname);
			}
			catch // (Exception e)
			{
				Albumname = DefaultName;                    // der Default Albumname
			}
		}

		public static void SpracheLaden(string Sprachdatei)
		{
			Console.WriteLine("Der Startpfad für die Sprachtabellen wird erstellt.");
			// Erstelle 'Startpfad':
			Hashtable sprachtabelle = new()
			{
				{ "local_de.txt", "Start_de" },
				{ "local_en.txt", "Start_en" }
			};
			Startpfad = (string)sprachtabelle[Sprachdatei]; // Typumwandlung
			Console.WriteLine("Der Startpfad für die Sprachdatei: {0}", Startpfad);

			// Erstelle 'StartFenster.Localarray':
			string filepfad = AlbumRootPath + "Baukasten" + Sep + "local" + Sep + Sprachdatei;
			Console.WriteLine("In SpracheLaden ist der filepfad {0}", filepfad);
			string ergstr = System.IO.File.ReadAllText(filepfad);
			ergstr = ergstr.Replace("\n", string.Empty);
			//Console.WriteLine("SpracheLaden: Der String: {0}", ergstr);
			string[] localarr = ergstr.Split(',');
			//Console.WriteLine("Die Länge des localarray: {0}", localarr.Length);
			//Console.WriteLine(localarr[^1]);
			Localarray = (string[])localarr.Clone();
		}
	}
}
