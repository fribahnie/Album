using System.Collections.Generic;

namespace BasicClasses
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
	public class AlbumPunkt
	{
		public int XWert;
		public int YWert;

		public AlbumPunkt(int x, int y)
		{
			XWert = x;
			YWert = y;
		}
		public AlbumPunkt() { }
	}


	public class AlbumBild
	{
		public string Datei { set; get; } // die Quelle des Bildes
		public Gtk.Image Bild { set; get; }
		public AlbumPunkt EckPunkt { set; get; } // platziert das Bild
		public string Richtung = "Q";  // 'Q' für Querformat, 'H' für Hochkant
		public bool BaukastenBild = true;
	}


	public class AlbumVorschauSeite
	{
		public List<AlbumPunkt> B_Ecken;
		public List<AlbumPunkt> T_Ecken;
		public int Bilderzahl;
		public string Name;
		public string Ausrichtung;

		public AlbumVorschauSeite(List<AlbumPunkt> b_Ecken,
						List<AlbumPunkt> t_Ecken,
						int anzahl,
						string formatname,
						string ausrichtung)
		{
			B_Ecken = b_Ecken;
			T_Ecken = t_Ecken;
			Bilderzahl = anzahl;
			Name = formatname;
			Ausrichtung = ausrichtung;
		}

		public AlbumVorschauSeite() { }
	}
}
