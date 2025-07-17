/*
  Die Klasse 'AlbumFormate' ist zuständig für die unterschiedlichen
  Layouts des Vorschauprogramms, also wo die Vorschaubilder und die TextViews 
  platziert werden. Das Layout ist abhängig davon,
  welches Format mit 'formatname' gewählt wurde (z.B. für drei oder 
  vier Bilder, Querformat oder Hochformat, 16x12 oder 16x9 usw.).
  'formatname' wird über das Menü gewählt und dann in der xml-Datei
  des Fotoalbums mit <format>'formatname'</format> gespeichert.
      
  Die Methode 'HoleEckpunkte' liefert die AlbumVorschauSeite
  'VorschauSeite' mit den Eckpunkten für die Platzierung
  der Bilder mit ihrer Ausrichtung und die Buttons dieser Seite.  Die Methode wird
  von den Klassen 'AlbumBuildSeite' und 'AlbumSeite' aufgerufen.
*/

using System;
using System.Collections.Generic;
using Startfenster;
using BasicClasses;

namespace AlbumBasis
{
	public static class AlbumFormate
	{
		public static AlbumVorschauSeite VorschauSeite { set; get; } // speichert das Ergebnis. 
		static int Vorschau_Breite { set; get; } // Breite des Vorschaubildes 
		static int Vorschau_Hoehe { set; get; } // Höhe des Vorschaubildes 
		static int Xoffset { set; get; } // Einrückung horizontal 
		static int YOffset { set; get; } // Einrückung vertikal 

