using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bloody_Day
{
	class Event
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<Option> Options { get; set; }

		public override string ToString()
		{
			string joinArray = String.Join("->", Options);
			return string.Format("{0} - {1} - {2}", Name, Description, joinArray);
		}
	}
}
