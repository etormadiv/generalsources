using System;

namespace ConsoleExecrcies
{
	public class Program
	{
		public static void Main()
		{
			var V1 = new Vector(12, 15, 16);
			var V2 = new Vector(12,  5, 16);
			
			Console.WriteLine( "The given vectors are " + ((V1 == V2) ? "": "not ") + "equals");
		}
	}
	
	public class Vector
	{
		public int FirstValue;
		public int SecondValue;
		public int ThirdValue;
		
		public Vector(int firstValue, int secondValue, int thirdValue)
		{
			FirstValue  = firstValue;
			SecondValue = secondValue;
			ThirdValue  = thirdValue;
		}
		
		public static bool operator ==(Vector leftVector, Vector rightVector)
		{
			return   (  leftVector.FirstValue  == rightVector.FirstValue
					&& leftVector.SecondValue  == rightVector.SecondValue
					&& leftVector.ThirdValue   == rightVector.ThirdValue  );
		}
		
		public static bool operator !=(Vector leftVector, Vector rightVector)
		{
			return   ! (leftVector == rightVector);
		}
	}
}