		public static void HoleEckpunkte(string formatname)
		{
			Console.WriteLine("Der Formatname, der hier angekommen ist, lautet: {0}", formatname);
			Vorschau_Breite = AlbumRead.Vorschauarray[0]; /* 320  */
			Vorschau_Hoehe = AlbumRead.Vorschauarray[1];  /* 240  */
			YOffset = AlbumRead.Vorschauarray[2];         /*  50  */
			int abstand = AlbumRead.Vorschauarray[3];     /*  10  */
			int groesse = AlbumRead.Vorschauarray[4];     /*   2  */
			StartFenster.DisplayDefault = groesse;               /* int => "mittel", "gross" od. "vierk" */
			int Xoffset = 10;
			int marginmin = 10;
			int halbebreite = Vorschau_Breite / 2;        /* 160 */
			int hoehe16x12 = Vorschau_Hoehe;              /* 240 */
			int gesamtx = 5 * halbebreite + abstand + 2 * marginmin;
			int gesamty = YOffset + 2 * hoehe16x12 + 2 * abstand;
			int margingross = ((2 * marginmin + halbebreite) / 2); /* 90 */
			int hmitte = gesamtx / 2;                        /*  415 */
			int h1 = Xoffset + marginmin;                              /* Bild unten links */
			int h2 = Xoffset + marginmin + 1 * halbebreite;            /* Bild links oben eingerückt */
			int h3 = Xoffset + marginmin + 2 * halbebreite + abstand;  /* Bild unten rechts */
			int h4 = Xoffset + marginmin + 3 * halbebreite + abstand;  /* Bild oben rechts eingerückt */
			int h5 = Xoffset + margingross;                            /* 2 Bilder mittig angeordnet: */
			int h6 = Xoffset + margingross + 2 * halbebreite + abstand;
			int h7 = Xoffset + hmitte - halbebreite;
			int h8 = Xoffset + gesamtx - marginmin - hoehe16x12;
			int h9 = Xoffset + hmitte - hoehe16x12 / 2;
			int h10 = Xoffset + gesamtx - marginmin - hoehe16x12;
			int h11 = Xoffset + marginmin + 4 * halbebreite + 2 * abstand;
			int h12 = Xoffset + marginmin + 2 * halbebreite + abstand + ((h8 - abstand) - h3) / 2 - hoehe16x12 / 2;
			int h13 = Xoffset + gesamtx - marginmin - hoehe16x12;
			int h14 = h3 + 2 * halbebreite + abstand;
			int h15 = Xoffset + hmitte;
			int yOffset = YOffset;
			int v1 = YOffset;                                /* y-Werte für 16x12 und 16x09 */
			int v2 = YOffset + (hoehe16x12 + abstand) / 2;
			int v3 = YOffset + hoehe16x12 + abstand;
			int v4 = v3 - hoehe16x12 / 2 - (abstand / 2);    /* */
			int v5 = v3 + hoehe16x12 / 2 + (abstand / 2);    /* */
			int v6 = YOffset + hoehe16x12 / 2 - 5 * abstand;
			int v7 = v6 + 2 * halbebreite + abstand;
			int v9 = gesamty - abstand - 2 * halbebreite;
			int v10 = v1 + (v9 - v1) / 2;
			int v11 = yOffset + 2 * halbebreite + abstand;
			int v12 = v6 + 2 * halbebreite - hoehe16x12;
			int v13 = v6 + 2 * halbebreite + abstand;
			int v14 = v6 + hoehe16x12 / 2;
			int v15 = v6 + hoehe16x12 + abstand;  /* */
			int v16 = v4 + hoehe16x12 + abstand;

			/*

	      Alle meine Formen für die Bilder und die zugehörigen Buttons
	      als Instanzen der class 'AlbumVorschauSeite'. Anhand ihres
	      Namens wird die gewünschte AlbumVorschauSeite
	      erstellt und nach dem switch-Konstrukt ausgewertet;

	    */

			AlbumVorschauSeite vorschauSeite = new();
			List<AlbumPunkt> b_Ecken = new() { };

			switch (formatname)
			{
				case "4aoQcoQbuQduQ":
					/* 4 Bilder Formatname: "4aoQcoQbuQduQ" */
					b_Ecken = new List<AlbumPunkt>{
						new(h1,v1),
						new(h3,v1),
						new(h2,v3),
						new(h4,v3)
				};
					vorschauSeite = new AlbumVorschauSeite
				(
				 b_Ecken,
				 new List<AlbumPunkt>{
					 new(h11, v1),
					 new(h1, v3)
				 },
				 4, "4aoQcoQbuQduQ", "QQQQ");
					break;
				case "4boQdoQauQcuQ":
					/* 4 Bilder Formatname "4boQdoQauQcuQ" */
					b_Ecken = new List<AlbumPunkt>{
						new(h2,v1),
						new(h4,v1),
						new(h1,v3),
						new(h3,v3)
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken,
				 		new List<AlbumPunkt>{
						new(h1, v1),
						new(h11, v3)
				 },
				 4, "4boQdoQauQcuQ", "QQQQ");
					break;
				/* 3 Bilder Formatname "3aoQbuQdmQ" */
				case "3aoQbuQdmQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h1, v1),
						new(h2, v3),
						new(h4, v2)
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken,
						 new List<AlbumPunkt>{
					 	new(h1, v5),
						new(h3, v1)
				 },
				 3, "3aoQbuQdmQ", "QQQ");
					break;

				/* 3 Bilder Formatname "3amQcuQdoQ" */
				case "3amQcuQdoQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h1, v2),
						new(h4, v1),
						new(h3, v3)
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken,
				 		new List<AlbumPunkt>{
					 	new(h1, v1),
					 	new(h1, v5)
				 },
				 3, "3amQcuQdoQ", "QQQ");
					break;
				/* 3 Bilder Formatname "3aoQbuQdmH" */
				case "3aoQbuQdmH":
					b_Ecken = new List<AlbumPunkt>{
						new(h1,v1),
						new(h2,v3),
						new(h4,v6)
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken, new List<AlbumPunkt>{
					 	new(h3, v1),
					 	new(h1, v3)
				 	},
				 	3, "3aoQbuQdmH", "QQH");
					break;
				/* 3 Bilder Formatname "3auQboQdmH" */
				case "3auQboQdmH":
					b_Ecken = new List<AlbumPunkt>{
						new(h1, v3),
						new(h2, v1),
						new(h4, v6)
				};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
						new List<AlbumPunkt>{
						new(h1, v1),
						new(h3, v13)
				 	},
				 3, "3auQboQdmH", "QQH");
					break;
				/* 3 Bilder Formatname "3amHcuQdoQ" */
				case "3amHcuQdoQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h5,v6),
						new(h3,v3),
						new(h4,v1)
				};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
						new(h1, v1),
						new(h14, v3)
				 	},
				 	3, "3amHcuQdoQ", "HQQ");
					break;
				/* 3 Bilder Formatname "3auQcoHduH" */
				case "3auQcoHduH":
					b_Ecken = new List<AlbumPunkt>{
						new(h1,v3),
						new(h12,v1),
						new(h8,v6)
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken,
				 new List<AlbumPunkt>{
						new(h1, v1),
						new(h3, v13)
				 	},
				 	3, "3auQcoHduH", "QHH");
					break;

				/* 3 Bilder Formatname "3amHbuQdmH" */
				case "3auHbmQdoH":
					b_Ecken = new List<AlbumPunkt>{
						new(h1,v9), /* v9: Hochkantbild ganz unten; */
						new(h7,v9),
						new(h13,v1) /* v1: Oberste Position; */
				};
					vorschauSeite = new AlbumVorschauSeite
				(b_Ecken,
				 new List<AlbumPunkt>{
					 	new(h7, v6),
					 	new(h13, v7)
				 },
				 3, "A3amHbuQdmH", "HQH");
					break;
				/* 3 Bilder Formatname "3auHcmHdoH" */
				case "3auHcmHdoH":
					b_Ecken = new List<AlbumPunkt>{
						new(h1, v9),
						new(h9, v10),
						new(h10, v1)
					};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 	new(h1, v1),
					 	new(h10, v11)
				 	},
				 	3, "3auHcmHdoH", "HHH");
					break;
				/* 2 Bilder Formatname "2boQcuQ" */
				case "2boQcuQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h5, v6),
						new(h6, v14)
					};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
						new(h6, v6),
					 	new(h5, v15)
				 },
				 2, "2boQcuQ", "QQ");
					break;
				/* 2 Bilder Formatname "2buQcoQ" */
				case "2buQcoQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h5, v14),
						new(h6, v6)
					};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 	new(h5, v6),
					 	new(h6, v15)
				 	},
				 	2, "2buQcoQ", "QQ");
					break;
				/* 2 Bilder Formatname "2bmQcoH" */
				case "2bmQcoH":
					b_Ecken = new List<AlbumPunkt>{
						new(h5, v12),
						new(h4, v6)};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 	new(h5, v6),
					 	new(h3, v13)
				 	},
				 	2, "2bmQcoH", "QH");
					break;

				/* 2 Bilder Formatname "2boHcmQ" */
				case "2boHcmQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h5, v6),
						new(h3, v12)
					};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 new(h5, v1),
					 new(h3, v6)
				 	},
				 	2, "2boHcmQ", "HQ");
					break;
				/* 2 Bilder Formatname "2buHcoH" */
				case "2buHcoH":
					b_Ecken = new List<AlbumPunkt>{
						new(h5, v4),
						new(h6, v1)};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 	new(h5, v1),
					 	new(h3, v5)
				 	},
				 	2, "2buHcoH", "HH");
					break;
				/* 1 Bild Formatname "1cmQ" */
				case "1cmQ":
					b_Ecken = new List<AlbumPunkt>{
						new(h7, v4)};
					vorschauSeite = new AlbumVorschauSeite
					(b_Ecken,
				 	new List<AlbumPunkt>{
					 	new(h7, v16),
					 	new(h15, v16)
				 	},
				 	1, "1cmQ", "Q");
					break;
				default:
					Console.WriteLine("Damit ist alles schief gelaufen!");
					break;
			}
			VorschauSeite = vorschauSeite;
			Console.WriteLine("Die Vorschauseite wurde erfolgreich erstellt.");
		}
	}
}

