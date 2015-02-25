using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BrightnessControll
{
	class Program
	{
		private const String QUIT = "quit";
		private const string pattern = @"\d+";

		private static Regex regex = new Regex(pattern);

		static void Main(string[] args)
		{
			Console.WriteLine("***Monitor Brightness Controller***\nEnter desired brightness in range [0-255]\nTo quite type 'quit'\n");
			Console.Write("brightness: ");
			string consoleText = Console.ReadLine();
			Match match;
			short inputValue;
			while (!consoleText.Equals(QUIT))
			{
				match = regex.Match(consoleText);
				if (!match.Success)
					continue;
				try 
				{
					inputValue = short.Parse(match.Value);
					BrightnessController.SetBrightness(inputValue);
				}
				catch (Exception exc)
				{
					Console.WriteLine(exc.Message);
				}
				Console.Write("brightness: ");
				consoleText = Console.ReadLine();
			}
			Console.WriteLine("Exiting");
		}
	}
}
