using System;
using System.IO;
using System.Collections.Generic;
using Gtk;
using Gdk;
using Startfenster;
using BasicClasses;

namespace AlbumBasis
{
  public class AlbumNeuesBild
  {
    /*
      Die Klasse 'AlbumNeuesBild' enthält die übergeordnete Methode 
      'SetzeHandler', die ihrerseits weitere Methoden der Klasse aufruft.  
      Die Klasse dient dazu, dass in eine Vorschauseite ein neues Bild 
      eingesetzt werden kann, entweder per Drag and Drop oder 
      über den eingebauten Dateimanager, der über das Clipboard 
      und die 3. Maustaste das Bild ins gewünschte Bilderfeld einfügt.
      Die Klasse wird von ganz unterschiedlichen Methoden aufgerufen.

      Die Methoden der Klasse und wie sie ineinandergreifen:
      'SetzeHandler()' schafft die Voraussetzungen für:
        'OnDragDataReceived()'; ruft 
          'SchneideBilderpfad()' und
          'LoescheAlteBilddateier()' und
          'LoescheHeader()' und 
          'IsImageFile() und 
          'ZeigeBild()' auf;
        'OnMouseClick()'; benutzt ebenfalls 'ZeigeBild()';
          'ZeigeBild()' verwendet 'SkaliereBild()';
            'SkaliereBild()' ruft 'BerechneLaenge()' auf;
    */
    public static AlbumSeite Seite { set; get; }        /* Die aktuelle Seite des Albums */
    public static bool IstBaukastenBild { set; get; }

    public static AlbumSeite SetzeHandler(AlbumSeite seite)   /* Handler für TextView und */
    {                                                   /* EventBoxes */
      List<Gtk.EventBox> eboxliste = seite.Eboxliste;

      foreach (Gtk.EventBox ebox in eboxliste)
      {
        /* die Ziele: */
        Gtk.Drag.DestSet(ebox, DestDefaults.All,
        new TargetEntry[]{
          new TargetEntry("text/uri-list", 0, 0)
        }, Gdk.DragAction.Copy);

        ebox.DragDataReceived                           /* fügt ein neues Bild ein */
            += new Gtk.DragDataReceivedHandler(OnDragDataReceived);
        ebox.ButtonPressEvent
            += new ButtonPressEventHandler(OnMouseClick);
      }
      // Seite = seite;
      return seite;
    }

    /* Wenn ein Bild importiert wird per Drag and Drop: */
    static void OnDragDataReceived(object sender, Gtk.DragDataReceivedArgs args) /* das importierte Bild */
    {
      AlbumSeite seite = AlbumRead.Seitenliste[AlbumBuildSeite.SeitenListenIndex];
      // seite = Seite;                         /* die aktuelle Albumseite */
      List<EventBox> tempEboxList = seite.Eboxliste;
      /* Typumwandlung: welche          */
      /* ebox soll die Daten empfangen? */
      Gtk.EventBox ebox = (Gtk.EventBox)sender;
      int eboxindex = tempEboxList.IndexOf(ebox);
      Console.WriteLine("der 'eboxindex' ist {0}", eboxindex);

      string oldfile = seite.Bilderliste[eboxindex].Datei; /* die bisherige Bilddatei */
      // oldfile = Path.GetFullPath(oldfile);
      oldfile = CreateBilderpfad(oldfile);
      LoescheAlteBilddateien(oldfile);

      string jpg_file = string.Empty;
      if (args.SelectionData.Length >= 0                /* wenn Daten übertragen wurden */
          && args.SelectionData.Format == 8)
      {
        string files = System.Text.Encoding.UTF8.GetString(args.SelectionData.Data);
        files = files.Replace('\r', '\n');
        files = files.Replace("%20", " ");              /* bei Leerzeichen im Pfad */
        string[] fileArray = files.Split('\n');         /* wandelt files in ein Array *a
		
		    /*  es reicht hier, fileArray[0] zu untersuchen: */

        jpg_file = fileArray[0];

        char trenn = '\0';                              /* JPhotoTagger mit xmp-Datei       */
        fileArray = jpg_file.Split(trenn);              /* wandelt ggf. in ein Array.       */
        string fileUri = fileArray[0];                  /* Die xmp-Datei interessiert nicht */
        string filePath = new Uri(fileUri).LocalPath;
        jpg_file = LoescheHeader(filePath);             /* falls file mit 'file://' beginnt */
        Console.WriteLine("Das komplette File: {0}", jpg_file);
        if (!RelativPaths.AlternativOrdnerBool)
        {
          // wandle in einen relativen Pfad um:
          int bilderpfadlaenge = RelativPaths.BilderPfadLaenge;
          string origstring = jpg_file[..bilderpfadlaenge];
          jpg_file = jpg_file.Replace(origstring, RelativPaths.RelPfad);
          Console.WriteLine("Das bearbeitete file: {0}", jpg_file);
        }
      }
      if (IsImageFile(jpg_file))                        /* prüft, ob Bilddatei; wenn true */
      {
        /* ruft die zuständige Methode auf: */
        ZeigeBild(ebox, jpg_file);
      }

      Gtk.Drag.Finish(args.Context, false, false, args.Time);
    }


