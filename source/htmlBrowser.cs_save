using System;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Startfenster;

namespace ModuleHtml
{
	class AlbumBrowser
	{
		public static void OnBrowserAufruf(object o, System.EventArgs args)
		{
			/*
	      Startet den Default-Browser der Plattform mit der
	      momentanen Seite des Albumprogramms.

	      Verwendete Felder aus der Klasse 'StartFenster':
              FotoalbenPath    { set; get; } // Pfad zu den Fotoalben
              Bereiche         { set; get; } // Array: { "mittel", "gross", "vierk" };
              Default          { set; get; } // Defaultgröße für die html-Ausgabe
              Sep              { set; get; } // Seperator in Dateipfaden je nach OS
              Albumname        { set; get; } // Albumname

	      Feld aus der Klasse 'AlbumApp':
	      Seitennr         { set; get; } // Nummer der aktuellen Seite (beginnt mit 1)
	    */

			string fotoalbenpfad = string.Empty;

			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			// Für den Aufruf als 'url' braucht es diese Erweiterung:
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				fotoalbenpfad = "file:///" + StartFenster.FotoalbenPath;
				fotoalbenpfad = fotoalbenpfad.Replace("&", "^&");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				fotoalbenpfad = "file://" + StartFenster.FotoalbenPath;
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				fotoalbenpfad = "file://" + StartFenster.FotoalbenPath;
			}
			Console.WriteLine("fotoalbenpfad: {0}", fotoalbenpfad);

			string[] groessen = StartFenster.Bereiche;
			int groessezahl = StartFenster.DisplayDefault;
			int seitennr = StartFenster.Seitennr;
			string albumgroesse = groessen[groessezahl];
			string endung = string.Format("{0,4:0000}", seitennr);
			string htmlseite = "seite" + endung + ".html";
			string albumseitenDir = "Albumseiten";
			string groesseDir = albumgroesse;
			string url = Path.Combine(fotoalbenpfad, StartFenster.Albumname);
			url = Path.Combine(url, albumseitenDir);
			url = Path.Combine(url, groesseDir);
			url = Path.Combine(url, htmlseite);
			url = url.Replace(" ", "%20");
			url = url.Replace(StartFenster.Sep, "/");

			Console.WriteLine("AlbumBrowser url = {0}", url);

			OpenUrl(url); // Startet auf der jeweiligen Plattform den Browser
		}


		private static void OpenUrl(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch
			{
				// hack because of this: https://github.com/dotnet/corefx/issues/10361
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					Process.Start(new ProcessStartInfo("xdg-open", url) { UseShellExecute = false });
				}
				else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
				{
					Process.Start("open", url); // vermutlich nicht ausreichend, weil nicht getestet;
				}
				else
				{
					throw;
				}
			}
		}
	}
}
