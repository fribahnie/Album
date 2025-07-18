
using System;
using Startfenster;
using AlbumBasis;

namespace ModuleHtml
{

  internal class Bilderpfad
  {
    /* Gibt den bearbeiteten Bildpfad zurück. Berücksichtigt, auf welcher Plattform
      das Programm läuft und ob es ein 'Baukastenbild' ist oder ein 'Originalbild'.
     */
    public static string PfadArbeiten(string pfad)
    {
      /* Speichere den unveränderten Pfad der Bilddatei: */
      string oldpfad = pfad;

      // wandle den Dateinamen der Bilddatei um für das Kopieren ins Fotoalbum:
      string newpfad = AlbumNeuesBild.CreateBilderpfad(pfad);
      bool istBaukastenBild = AlbumNeuesBild.IstBaukastenBild;
      string vomAlbumBildzumAlbumOrdner = @"../../../../";

      if (StartFenster.Plattform == 0) /* Windows  */
      {
        // Bearbeite die Pfadtrenner von Windows:
        pfad = pfad.Replace("\\", "/");
        if (!istBaukastenBild)
        {
          if (RelativPaths.AlternativOrdnerBool) /* Alternatilaufwerk existiert. z.B. Z: */
          {
            pfad = XMLDoc.CopyImagesBool ? @"../../Bilder/" + newpfad : @"file:///" + oldpfad;
          }
          else /* Kein Alternativlaufwerk */
          {
            pfad = vomAlbumBildzumAlbumOrdner + oldpfad;
          }
        }
        else /* ist ein Baukastenbild; */
        {
          pfad = vomAlbumBildzumAlbumOrdner + pfad;
        }
        Console.WriteLine("Baukastenbild? {0} Z:? {1} Copy? {2} pfad: {3}",
          istBaukastenBild,
          RelativPaths.AlternativOrdnerBool,
          XMLDoc.CopyImagesBool,
          pfad
        );
      }

      if (StartFenster.Plattform == 1)/* Linux */
      {
        if (!istBaukastenBild)
        {
          string zwischenstring;
          if (XMLDoc.CopyImagesBool) /* kopiere! */
          {
            // Linux: Kopiert die Originalbilder ins Fotoalbum:
            zwischenstring = @"Fotoalben/" + XMLDoc.Albumname + "/Bilder/";
            pfad = newpfad;
          }
          else                             /* nicht kopieren! */
          {
            zwischenstring = string.Empty;
            pfad = oldpfad;
          }
          pfad = vomAlbumBildzumAlbumOrdner + zwischenstring + pfad;
        }
        else /* ist ein Baukastenbild: */
        {
          pfad = vomAlbumBildzumAlbumOrdner + pfad;
        }
      }
      return pfad;
    }
  }
}
