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

		public void StartGame(string firstEvent = "EventStart")
		{
			Events = JsonParser.ParseEvents();

			//Console.WriteLine(JsonParser.CreateJson());
			//Console.ReadKey();

			GoThroughEvent(firstEvent);
		}

		public void GoThroughEvent(string currentEvent)
		{
			Console.Clear();
			Console.WriteLine(Events[currentEvent].Description);
			int optionCount = Events[currentEvent].Options.Count;
			for (int i = 0; i < optionCount; i++)
			{
				Events[currentEvent].Options[i] = InterpretConditions(Events[currentEvent].Options[i]);

				Console.WriteLine((i + 1) + " -> " + Events[currentEvent].Options[i].Text);
			}

			bool keyValid = false;
			while (keyValid == false)
			{
				ConsoleKeyInfo pressedKey = Console.ReadKey(true);
				if (int.TryParse(pressedKey.KeyChar.ToString(), out int number)
					&& 1 <= number && number <= optionCount)
				{
					keyValid = true;

					ChangeState(Events[currentEvent].Options[number - 1].StateChange);

					string whereTo = Events[currentEvent].Options[number - 1].JumpTo;

					GoToNextStep(whereTo);
				}
				else if (pressedKey.KeyChar == 'q')
				{
					keyValid = true;
					Save(currentEvent);
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

		private void Save(string currentEvent)
		{
			string saveData = "{\"Event\":\""+currentEvent+"\",\"States\":[";

			foreach (bool state in States.Values)
			{
				saveData += Convert.ToInt32(state) + ",";
			}
			saveData = saveData.Remove(saveData.Length - 1);
			saveData += "]}";

			FileManager.WriteToFile("save", saveData);
		}

		public void Load()
		{
			string loadData = FileManager.ReadFromFile("save");

			SavedGame save = JsonParser.ParseLoadData(loadData);

			int i = 0;
			List<string> keys = new List<string>(States.Keys);
			foreach (string key in keys)
			{
				States[key] = Convert.ToBoolean(save.States[i]); ;
				i++;
			}

			StartGame(save.Event);
		}
	}
}
