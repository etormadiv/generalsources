using System;
using System.Text;
using System.Security.Cryptography;

/* Author: Etor Madiv */

namespace Keygen
{
	public class Program
	{
		public static void Main(string[] args)
		{
			if(args.Length != 1)
			{
				Console.WriteLine("Please specify the username\n\nExample: keygen.exe etormadiv");
				return;
			}
			string key = "3lrKPYay7hut7Ye";
			MD5 md5 = new MD5CryptoServiceProvider();
			var tripleDes = new TripleDESCryptoServiceProvider()
			{
				Key = md5.ComputeHash(Encoding.Unicode.GetBytes(key)),
				IV = new byte[8]
			};
			byte[] dataToEncrypt = Encoding.UTF8.GetBytes(args[0]);
			byte[] encryptedBlock = tripleDes.CreateEncryptor().TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
			Console.WriteLine( Convert.ToBase64String(encryptedBlock) );
		}
	}
}
