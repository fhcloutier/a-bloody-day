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
			Dictionary<string, bool> states = new Dictionary<string, bool> { 
				{ "has_ally", false },
				{ "is_murderer", false },
				{ "deleted_footage", false }
			};
			Dictionary<string, Event> events = JsonParser.ParseEvents();

			GoThroughEvent(ref events, ref states, "EventStart");
		}

		public void GoThroughEvent(
			ref Dictionary<string, Event> events,
			ref Dictionary<string, bool> states,
			string happening
			)
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

					ChangeState(events[happening].Options[number - 1].StateChange, ref states);

					string whereTo = events[happening].Options[number - 1].JumpTo;

					GoToNextStep(ref events, ref states, whereTo);
				}
				else
				{
					Console.WriteLine("\nPlease press a number between 1 and " + i);
				}
			}
		}

		private void GoToNextStep(ref Dictionary<string, Event> events, ref Dictionary<string, bool> states, string whereTo)
		{
			if (whereTo == "MainMenu")
			{
				return;
			}
			else if (whereTo != string.Empty)
			{
				GoThroughEvent(ref events, ref states, whereTo);
			}
			else
			{
				Die();
			}
		}

		private void ChangeState(string stateToChange, ref Dictionary<string, bool> states)
		{
			if (!(stateToChange is null) && stateToChange != "")
			{
				states[stateToChange] = !states[stateToChange];
			}
		}

		private void Die()
		{
			Console.WriteLine("You die");
			Console.WriteLine("Press any key to go back to main menu...");
			Console.ReadKey(true);
			Console.Clear();
			return;
		}
	}
}
