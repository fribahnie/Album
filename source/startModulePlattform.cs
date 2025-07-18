
using System;
using System.Runtime.InteropServices;
using Startfenster;

namespace ModulePlattform
{
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