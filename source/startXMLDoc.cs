using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Startfenster
{
  public class XMLDoc
  {
    public static string AlbumRootPath { set; get; }// Pfad zu dem Programm
    public static string Sep { set; get; }// Seperator in Dateipfaden
    public static string CopyImages { set; get; }// 'false' oder 'true' in pfade.xml
    public static string Aktiv { set; get; }// 'false' oder 'true' in pfade.xml
    public static string Bildformat { set; get; }// z.B. '16x12' für 'AlbumMenu'
    public static int Drehwinkel { set; get; }
    public static string LangDefault { get; set; }// Sprache der Textbausteine
    public static bool CopyImagesBool { set; get; } // sollen die Bilder ins Album kopiert werden?
    public static string HomeBilder { set; get; }// z.B.:  'C:\Users\fbahr\'
    public static string FotoRootDir { set; get; }// Fotoalben-Verzeichnis
    public static string FotoalbenPath { set; get; }// Pfad zu den Fotoalben
    public static string Albumname { set; get; }// Name des Albums
    public static string DefaultName { set; get; }// Defaultname des Albums

    public static void MainXMLDoc()
    {
      Wertzuweisungen(LiesDefaultWerteEin());
      WerteDefaultWerteAus();
    }
    static List<string> LiesDefaultWerteEin()
    {
      List<string> innerText = [];
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
      innerText.Add(copyimages.InnerText);
      innerText.Add(aktiv.InnerText);
      innerText.Add(bildformat.InnerText);
      innerText.Add(drehwinkel.InnerText);
      innerText.Add(lang_default.InnerText);
      innerText.Add(alternatdirectory.InnerText);
      return innerText;
    }

    static void Wertzuweisungen(List<string> innerText)
    {
      CopyImages = innerText[0]; // sollen die Bilder ins Album kopiert werden?
      Aktiv = innerText[1];         // absolute od. relat. Pfade
      Bildformat = innerText[2];    // '16x12' od. '16x09'
      Drehwinkel = int.Parse(innerText[3]); // 270 oder 90 Grad
      LangDefault = innerText[4];  // Vorgabe Sprache
      RelativPaths.AltDirectory = innerText[5];
    }

    static void WerteDefaultWerteAus()
    {
      CopyImagesBool = CopyImages == "true"; // je nach Einstellung: true oder false
      if (RelativPaths.AltDirectory != string.Empty && StartFenster.Plattform == 0)
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
  }
}

