using System.Collections.Generic;
using Gtk;
using ModuleHtml;
using Startfenster;

namespace AlbumBasis
{

	public partial class AlbumApp : Gtk.Window
	{
		/*
      enthält:

      - public Gtk.MenuBar ErzeugeMenue()
      - static void OnToggeld(object sender, EventArgs args)
    */

		public Gtk.MenuBar ErzeugeMenue()
		{
			var fotoformatcheckboxList = new List<Gtk.CheckMenuItem>();
			var winkelcheckboxList = new List<Gtk.CheckMenuItem>();
			var displaycheckboxList = new List<Gtk.CheckMenuItem>();
			var copycheckboxList = new List<Gtk.CheckMenuItem>();
			var formateList = new List<Gtk.MenuItem>();
			var sprachenList = new List<MenuItem>();


			/*
	      Erzeugt das Menü:
	     */
			Gtk.MenuBar bar = new();
			// accel key
			Gtk.AccelGroup ag = new();
			this.AddAccelGroup(ag);

			// Menüpunkt 1: File
			//[42]: "Datei"
			Gtk.Menu menu = new();
			Gtk.MenuItem item = new(StartFenster.Localarray[42])
			{
				Submenu = menu
			};
			bar.Append(item);

			//[12]: "BaueHtml"
			item = new Gtk.MenuItem(StartFenster.Localarray[12]);
			item.Activated += HtmlBuild.OnBaueHtml;
			menu.Append(item);

			//[13]: "Sichern"
			item = new Gtk.MenuItem(StartFenster.Localarray[13]);
			item.Activated += AlbumSave.OnSichern;
			menu.Append(item);

			//[14]: "ZeigeAlbum"
			item = new Gtk.MenuItem(StartFenster.Localarray[14]);
			item.Activated += AlbumBrowser.OnBrowserAufruf;
			menu.Append(item);

			//[15]: "Beenden"
			item = new Gtk.MenuItem(StartFenster.Localarray[15]);
			item.Activated += OnTerminated;
			item.AddAccelerator
				("activate", ag, new Gtk.AccelKey
				 (Gdk.Key.Q, Gdk.ModifierType.ControlMask,
				Gtk.AccelFlags.Visible));
			menu.Append(item);

			// Menüpunkt 2: [16]: "Blättern"
			menu = new Gtk.Menu();
			item = new Gtk.MenuItem(StartFenster.Localarray[16])
			{
				Submenu = menu
			};
			bar.Append(item);

			//[17]: "vorwärts"
			item = new Gtk.MenuItem(StartFenster.Localarray[17]);
			item.Activated += OnBlaetternVor;
			item.AddAccelerator
				("activate", ag, new Gtk.AccelKey
				 (Gdk.Key.F, Gdk.ModifierType.ControlMask,
				Gtk.AccelFlags.Visible));
			menu.Append(item);

			//[18]: "rückwärts"
			item = new Gtk.MenuItem(StartFenster.Localarray[18]);
			item.Activated += OnBlaetternRueck;
			item.AddAccelerator
				("activate", ag, new Gtk.AccelKey
				 (Gdk.Key.B, Gdk.ModifierType.ControlMask,
				Gtk.AccelFlags.Visible));
			menu.Append(item);

			// Menüpunkt 3: Einstellungen
			//[19]: "Einstellungen"
			menu = new Gtk.Menu();
			item = new Gtk.MenuItem(StartFenster.Localarray[19]);
			item.Submenu = menu;
			bar.Append(item);

			//[20]: "Fotoformat"
			item = new Gtk.MenuItem(StartFenster.Localarray[20]);
			Gtk.Menu unteruntermenu = new();
			item.Submenu = unteruntermenu;
			menu.Append(item);

			AlbumMenuePunkte.FotoformatMenuePunkt("16x12", fotoformatcheckboxList, unteruntermenu);
			AlbumMenuePunkte.FotoformatMenuePunkt("16x09", fotoformatcheckboxList, unteruntermenu);
			AlbumMenuePunkte.FotoformatMenuePunkt("12x12", fotoformatcheckboxList, unteruntermenu);

			//[20]: "Drehwinkel"
			item = new Gtk.MenuItem("Drehwinkel");
			Gtk.Menu winkeluntermenu = new();
			item.Submenu = winkeluntermenu;
			menu.Append(item);

			var checkitem = new Gtk.CheckMenuItem("270 Grad");
			checkitem.Activated += AlbumMenuePunkte.OnToggeldWinkel;
			winkelcheckboxList.Add(checkitem);
			winkeluntermenu.Append(checkitem);

			checkitem = new Gtk.CheckMenuItem(" 90 Grad");
			checkitem.Activated += AlbumMenuePunkte.OnToggeldWinkel;
			winkelcheckboxList.Add(checkitem);
			winkeluntermenu.Append(checkitem);

			//[43]: "Sprachauswahl (Sichern - Neustart)"
			item = new Gtk.MenuItem(StartFenster.Localarray[43]);
			Gtk.Menu untermenu3 = new();
			item.Submenu = untermenu3;
			menu.Append(item);

			//[44]: "englisch"
			item = new Gtk.MenuItem(StartFenster.Localarray[44]);
			item.Activated += OnSprachauswahl;
			untermenu3.Append(item);
			sprachenList.Add(item);

			//[45]: "deutsch"
			item = new Gtk.MenuItem(StartFenster.Localarray[45]);
			item.Activated += OnSprachauswahl;
			untermenu3.Append(item);
			sprachenList.Add(item);
			AlbumApp.Sprachenliste = sprachenList;

			//[21]: "Displaygröße:"
			item = new Gtk.MenuItem(StartFenster.Localarray[21]);
			Gtk.Menu untermenu2 = new();
			item.Submenu = untermenu2;
			menu.Append(item);

			//[22]: "mittel: 1440px"
			AlbumMenuePunkte.DisplayMenuePunkt(StartFenster.Localarray[22], untermenu2, displaycheckboxList);
			//[23]: "gross: 1920px"
			AlbumMenuePunkte.DisplayMenuePunkt(StartFenster.Localarray[23], untermenu2, displaycheckboxList);
			//[24]: "vierk: 3840px"
			AlbumMenuePunkte.DisplayMenuePunkt(StartFenster.Localarray[24], untermenu2, displaycheckboxList);

			// Menüpunkt 4: Wähle Bilder
			//[25]: "Wähle Bilder"
			menu = new Gtk.Menu();
			item = new(StartFenster.Localarray[25])
			{
				Submenu = menu
			};
			bar.Append(item);

			//[26]: "Dateimanager"
			item = new Gtk.MenuItem(StartFenster.Localarray[26]);
			item.Activated += AlbumBildDateien.OnRufeFileChooser;
			menu.Append(item);

			// Menüpunkt 5: Edit
			//[27]: "Edit"
			menu = new Gtk.Menu();
			item = new Gtk.MenuItem(StartFenster.Localarray[27]);
			item.Submenu = menu;
			bar.Append(item);

			/*
	      Die Icons für das Menü werden erzeugt und
	      jeweils in eine HBox gepackt. Die Bilder
	      befinden sich in dem Verzeichnis
	      './Baukasten/icons/'. Die HBox wird später
	      mit "item.Add()" in das MenuItem-Widget
	      eingefügt.  Die Strings dienen als
	      Bezeichnung für die icons, mehr nicht.
	    */
			var box4boQdoQauQcuQ = ErzeugeHBox("A4boQdoQauQcuQ"); // 4 Bilder
			var box4aoQcoQbuQduQ = ErzeugeHBox("A4aoQcoQbuQduQ"); // 4 Bilder
			var box3auHbmQdoH = ErzeugeHBox("A3auHbmQdoH"); // 3 Bilder
			var box3amHcuQdoQ = ErzeugeHBox("A3amHcuQdoQ"); // 3 Bilder 
			var box3amQcuQdoQ = ErzeugeHBox("A3amQcuQdoQ"); // 3 Bilder
			var box3aoQbuQdmH = ErzeugeHBox("A3aoQbuQdmH"); // 3 Bilder
			var box3aoQbuQdmQ = ErzeugeHBox("A3aoQbuQdmQ"); // 3 Bilder
			var box3auHcmHdoH = ErzeugeHBox("A3auHcmHdoH"); // 3 Bilder 
			var box3auQboQdmH = ErzeugeHBox("A3auQboQdmH"); // 3 Bilder
			var box3auQcoHduH = ErzeugeHBox("A3auQcoHduH"); // 3 Bilder 
			var box2bmQcoH = ErzeugeHBox("A2bmQcoH"); // 2 Bilder
			var box2boHcmQ = ErzeugeHBox("A2boHcmQ"); // 2 Bilder
			var box2boQcuQ = ErzeugeHBox("A2boQcuQ"); // 2 Bilder
			var box2buHcoH = ErzeugeHBox("A2buHcoH"); // 2 Bilder
			var box2buQcoQ = ErzeugeHBox("A2buQcoQ"); // 2 Bilder
			var box1cmQ = ErzeugeHBox("A1cmQ"); // 1 Bild

			//[28]: "Neue Seite"
			item = new Gtk.MenuItem(StartFenster.Localarray[28]);
			Gtk.Menu untermenu = new();
			item.Submenu = untermenu;
			menu.Append(item);

			/* Die 16 Untermenüs für die Seitenformate (je nach Anzahl der Bilder pro Seite): */
			//[29]: "4 Bilder"
			var formatbild = AlbumMenuePunkte.Bilderzahl(StartFenster.Localarray[29], untermenu);
			AlbumMenuePunkte.SeitenformatMenuPunkt(box4aoQcoQbuQduQ, formatbild, formateList); // Format A4aoQcoQbuQduQ:     
			AlbumMenuePunkte.SeitenformatMenuPunkt(box4boQdoQauQcuQ, formatbild, formateList); // Format A4boQdoQauQcuQ:   

			//[30]: "3 Bilder"
			formatbild = AlbumMenuePunkte.Bilderzahl(StartFenster.Localarray[30], untermenu);
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3aoQbuQdmQ, formatbild, formateList);  // Format  A3aoQbuQdmQ:
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3amQcuQdoQ, formatbild, formateList);  // Format  A3amQcuQdoQ
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3aoQbuQdmH, formatbild, formateList);  // Format  A3aoQbuQdmH
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3auQboQdmH, formatbild, formateList);  // Format  A3auQboQdmH 
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3amHcuQdoQ, formatbild, formateList);  // Format  A3amHcuQdoQ
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3auQcoHduH, formatbild, formateList);  // Format  A3auQcoHduH 
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3auHbmQdoH, formatbild, formateList);  // Format  A3auHbmQdoH
			AlbumMenuePunkte.SeitenformatMenuPunkt(box3auHcmHdoH, formatbild, formateList);  // Format  A3auHcmHdoH

			//[31]: "2 Bilder"
			formatbild = AlbumMenuePunkte.Bilderzahl(StartFenster.Localarray[31], untermenu);
			AlbumMenuePunkte.SeitenformatMenuPunkt(box2boQcuQ, formatbild, formateList); // Format A2boQcuQ    
			AlbumMenuePunkte.SeitenformatMenuPunkt(box2buQcoQ, formatbild, formateList); // Format A2buQcoQ    
			AlbumMenuePunkte.SeitenformatMenuPunkt(box2bmQcoH, formatbild, formateList); // Format A2bmQcoH
			AlbumMenuePunkte.SeitenformatMenuPunkt(box2boHcmQ, formatbild, formateList); // Format A2boHcmQ 
			AlbumMenuePunkte.SeitenformatMenuPunkt(box2buHcoH, formatbild, formateList); // Format A2buHcoH

			//[32]: "1 Bild"
			formatbild = AlbumMenuePunkte.Bilderzahl(StartFenster.Localarray[32], untermenu);
			AlbumMenuePunkte.SeitenformatMenuPunkt(box1cmQ, formatbild, formateList);    // Format A1cmQ

			//[33]: "Lösche Seite"
			item = new Gtk.MenuItem(StartFenster.Localarray[33]);
			item.Activated += OnLoeschen;
			menu.Append(item);

			StartFenster.FormateList = formateList;
			StartFenster.FotoformatCheckboxList = fotoformatcheckboxList;
			StartFenster.WinkelCheckboxList = winkelcheckboxList;
			return bar;
		}

		static Gtk.Box ErzeugeHBox(string datei) /* Für die Icons im Menü: */
		{
			Gtk.Image bild = new();
			datei += ".png";
			datei = @"./Baukasten/icons/" + datei;
			Gdk.Pixbuf pixbuf = new(datei, 48, 48);
			bild.Pixbuf = pixbuf;
			var hbox = new Box(Orientation.Horizontal, 0);
			hbox.PackStart(bild, false, false, 0);
			return hbox;
		}
	}
}


