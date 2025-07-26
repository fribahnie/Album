
using System;
using System.Runtime.InteropServices;
using Startfenster;

namespace ModulePlattform
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
	public class DiePlattform
	{
		public static void BestimmePlattform()
		{
			Console.WriteLine("Wir beginnen damit, die Plattform zu bestimmen.");
			// ermittelt die verwendete Plattform:
			int plattform = -1;
			// hack because of this: https://github.com/dotnet/corefx/issues/10361
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				plattform = 0;
				Console.WriteLine("Wir schaffen mit Windows.");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				plattform = 1;
				Console.WriteLine("Wir schaffen mit Linux.");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				plattform = 2;
			}
			StartFenster.Plattform = plattform;                  // Wertzuweisung 0, 1 od. 2
			Console.WriteLine("Der Plattform wurde der Wert {0} zugewiesen", plattform);
		}
	}
}