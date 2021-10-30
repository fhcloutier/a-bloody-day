using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace A_Bloody_Day
{
	class JsonParser
	{
		public static Dictionary<string, Event> ParseEvents()
		{
			string json = File.ReadAllText(@"Ressources/Events.json");

			return JsonConvert.DeserializeObject<Dictionary<string, Event>>(json);
		}

		public static object ParseLoadData(string data)
		{
			return JsonConvert.DeserializeObject<object>(data);
		}

		public static Event ParseInfo()
		{
			Event test = JsonConvert.DeserializeObject<Event>(@"{" +
				"'Description': 'You wake up. You feel completely wet and like part of your body is under water." +
				"The liquid feels viscous and lukewarm. You open your eyes and the sight horrifies you." +
				"\nThe scene around you feels unreal. You are drenching in an insane amount of blood and you can see dead bodies everywhere.'," +
				"'Options': [" +
					"{'Text':'You get the hell out of the room!', 'JumpTo':'Hallway'}," +
					"{'Text':'You are too shocked to move, you wait a minute.', 'JumpTo':'FirstRoom'}" +
				"]" +
			"}");
			return test;
		}

		public static string CreateJson()
		{
			Event[] events = new Event[2];
			Event test1 = new Event();
			test1.Name = "Start";
			test1.Description = "You wake up. You feel completely wet and ..";
			List<Option> optionsList = new List<Option>(2);
			optionsList.Add(new Option("Hell out", "hallway", "", new Dictionary<string, string> { { "test", "test" } }));
			optionsList.Add(new Option("Too shocked", "FirstRoom", "", new Dictionary<string, string> { { "If", "is_murderer" } }));
			test1.Options = optionsList;

			Event test2 = new Event();
			test2.Name = "Hallway";
			test2.Description = "You sleep...";
			List<Option> optionsList2 = new List<Option>(2);
			optionsList2.Add(new Option("Back out", "outside", "", new Dictionary<string, string> { 
				{ "If", "is_murderer" },
				{"JumpTo", "" }
			}));
			optionsList2.Add(new Option("Too wet", "die", "", new Dictionary<string, string> { { "If", "is_murderer" } }));
			test2.Options = optionsList2;
			events[0] = test1;
			events[1] = test2;

			string json = JsonConvert.SerializeObject(events);

			return json;
		}
	}
}
