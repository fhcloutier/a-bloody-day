using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace A_Bloody_Day
{
	class FileManager
	{
		public static void WriteToFile(string fileName, string data, bool overwrite=true)
		{
			if (!overwrite)
			{
				string oldData = File.ReadAllText(fileName);
				data = oldData + data;
			}
			File.WriteAllText(fileName, data);
		}

		public static string ReadFromFile(string fileName)
		{
			return File.ReadAllText(fileName);
		}
	}
}