    public static bool IsImageFile(string prevfile)     /* file-Endung: Bilddatei? */
    {
      bool is_image = false;
      string result = prevfile.ToLower();
      string[] endArray = {
    ".jpg", ".jpeg", ".png", ".pnm",
    ".bmp", ".tiff", ".ppm"
      };

      foreach (string item in endArray)
      {
        if (result.EndsWith(item))
        {
          is_image = true;
          return is_image;
        }
      }
      return is_image;
    }


    static string LoescheHeader(string jpg_file)
    {
      int[] loescheArray = { 8, 7, 7 };    /* unterscheide 'file://' und 'file:///' */
      /*
        Unterscheide je nach Betriebssystem, wieviel gelösch wird:
      */
      int loesche;
      if (-1 < StartFenster.Plattform && StartFenster.Plattform < 3)
        loesche = loescheArray[StartFenster.Plattform];
      else
      {
        Console.WriteLine("Die Plattform wurde nicht erkannt.");
        loesche = 7;
      }

      if (jpg_file.StartsWith("file://"))
      {
        jpg_file = jpg_file[loesche..];         /* 'loesche' je nach OS */
      }
      return jpg_file;
    }

    /*
      Bildauswahl per eingebautem Dateimanager und Clipboard.
      Wähle mit der rechten Maustaste, in welches Bildfeld das Bild
      eingefügt werden soll:
     */
    static void OnMouseClick(object sender, ButtonPressEventArgs args)
    {
      /* Typumwandlung: von welcher ebox stammt das Signal?  : */
      Gtk.EventBox ebox = (Gtk.EventBox)sender;
      /* Wenn das Clipboard benutzt wird: */
      if (args.Event.Button == 3)                       /* rechte Maustaste */
      {
        Gtk.Clipboard clipboard = Gtk.Clipboard.Get(Gdk.Atom.Intern("CLIPBOARD", false));
        string pastetext = clipboard.WaitForText();
        // Console.WriteLine(pastetext);
        if (pastetext != string.Empty)
        {
          // Der absolute Pfad muss noch in einen relativen umgewandelt werden:
          Console.WriteLine("Das komplette File: {0}", pastetext);
          int bilderpfadlaenge = RelativPaths.BilderPfadLaenge;
          string origstring = pastetext[..bilderpfadlaenge];
          string jpg_file = pastetext.Replace(origstring, RelativPaths.RelPfad);
          Console.WriteLine("Das bearbeitete file: {0}", jpg_file);
          ZeigeBild(ebox, jpg_file);
        }
      }
    }

    /*
      Zeigt das per DragAndDrop ausgewählte Bild in dem
      ausgesuchten Bildfeld. Funktioniert mit jpg-Bildern und
      png-Bildern vom JPhotoTagger und vom Dateimanager.
    */
    static void ZeigeBild(Gtk.EventBox ebox, string jpg_file)
    {
      int seitenindex = AlbumBuildSeite.SeitenListenIndex; // Index der aktuellen Seite
      AlbumSeite seite = AlbumRead.Seitenliste[seitenindex]; // die aktuelle Seite
      List<EventBox> eboxList = seite.Eboxliste;
      int eboxindex = eboxList.IndexOf(ebox);
      // Console.WriteLine("vor Absturz. Der Index: {0} Listlänge: {1} Seitenindex: {2}", eboxindex, eboxList.Count, seitenindex);
      AlbumBild mybildle = seite.Bilderliste[eboxindex];    /* welches Bild wird ersetzt? */
      ebox.Remove(ebox.Child);                          /* entfernt das bisherige Bild */
      Gtk.Image image = SkaliereBild(jpg_file, mybildle.Richtung); /* Original => Vorschaubild */
      ebox.Add(image);                                  /* füge Bild in EventBox */
      mybildle.Datei = jpg_file;                        /* speichert Datei */
      mybildle.Bild = image;                            /* speichert image */
      mybildle.BaukastenBild = false;
      image.Show();
    }

