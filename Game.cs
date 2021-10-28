using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bloody_Day
{
	class Game
	{
		private Dictionary<string, bool> States { get; } = new Dictionary<string, bool> {
				{ "has_ally", false },
				{ "is_murderer", false },
				{ "deleted_footage", false }
			};

		private Dictionary<string, Event> Events { get; set; }

		public void StartGame()
		{
			Events = JsonParser.ParseEvents();

			//Console.WriteLine(JsonParser.CreateJson());
			//Console.ReadKey();

			GoThroughEvent("EventStart");
		}

		public void GoThroughEvent(string happening)
		{
			Console.Clear();
			Console.WriteLine(Events[happening].Description);
			int optionCount = Events[happening].Options.Count;
			for (int i = 0; i < optionCount; i++)
			{
				Events[happening].Options[i] = InterpretConditions(Events[happening].Options[i]);

				Console.WriteLine((i + 1) + " -> " + Events[happening].Options[i].Text);
			}

			bool keyValid = false;
			while (keyValid == false)
			{
				ConsoleKeyInfo pressedKey = Console.ReadKey(true);
				if (int.TryParse(pressedKey.KeyChar.ToString(), out int number)
					&& 1 <= number && number <= optionCount)
				{
					keyValid = true;

					ChangeState(Events[happening].Options[number - 1].StateChange);

					string whereTo = Events[happening].Options[number - 1].JumpTo;

					GoToNextStep(whereTo);
				}
				else if (pressedKey.KeyChar == 'q')
				{
					break;
				}
				else
				{
					Console.WriteLine("\nPlease press a number between 1 and " + optionCount);
				}
			}
		}

		private void GoToNextStep(string whereTo)
		{
			if (whereTo == "MainMenu")
			{
				return;
			}
			else if (whereTo != string.Empty)
			{
				GoThroughEvent(whereTo);
			}
			else
			{
				Die();
			}
		}

		private void ChangeState(string stateToChange)
		{
			if (stateToChange != null && stateToChange != "")
			{
				States[stateToChange] = !States[stateToChange];
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

		private Option InterpretConditions(Option option)
		{
			if (option.Condition != null)
			{
				if (States[option.Condition["If"]])
				{
					option.GetType().GetProperty(option.Condition["FieldToChange"]).SetValue(option, option.Condition["Value"]);
				}
			}

			return option;
		}
	}
}
