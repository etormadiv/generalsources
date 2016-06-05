using System;

/*
 *		Author: Etor Madiv
 *		Contact us: etormadiv [ATAtat] gmail [DOTDotdot] com
 *		<facebook id="100012269284978">
 *			<firstName>Etor</firstName>
 *			<lastName>Madiv</lastName>
 *		</facebook>
 */

namespace BruteForce
{
	public class Program
	{
		public static void Main()
		{
			BruteForceListBuilder builder = new BruteForceListBuilder("qwer", 2);
			string result = builder.Build();
			Console.WriteLine(result);
		}
	}
	
	public class BruteForceListBuilder
	{
		private string bfAllowedChars;
		private int bfCount;
		private string result = "";
		
		public BruteForceListBuilder()
		{
			//TODO: Init by default bfAllowedChars and bfCount
		}
		
		public BruteForceListBuilder(string allowedChars, int count)
		{
			bfAllowedChars = allowedChars;
			bfCount = count;
		}
		
		public void SetAllowedChars(string newAllowedChars)
		{
			bfAllowedChars = newAllowedChars;
		}
		
		public void SetCount(int newCount)
		{
			bfCount = newCount;
		}
		
		public string Build()
		{
			char[] buffer = new char[bfCount];
			Generate(0, buffer);
			return result;
		}
		
		private void Generate(int index, char[] buffer)
		{
			for(int i=0; i<bfAllowedChars.Length; i++)
			{
				if(index < bfCount)
				{
					buffer[index] = bfAllowedChars[i];
					Generate(index + 1, buffer);
					if(index % bfCount > bfCount - 2)
						result += new string(buffer) + "\n";
				}
			}
		}
	}
	
}
