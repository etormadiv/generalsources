using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

/*
 *
 *	Author: Etor Madiv
 *  Contact us: etormadiv [ATAtat] gmail [DOTDotdot] com
 *
 */

namespace YoutubeLinksGrabber
{
	public class Program
	{
		public static void Main()
		{
			Console.Write("Please enter search keyword: ");
			string keyword = Console.ReadLine();
			string[] links = new YoutubeGrabber(keyword).GetLinks();
			StreamWriter sw = new StreamWriter("__result.txt");
			for(int i=0, j=1; i<links.Length; i+=3, j++)
				sw.WriteLine(string.Format("{0} => {1}", j, links[i]));
			sw.Flush();
			sw.Close();
			Console.WriteLine("Check __result.txt file");
		}
	}
	
	/*
	 * YoutubeGrabber class
	 * Grab youtube video links by giving a keyword
	 */
	public class YoutubeGrabber
	{
		private string ygSearchKeyword;
		
		public string SearchKeyword{
			get {
				return ygSearchKeyword;
			}
			set {
				ygSearchKeyword = FixKeyword(value);
			}
		}
		
		public YoutubeGrabber(string searchKeyword)
		{
			SearchKeyword = searchKeyword;
		}
		
		private string FixKeyword(string keyword)
		{
			return keyword.Replace(" ", "+");
		}
		
		//TODO: implement googlePageNumber parameter
		public string[] GetLinks(/*int googlePageNumber = 1*/)
		{	
			var hwr = (HttpWebRequest) WebRequest.Create("https://www.google.com/search?sclient=psy-ab&site=&source=hp&q=site:youtube.com+" + SearchKeyword + "&oq=site:youtube.com+" + SearchKeyword +"&gs_l=hp.12...27787.27787.0.38745.6.6.0.0.0.0.353.1268.0j5j0j1.6.0....0...1c.1.64.psy-ab..0.0.0.0.Qjwm1oCa-88&pbx=1&bav=on.2,or.r_cp.&bvm=bv.125596728,d.d2s&fp=1&biw=1280&bih=342&dpr=1&tch=1&ech=1&psi=c9dvV6DQJYu4aabtqugB.1466947443089.3");
			hwr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
			var sr = new StreamReader(hwr.GetResponse().GetResponseStream());
			var content = sr.ReadToEnd();
			var links = new List<string>();
			string link = null;
			int startIndex = 0;
			
			while( (link = GetNextUrl(content, ref startIndex)) != null)
			{
				if (link.Contains("\""))
					break;
				links.Add(link);
			}
			
			sr.Close();
			return links.ToArray();
		}
		
		private string GetNextUrl(string content, ref int startIndex)
		{
			int index = content.IndexOf("youtube.com\\/watch?v\\\\x3d", startIndex) + 25;
			if ( index < 0)
				return null;
			startIndex = index;
			return "https://www.youtube.com/watch?v=" + content.Substring(index, 11);
		}
	}
}