using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
	public class Program
	{
		private const string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		public void Main(string[] args)
		{
			var key = "";
			var text = "";

			do
			{
				Console.Write("Please enter cipher key (max 7 chars): ");
				key = Console.ReadLine();
			}
			while (key.Length < 1 || key.Length > 7);

			do
			{
				Console.Write("Please enter cipher text: ");
				text = Console.ReadLine();
			}
			while (text.Length == 0);

			Console.WriteLine(Decode(key, text));
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private string Encode(string key, string data)
		{
			var cipher = GetCipherMap(key);
			var result = "";

			data = data.ToUpper();

			foreach (char c in data)
			{
				var index = (int)c - (int)'A';

				if (index >= 0 && index < 26)
				{
					result += cipher[index];
				}
				else
				{
					result += c;
				}
			}

			return result;
		}

		private string Decode(string key, string data)
		{
			var cipher = GetCipherMap(key);
			var result = "";

			data = data.ToUpper();

			foreach (char c in data)
			{
				var index = cipher.IndexOfValue(c);

				if (index != -1)
				{
					result += ((char)((int)index + (int)'A'));
				}
				else
				{
					result += c;
				}
			}

			return result;
		}

		private SortedList<int, char> GetCipherMap(string cipherKey)
		{
			var result = new SortedList<int, char>();
			var alphabet = new List<List<char>>();
			var index = 0;
			var key = "";

			cipherKey = cipherKey.ToUpper();

			foreach (char c in cipherKey)
			{
				if (key.IndexOf(c) == -1)
				{
					key += c;
				}
			}

			foreach (char c in key)
			{
				alphabet.Add(new List<char>());
				alphabet[alphabet.Count - 1].Add(c);
			}

			foreach (char c in ALPHABET)
			{
				if (key.IndexOf(c) == -1)
				{
					alphabet[index].Add(c);
					index++;

					if (index == alphabet.Count)
					{
						index = 0;
					}
				}
			}

			alphabet.Sort((x, y) => x[0].CompareTo(y[0]));

			for (var i = 0; i < alphabet.Count; i++)
			{
				for (var j = 0; j < alphabet[i].Count; j++)
				{
					result.Add(result.Count, alphabet[i][j]);
				}
			}

			return result;
		}
	}
}
