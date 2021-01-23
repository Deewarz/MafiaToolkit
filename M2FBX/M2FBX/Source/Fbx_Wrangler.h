#pragma once

#include "Common.h"

class MT_FaceGroup;
class MT_MaterialInstance;
class MT_Object;
class MT_Lod;

enum class UVElementType
{
	UV_Diffuse = 0,
	UV_1 = 1,
	UV_2 = 2,
	UV_Omissive = 3
};

class Fbx_Wrangler
{
public:

	Fbx_Wrangler() = default;
	Fbx_Wrangler(const char* InName, const char* InDest);

	bool SetupFbxManager();
	bool ConstructScene();
	bool ConvertObjectToFbx();
	bool ConvertBundleToFbx();
	bool ConvertObjectToNode(const MT_Object& Object);

	bool ConvertLodToNode(const MT_Lod& Lod, FbxNode* LodNode);

private:

	FbxGeometryElementUV* CreateUVElement(FbxMesh* Mesh, const UVElementType Type);

	FbxGeometryElementMaterial* CreateMaterialElement(FbxMesh* pMesh, const char* pName);

	FbxSurfacePhong* CreateMaterial(const MT_MaterialInstance& MaterialInstance);

	FbxTexture* CreateTexture(const std::string& Name);

	const std::string& GetNameByUVType(const UVElementType Type);

	bool SaveDocument();

	const char* FbxName;
	const char* MTOName;

	// MTO related
	MT_Object* LoadedObject = nullptr;

	// Fbx related
	FbxManager* SdkManager = nullptr;
	FbxScene* Scene = nullptr;
};