    /*
      verkleinert das Originalbild zum Vorschaubild:
    */
    public static Gtk.Image SkaliereBild(string jpg_file, string richtung)
    {
      string jpegNewFileName = CreateBilderpfad(jpg_file);
      string pfadmini = XMLDoc.FotoalbenPath + XMLDoc.Sep +
                  XMLDoc.Albumname + XMLDoc.Sep +
                  "Vorschaubilder";
      string pfadgross = XMLDoc.FotoalbenPath + XMLDoc.Sep +
          XMLDoc.Albumname + XMLDoc.Sep +
          "Bilder";
      Console.WriteLine("'bildergross':  {0}", pfadgross);
      string pfadbildmini = pfadmini + XMLDoc.Sep + jpegNewFileName;
      string pfadbildgross = pfadgross + XMLDoc.Sep + jpegNewFileName;
      Gtk.Image image = new();
      Gdk.Pixbuf pixbuf;
      if ((!File.Exists(pfadbildgross)) && XMLDoc.CopyImagesBool)
      {
        // Kopiere die Bilddatei ins Fotoalbum. Geschwindigkeitsvorteil beim Anschauen.
        // Console.WriteLine("Kopiere {0} nach {1}", jpg_file, filebild);
        File.Copy(jpg_file, pfadbildgross);
      }

      if (File.Exists(pfadbildmini))
      {
        // Wenn das Vorschaubild bereits existiert:
        pixbuf = new(pfadbildmini);
      }
      else
      {
        // das Bild wird vom Original eingelesen:
        pixbuf = new(jpg_file);                /* ergibt das neue Bild  */
      }

      int vorschau_breite = 0;
      int vorschau_hoehe = 0;

      //Console.WriteLine("Vorschaubreite: {0} Vorschauhöhe: {1}", pixbuf.Width, pixbuf.Height);
      int vorschau_breite_save = AlbumRead.Vorschauarray[0]; /* Wertzuweisung */
      if (richtung.CompareTo("Q") == 0)                 // Querformatbild
      {
        // Console.WriteLine("Es handelt sich um ein Querformatbild");
        int grosser_wert = pixbuf.Width;
        int kleiner_wert = pixbuf.Height;
        vorschau_hoehe = Berechne_Laenge(vorschau_breite_save, grosser_wert, kleiner_wert);
        vorschau_breite = vorschau_breite_save;
      }

      if (richtung.CompareTo("H") == 0)                   // Hochformatbild
      {
        if (pixbuf.Width >= pixbuf.Height) // true: Das Bild muss gedreht und 'vorschau_breite' angepasst werden;
        {
          Console.WriteLine("Es handelt sich um ein Hochkantbild; es muss gedreht werden.");
          double var_ergebnis = Math.Round((double)pixbuf.Height / pixbuf.Width * vorschau_breite_save, 0);
          vorschau_breite_save = Convert.ToInt32(var_ergebnis);
          int grosser_wert = pixbuf.Height;
          int kleiner_wert = pixbuf.Width;
          vorschau_breite = Berechne_Laenge(vorschau_breite_save, grosser_wert, kleiner_wert);
          vorschau_hoehe = vorschau_breite_save;
        }
        else
        {
          Console.WriteLine("Es handelt sich um ein Hochkantbild; es muss  n i c h t  gedreht werden.");
          double var_ergebnis = Math.Round((double)pixbuf.Width / pixbuf.Height * vorschau_breite_save, 0);
          vorschau_breite_save = Convert.ToInt32(var_ergebnis);
          int grosser_wert = pixbuf.Width;
          int kleiner_wert = pixbuf.Height;
          vorschau_hoehe = Berechne_Laenge(vorschau_breite_save, grosser_wert, kleiner_wert);
          vorschau_breite = vorschau_breite_save;
        }
      }

      /*
        erstellt das Vorschaubild durch Skalieren und evtl. mit Rotation:
      */
      image.Pixbuf = pixbuf.ScaleSimple(vorschau_breite, vorschau_hoehe, Gdk.InterpType.Hyper);
      if (image.Pixbuf.Width >= image.Pixbuf.Height && richtung.CompareTo("H") == 0)
      {
        Console.WriteLine("Der Drehwinkel beträgt {0} Grad.", XMLDoc.Drehwinkel);
        image.Pixbuf = image.Pixbuf.RotateSimple((PixbufRotation)XMLDoc.Drehwinkel);
      }
      //Console.WriteLine("Das image ist erstellt und wird zurückgegeben.");
      // Speichert das Vorschaubild im Vorzeichnis "Vorschaubilder" des Fotoalbums.
      image.Pixbuf.Save(pfadmini + "/" + jpegNewFileName, "jpeg");
      return image;
    }


