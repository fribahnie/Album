/* 
   Die Klasse 'AlbumFormennamen' enthält die Methode 'Formenname()'.
   Mit ihr wird jedem Wert von 'senderindex' ein 'formenname'
   zugeordnet. Sie gibt anhand von 'senderindex' 'formenname' zurück.
   Die Methode wird aufgerufen von der Methode 'NeueSeite()'
   der Klasse 'AlbumSeite'. 
	  
   'formenname' bestimmt später das konkrete Layout
   für die Vorschauseite.
   Das Layout wird definiert durch den Aufruf von
   'AlbumFormate.HoleEckpunkte(formenname)'. 
*/

using System;
using System.Collections.Generic;

namespace BasicClasses
{

	public class AlbumFormennamen
	{
		/*
			'senderindex' stammt aus der 'itemliste' in
			'albumApp2.cs'.
			Liefert den Formennamen 'formenname' für die neue Vorschauseite:
		*/

		public static string Formenname(int senderindex)
		{
			string formenname = "";
			/* Create a new dictionary. */

			Dictionary<int, string> formennamen =
		new();

			formennamen.Add(0, "4aoQcoQbuQduQ");
			formennamen.Add(1, "4boQdoQauQcuQ");
			formennamen.Add(2, "3aoQbuQdmQ");
			formennamen.Add(3, "3amQcuQdoQ");
			formennamen.Add(4, "3aoQbuQdmH");
			formennamen.Add(5, "3auQboQdmH");
			formennamen.Add(6, "3amHcuQdoQ");
			formennamen.Add(7, "3auQcoHduH");
			formennamen.Add(8, "3auHbmQdoH");
			formennamen.Add(9, "3auHcmHdoH");
			formennamen.Add(10, "2boQcuQ");
			formennamen.Add(11, "2buQcoQ");
			formennamen.Add(12, "2bmQcoH");
			formennamen.Add(13, "2boHcmQ");
			formennamen.Add(14, "2buHcoH");
			formennamen.Add(15, "1cmQ");

			// The indexer throws an exception if the requested key is
			// not in the dictionary.
			try
			{
				formenname = formennamen[senderindex];
				Console.WriteLine("For key = {0}, value = {1}.", senderindex, formenname);
			}
			catch (KeyNotFoundException)
			{
				Console.WriteLine("Key = {0} is not found.", senderindex);
			}
			return formenname;
		}
	}
}
