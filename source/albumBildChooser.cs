using Gtk;
using System;
using Startfenster;
using ModuleHtml;

namespace AlbumBasis
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

	/*  
		Mit den Methoden der Klasse 'AlbumBildChooser' können 
		Bilder für das Vorschaufenster ausgewählt werden. Der 
		'FileChooser' wird aufgerufen. Er kann jeweils ein Bild 
		darstellen.
		Mit der Entscheidung für dieses Bild ('Meine Wahl') wird 
		der Dateipfad ins Clippboard kopiert. Durch einen Klick 
		der rechten Maustaste wird das Bild in ein Bildfeld 
		eingefügt. Es ersetzt das vorhandene Bild.
	*/
	class AlbumBildDateien
	{
		static readonly string DefaultFolder = Environment.GetFolderPath
				(Environment.SpecialFolder.MyPictures);
		static readonly string DefaultFiles = DefaultFolder + XMLDoc.Sep + "*";
		static string auswahlfile = string.Empty;
		static Gtk.FileChooserWidget fcw;
		static Gtk.Window win1;


		public static void OnRufeFileChooser(object obj, EventArgs args)
		{
			win1 = new Gtk.Window(StartFenster.Localarray[1]);
			win1.SetDefaultSize(800, 450);
			Box vbox1 = new(Orientation.Vertical, 5);
			Box hbox = new(Orientation.Horizontal, 5);
			Gtk.Button btn1 = new(StartFenster.Localarray[2]);
			Gtk.Button btn2 = new(StartFenster.Localarray[3]);
			Gtk.Button btn3 = new(StartFenster.Localarray[46]);
			btn1.Clicked += new EventHandler(Btn1_Click);
			btn2.Clicked += new EventHandler(Btn2_Click);
			btn3.Clicked += new EventHandler(Btn3_Click);

			hbox.PackEnd(btn2, false, false, 0);
			hbox.PackEnd(btn1, false, false, 0);
			hbox.PackEnd(btn3, false, false, 0);
			fcw = new FileChooserWidget(FileChooserAction.Open);
			vbox1.PackStart(fcw, true, true, 2);
			vbox1.PackStart(hbox, false, false, 2);

			Gtk.Widget preview = fcw.PreviewWidget;
			fcw.UpdatePreview += new EventHandler(OnUpdatePreview);
			fcw.ShowAll();

			fcw.SetFilename(DefaultFiles);
			fcw.SetCurrentFolder(DefaultFolder);
			win1.Add(vbox1);
			win1.ShowAll();
		}

		static void OnUpdatePreview(object sender, EventArgs args)
		{
			Gtk.FileChooserWidget fcw = (Gtk.FileChooserWidget)sender;
			string prevfile = fcw.PreviewFilename;
			if (prevfile != null)
			{
				string result = prevfile.ToLower();
				string[] endArray = {".jpg", ".jpeg", ".png", ".pnm",
				".bmp", ".tiff", ".tif", ".ppm"};
				foreach (string item in endArray)
				{
					if (result.EndsWith(item))
					{
						var pixbuf = new Gdk.Pixbuf(prevfile, 512, 512);
						if (pixbuf != null)
						{
							Gtk.Image image = new();
							image.Pixbuf = pixbuf;
							fcw.PreviewWidget = (Gtk.Widget)image;
							fcw.PreviewWidgetActive = true;
							auswahlfile = fcw.Filename;
						}
					}
				}
			}
		}


		static void Btn1_Click(object obj, EventArgs args)
		{
			Gtk.Clipboard clipboard =
		Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));

			clipboard.Text = auswahlfile;
			Console.WriteLine(StartFenster.Localarray[4], auswahlfile);
		}

		static void Btn3_Click(object obj, EventArgs args)
		{
			HtmlBuild.InhaltBild = auswahlfile;
			Console.WriteLine(StartFenster.Localarray[4], auswahlfile);
			win1.Destroy();
		}


		static void Btn2_Click(object obj, EventArgs args)
		{
			win1.Destroy();
		}
	}
}