    static int Berechne_Laenge(int vorschau_breite_save, int grosser_wert, int kleiner_wert)
    {
      double var_ergebnis = Math.Round((double)kleiner_wert / grosser_wert * vorschau_breite_save, 0);
      int ergebnis = Convert.ToInt32(var_ergebnis);
      return ergebnis;
    }


    public static AlbumSeite SetzeBildEin(AlbumSeite seite,
              Gtk.Image image,
              List<AlbumPunkt> bilderEckpunkte,
              int zaehler,
              Gtk.Fixed layout,
              List<Gtk.EventBox> eboxliste)
    {
      // int seitenlisteindex = AlbumBuildSeite.SeitenListenIndex;
      Gtk.EventBox ebox = new();
      ebox.Add(image);                                  // packt das Bild in die Box
      layout.Put(ebox,                                  // platziert das Bild
            bilderEckpunkte[zaehler].XWert,
            bilderEckpunkte[zaehler].YWert);
      seite.Layout = layout;

      eboxliste.Add(ebox);
      seite.Eboxliste = eboxliste;
      int eboxindex = eboxliste.IndexOf(ebox);
      // Seite = seite;

      Console.WriteLine("Die eboxliste hat die Länge {0}; der eboxindex: {1}", eboxliste.Count, eboxindex);
      return seite;
    }

    public static string CreateBilderpfad(string jpg_file)
    {
      // Anpassung an Windows: Ersetze gegebenenfalls "/" durch "\"
      jpg_file = jpg_file.Replace("/", XMLDoc.Sep);
      string vergleichstr = jpg_file[..11]; // Vergleiche die ersten 11 Zeichen des Bild-Dateipfads
      string teststr = "." + XMLDoc.Sep + "Baukasten";
      Console.WriteLine("Vergleichsstring: {0}; Teststring: {1}", vergleichstr, teststr);
      // wenn ein anderes Laufwerk als C: verwendet wird; else: Bilder auf C:
      int schnittstart = RelativPaths.AlternativOrdnerBool ? 3 : RelativPaths.LaengeRelBilderOrdner;
      //int startindex = (vergleichstr == teststr) ? 12 : schnittstart;
      int startindex;
      if (vergleichstr == teststr)
      {
        startindex = 12;
        IstBaukastenBild = true;
      }
      else
      {
        startindex = schnittstart;
        IstBaukastenBild = false;
      }
      if (startindex == 12) Console.WriteLine("Es handelt sich um ein Beispielbild. Startindex: 12");
      else Console.WriteLine("Es handelt sich um ein Normalobild. Startindex {0}", schnittstart);
      // int laenge = jpg_file.Length - startindex;
      string jpegNewFileName = jpg_file[startindex..];
      jpegNewFileName = jpegNewFileName.Replace(XMLDoc.Sep, "qq");
      Console.WriteLine("Das Erg. von SchneideBilderpfad: {0}", jpegNewFileName);
      return jpegNewFileName;
    }


    static void LoescheAlteBilddateien(string oldfile)
    {
      Console.WriteLine("Die Datei soll ersetzt werden: {0}", oldfile);
      string vorschaudirectory = Path.Combine(StartFenster.AlbumnamePath, "Vorschaubilder");
      string bilderdirectory = Path.Combine(StartFenster.AlbumnamePath, "Bilder");
      try
      {
        File.Delete(Path.Combine(vorschaudirectory, oldfile));
        File.Delete(Path.Combine(bilderdirectory, oldfile));
        Console.WriteLine("Gelöscht wurde {0}", Path.Combine(vorschaudirectory, oldfile));
        Console.WriteLine("und {0}", Path.Combine(bilderdirectory, oldfile));
      }
      catch (IOException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}