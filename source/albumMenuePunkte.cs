using System.Collections.Generic;
using Gtk;
using Startfenster;
using System;

namespace AlbumBasis
{
  /* Viele Menüpunkte haben Untermenüs, die hier verarbeitet werden. */
  public class AlbumMenuePunkte
  {
    public static void OnToggeldFotoformat(object sender, EventArgs args)
    {
      /*
        Behandelt die Checkboxen für das Bildformat.
        Typumwandlung: Wer sendet?
        Der Index verrät den Sender:
       */
      var testitem = (Gtk.CheckMenuItem)sender;
      int senderindex = StartFenster.FotoformatCheckboxList.IndexOf(testitem);
      Console.WriteLine("Der Senderindex für das Bildformat ist: {0}", senderindex);
      int zaehler = 0;

      foreach (var eintrag in StartFenster.FotoformatCheckboxList)
      {
        // bisher aktive Checkbox umschalten auf inaktiv:
        int checkindex = StartFenster.FotoformatCheckboxList.IndexOf(eintrag);
        if (eintrag.Active && checkindex != senderindex)
          StartFenster.FotoformatCheckboxList[checkindex].Toggle();
        if (eintrag.Active)
          zaehler++;
      }
      // Wenn keine Box aktiv ist:
      if (zaehler == 0)
        StartFenster.FotoformatCheckboxList[senderindex].Toggle();
    }

    public static void OnToggeldWinkel(object winkelsender, EventArgs args)
    {
      /*
        Behandelt die Checkboxen für den Drehwinkel.
        Typumwandlung: Wer sendet?
        Der Index verrät den Sender:
       */
      var testitem = (Gtk.CheckMenuItem)winkelsender;
      int senderindex = StartFenster.WinkelCheckboxList.IndexOf(testitem);
      Console.WriteLine("Der Senderindex für den Drehwinkel ist: {0}", senderindex);
      int zaehler = 0;

      foreach (var eintrag in StartFenster.WinkelCheckboxList)
      {
        // bisher aktive Checkbox umschalten auf inaktiv:
        int checkindex = StartFenster.WinkelCheckboxList.IndexOf(eintrag);
        if (eintrag.Active && checkindex != senderindex)
          StartFenster.WinkelCheckboxList[checkindex].Toggle();
        if (eintrag.Active)
          zaehler++;
      }
      // Wenn keine Box aktiv ist:
      if (zaehler == 0)
        StartFenster.WinkelCheckboxList[senderindex].Toggle();
      StartFenster.Drehwinkel = StartFenster.DrehwinkelArray[senderindex];
      Console.WriteLine("Der Drehwinkel wurde auf {0} Grad gesetzte", StartFenster.Drehwinkel);
    }

    public static void DisplayMenuePunkt(string displaywidth, Menu untermenu2, List<Gtk.CheckMenuItem> displaycheckboxList)
    {
      var checkitem = new Gtk.CheckMenuItem(displaywidth);
      checkitem.Activated += OnDisplayGroesse;
      displaycheckboxList.Add(checkitem);
      StartFenster.DisplayCheckboxList = displaycheckboxList;
      untermenu2.Append(checkitem);
    }



    public static void OnDisplayGroesse(object sender, EventArgs args)
    {
      Gtk.CheckMenuItem testitem = (Gtk.CheckMenuItem)sender;     /* Typumwandlung: Wer sendet? */
      int zwischenwert = StartFenster.DisplayCheckboxList.IndexOf(testitem);/* der Index verrät den Sender */
      StartFenster.DisplayDefault = zwischenwert;              /* übergibt die Groessezahl */
      Console.WriteLine("Größenzahl: {0}", StartFenster.DisplayDefault);
      int zaehler = 0;

      foreach (var eintrag in StartFenster.DisplayCheckboxList)
      {
        // bisher aktive Checkbox umschalten auf inaktiv:
        int checkindex = StartFenster.DisplayCheckboxList.IndexOf(eintrag);
        if (eintrag.Active && checkindex != zwischenwert)
          StartFenster.DisplayCheckboxList[checkindex].Toggle();
        if (eintrag.Active)
          zaehler++;
      }
      // Wenn keine Box aktiv ist:
      if (zaehler == 0)
        StartFenster.DisplayCheckboxList[zwischenwert].Toggle();
      StartFenster.DisplayDefault = zwischenwert;
      Console.WriteLine("Die Größenzahl ist nun: {0}", StartFenster.DisplayDefault);
    }

    public static void FotoformatMenuePunkt(string bildformat, List<Gtk.CheckMenuItem> checkboxList, Gtk.Menu unteruntermenu)
    {
      var checkitem = new Gtk.CheckMenuItem(bildformat);
      checkitem.Activated += OnToggeldFotoformat;
      checkboxList.Add(checkitem);
      unteruntermenu.Append(checkitem);
    }

    public static Gtk.Menu Bilderzahl(string beschriftung, Gtk.Menu untermenu)
    {
      var item = new Gtk.MenuItem(beschriftung);
      var formatbild = new Gtk.Menu();
      item.Submenu = formatbild;
      untermenu.Append(item);
      return formatbild;
    }

    public static void SeitenformatMenuPunkt(Box bilderbox, Menu formatbild, List<Gtk.MenuItem> formateList)
    {
      var item = new Gtk.MenuItem
      {
        bilderbox
      };
      item.Activated += AlbumApp.OnSeiteNeu;
      formateList.Add(item);
      formatbild.Append(item);
    }
  }
}

