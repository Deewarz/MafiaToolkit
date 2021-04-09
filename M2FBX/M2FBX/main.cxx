#include <fbxsdk.h>
#include "Source/MTObject/MT_Object.h"
#include "Source/MTObject/MT_ObjectHandler.h"
#include "Source/Fbx_Wrangler.h"
#include "Source/MT_Wrangler.h"
#include <conio.h>

extern "C" int  __declspec(dllexport) _stdcall RunConvertFBX(const char* source, const char* dest);
extern "C" int  __declspec(dllexport) _stdcall RunConvertMTB(const char* source, const char* dest, unsigned char isBin);

extern int _stdcall RunConvertFBX(const char* source, const char* dest)
{
	printf("Called RunConvertFBX\n");
	MT_Wrangler* Wrangler = new MT_Wrangler(source, dest);
	Wrangler->ConstructMTBFromFbx();
	Wrangler->SaveBundleToFile();
	
	delete Wrangler;
	Wrangler = nullptr;

	return 0;
}
extern int _stdcall RunConvertMTB(const char* source, const char* dest, unsigned char isBin)
{
	printf("Called RunConvertMTB\n");
	MT_ObjectBundle* ObjectBundle = MT_ObjectHandler::ReadBundleFromFile(source);
	if (ObjectBundle)
	{
		Fbx_Wrangler* Wrangler = new Fbx_Wrangler(source, dest);
		Wrangler->ConvertBundleToFbx();

		ObjectBundle->Cleanup();

		delete Wrangler;
		Wrangler = nullptr;

		delete ObjectBundle;
		ObjectBundle = nullptr;
	}

	return 0;
}

int main(int argc, char** argv)
{
	int result = 0;

	if ((strcmp(argv[1], "-ConvertFBX") == 0) && (argc >= 4))
	{
		result = RunConvertFBX(argv[2], argv[3]);
	}
	else if ((strcmp(argv[1], "-ConvertMTB") == 0) && (argc >= 4))
	{
		result = RunConvertMTB(argv[2], argv[3], 0);
	}
	else if ((strcmp(argv[1], "-ConvertMTO") == 0) && (argc >= 4))
	{
		Fbx_Wrangler* Wrangler = new Fbx_Wrangler(argv[2], argv[3]);
		Wrangler->ConvertObjectToFbx();
	}
	else
	{
		printf("M2FBX Initiated succesfully.");
	}
	return result;
}