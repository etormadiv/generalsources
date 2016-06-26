Imports System.IO
Imports System.Net
Imports System.Collections.Generic

'
' *
' *	 Author: Etor Madiv
' *  Contact us: etormadiv [ATAtat] gmail [DOTDotdot] com
' *
' 

Namespace YoutubeLinksGrabber
	Public Class Program
		Public Shared Sub Main()
			Console.Write("Please enter search keyword: ")
			Dim keyword As String = Console.ReadLine()
			Dim links As String() = New YoutubeGrabber(keyword).GetLinks()
			Dim sw As New StreamWriter("__result.txt")
			Dim i As Integer = 0, j As Integer = 1
			While i < links.Length
				sw.WriteLine(String.Format("{0} => {1}", j, links(i)))
				i += 3
				j += 1
			End While
			sw.Flush()
			sw.Close()
			Console.WriteLine("Check __result.txt file")
		End Sub
	End Class

	'
	' * YoutubeGrabber class
	' * Grab youtube video links by giving a keyword
	'

	Public Class YoutubeGrabber
		Private ygSearchKeyword As String

		Public Property SearchKeyword() As String
			Get
				Return ygSearchKeyword
			End Get
			Set
				ygSearchKeyword = FixKeyword(value)
			End Set
		End Property

		Public Sub New(searchKeyword__1 As String)
			SearchKeyword = searchKeyword__1
		End Sub

		Private Function FixKeyword(keyword As String) As String
			Return keyword.Replace(" ", "+")
		End Function

		'TODO: implement googlePageNumber parameter
		'int googlePageNumber = 1
		Public Function GetLinks() As String()
			Dim hwr = DirectCast(WebRequest.Create((Convert.ToString((Convert.ToString("https://www.google.com/search?sclient=psy-ab&site=&source=hp&q=site:youtube.com+") & SearchKeyword) + "&oq=site:youtube.com+") & SearchKeyword) + "&gs_l=hp.12...27787.27787.0.38745.6.6.0.0.0.0.353.1268.0j5j0j1.6.0....0...1c.1.64.psy-ab..0.0.0.0.Qjwm1oCa-88&pbx=1&bav=on.2,or.r_cp.&bvm=bv.125596728,d.d2s&fp=1&biw=1280&bih=342&dpr=1&tch=1&ech=1&psi=c9dvV6DQJYu4aabtqugB.1466947443089.3"), HttpWebRequest)
			hwr.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36"
			Dim sr = New StreamReader(hwr.GetResponse().GetResponseStream())
			Dim content = sr.ReadToEnd()
			Dim links = New List(Of String)()
			Dim link As String = Nothing
			Dim startIndex As Integer = 0

			While (InlineAssignHelper(link, GetNextUrl(content, startIndex))) IsNot Nothing
				If link.Contains("""") Then
					Exit While
				End If
				links.Add(link)
			End While

			sr.Close()
			Return links.ToArray()
		End Function

		Private Function GetNextUrl(content As String, ByRef startIndex As Integer) As String
			Dim index As Integer = content.IndexOf("youtube.com\/watch?v\\x3d", startIndex) + 25
			If index < 0 Then
				Return Nothing
			End If
			startIndex = index
			Return "https://www.youtube.com/watch?v=" + content.Substring(index, 11)
		End Function
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
	End Class
End Namespace