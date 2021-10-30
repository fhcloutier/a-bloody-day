using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Bloody_Day
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("Welcome to \"A Bloody Day\"\n");
				Console.WriteLine("Please enter one of the following keys:");
				Console.WriteLine("1 -> Start a new game");
				Console.WriteLine("2 -> Quit\n");

				ConsoleKeyInfo keyPressed = Console.ReadKey(true);

				if (keyPressed.KeyChar == GameOption.Option1)
				{
					new Game().Load();
					//new Game().StartGame();
					Console.Clear();
				}
				else if (keyPressed.KeyChar == GameOption.Option2)
				{
					break;
				}
				else
				{
					Console.WriteLine("The key you entered is not recognized.\n\n");
				}
			}
		}
	}

	public sealed class GameOption
	{
		public readonly static GameOption Option1 = new GameOption('1');
		public readonly static GameOption Option2 = new GameOption('2');

		private readonly char _option;

		public GameOption(char option)
		{
			_option = char.ToLower(option);
		}

		public static implicit operator char(GameOption option) => option._option;
		
	}
}
