using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bloody_Day
{
	class Option
	{
		public string Text { get; set; }
		public string JumpTo { get; set; }

		public Option(string text, string jumpTo)
		{
			Text = text;
			JumpTo = jumpTo;
		}

		public override string ToString()
		{
			return string.Format("{0} -> {1}", Text, JumpTo);
		}
	}
}
