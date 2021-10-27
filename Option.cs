﻿using System;
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
		public string StateChange { get; set; }

		public Option(string text, string jumpTo, string stateChange)
		{
			Text = text;
			JumpTo = jumpTo;
			StateChange = stateChange;
		}
		
		public override string ToString()
		{
			return string.Format("{0} -> {1}, {2}", Text, JumpTo, StateChange);
		}
	}
}
