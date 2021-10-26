using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bloody_Day
{
	class Game
	{
		public void StartGame()
		{
			Dictionary<string, Event> events = JsonParser.ParseEvents();

			GoThroughEvent(ref events, "EventStart");
		}

		public void GoThroughEvent(ref Dictionary<string, Event> events, string happening)
		{
			Console.Clear();
			Console.WriteLine(events[happening].Description);
			int i = 0;
			foreach (Option option in events[happening].Options)
			{
				i++;
				Console.WriteLine(i + " -> " + option.Text);
			}

			bool keyValid = false;
			while (keyValid == false)
			{
				ConsoleKeyInfo pressedKey = Console.ReadKey(true);
				if (int.TryParse(pressedKey.KeyChar.ToString(), out int number) && number <= i)
				{
					keyValid = true;
					string whereTo = events[happening].Options[number - 1].JumpTo;

					if (whereTo == "MainMenu")
					{
						return;
					}
					else if (whereTo != string.Empty)
					{
						GoThroughEvent(ref events, whereTo);
					}
					else
					{
						Die();
					}
				}
				else
				{
					Console.WriteLine("\nPlease press a number between 1 and " + i);
				}
			}
		}

		public void Die()
		{
			Console.WriteLine("You die");
			Console.WriteLine("Press any key to go back to main menu...");
			Console.ReadKey(true);
			Console.Clear();
			return;
		}
	}
}
