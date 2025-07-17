using System.Collections.Generic;

namespace BasicClasses
{
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
