﻿using Mafia2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mafia2Tool
{
    public partial class FrameResourceTool : Form
    {
        private List<FrameObjectSingleMesh> mesh = new List<FrameObjectSingleMesh>();
        private List<TreeNode> unadded = new List<TreeNode>();
        private IniFile ini = new IniFile();
        public FrameResourceTool()
        {
            InitializeComponent();
            if (SceneData.ScenePath == "")
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string path = ini.Read("SDSPath", "Directories");
                    path = folderBrowser.SelectedPath;
                    SceneData.ScenePath = path;
                    ini.Write("SDSPath", path, "Directories");
                }
            }
            SceneData.BuildData();
            ReadFrameResource();
        }

        public void ReadFrameResource()
        {
            foreach (FrameNameTable.Data data in SceneData.FrameNameTable.FrameData)
            {
                int index = treeView1.Nodes.IndexOfKey(data.ParentName);

                if (index == -1)
                    treeView1.Nodes.Add(data.ParentName, data.ParentName);

                index = treeView1.Nodes.IndexOfKey(data.ParentName);

                TreeNode root = treeView1.Nodes[index];

                if (data.FrameIndex != -1)
                {
                    TreeNode node = CreateTreeNode((SceneData.FrameResource.FrameObjects[data.FrameIndex] as FrameObjectBase));

                    if (node == null)
                        continue;

                    root.Nodes.Add(node);
                }
            }
            for (int i = 0; i != SceneData.FrameResource.FrameBlocks.Length; i++)
                FrameResourceListBox.Items.Add(SceneData.FrameResource.FrameBlocks[i]);

            for (int i = 0; i != SceneData.FrameResource.FrameObjects.Length; i++)
                FrameResourceListBox.Items.Add(SceneData.FrameResource.FrameObjects[i]);

            for (int i = 0; i != SceneData.FrameResource.FrameObjects.Length; i++)
            {
                FrameObjectBase fObject = (FrameObjectBase)SceneData.FrameResource.FrameObjects[i];

                //if (fObject.ParentIndex1.Index == fObject.ParentIndex2.Index)
                //{
                //    if (fObject.ParentIndex1.Index == -1)
                //        continue;
                //}

                TreeNode node = CreateTreeNode(fObject);

                if (node == null)
                    continue;

                int index = 0;

                if (fObject.ParentIndex2.Index != -1)
                    index = fObject.ParentIndex2.Index;

                TreeNode[] nodes = treeView1.Nodes.Find(fObject.ParentIndex2.Name, true);

                if (fObject.ParentIndex1.Index != -1)
                    node = AddChildren(node, fObject);

                if (nodes.Length > 0)
                    nodes[0].Nodes.Add(node);
                else
                    unadded.Add(node);

            }

            foreach(TreeNode obj in unadded)
            {
                TreeNode[] nodes = treeView1.Nodes.Find((obj.Tag as FrameObjectBase).ParentIndex2.Name, true);

                if (nodes.Length > 0)
                    nodes[0].Nodes.Add(obj);
                else
                    Debug.WriteLine(string.Format("Warning: node: {0} was not added", obj.Name));
            }
        }
        private TreeNode AddChildren(TreeNode node, FrameObjectBase fObject)
        {
            while (fObject.ParentIndex1.Index != -1)
            {
                fObject = (SceneData.FrameResource.EntireFrame[fObject.ParentIndex1.Index] as FrameObjectBase);

                if (fObject.ParentIndex1.Index == fObject.ParentIndex2.Index)
                    return node;

                TreeNode child = CreateTreeNode(fObject);

                if (child == null)
                    return node;

                node.Nodes.Add(child);
            }
            return node;
        }

        private TreeNode CreateTreeNode(string NameText, int index)
        {
            TreeNode node = new TreeNode
            {
                Name = NameText,
                Text = NameText,
                Tag = SceneData.FrameResource.FrameBlocks[index]
            };

            return node;
        }
        private TreeNode CreateTreeNode(FrameObjectBase fObject)
        {
            TreeNode[] nodes2 = treeView1.Nodes.Find(fObject.Name.String, true);

            if (nodes2.Length > 0)
                return null;

            TreeNode node = ConvertNode(fObject.NodeData);

            if (fObject.GetType() == typeof(FrameObjectSingleMesh))
            {
                node.Nodes.Add(CreateTreeNode("Material", (fObject as FrameObjectSingleMesh).MaterialIndex));
                node.Nodes.Add(CreateTreeNode("Geometry", (fObject as FrameObjectSingleMesh).MeshIndex));
                node.ContextMenuStrip = contextMenuStrip1;
                mesh.Add((fObject as FrameObjectSingleMesh));
            }
            else if (fObject.GetType() == typeof(FrameObjectModel))
            {
                node.Nodes.Add(CreateTreeNode("Material", (fObject as FrameObjectModel).MaterialIndex));
                node.Nodes.Add(CreateTreeNode("Geometry", (fObject as FrameObjectModel).MeshIndex));
                node.Nodes.Add(CreateTreeNode("Skeleton Info", (fObject as FrameObjectModel).SkeletonIndex));
                node.Nodes.Add(CreateTreeNode("Skeleton Hierachy Info", (fObject as FrameObjectModel).SkeletonHierachyIndex));
                node.ContextMenuStrip = contextMenuStrip1;
                mesh.Add((fObject as FrameObjectModel));
            }
            
            return node;
        }
        private TreeNode ConvertNode(Node node)
        {
            TreeNode treeNode = new TreeNode()
            {
                Name = node.NameText,
                Text = node.NameText,
                Tag = node.Tag,
            };

            return treeNode;
        }

        private Vector3 RetrieveParent1Position(FrameObjectSingleMesh mesh)
        {
            Vector3 curPos;
            curPos = mesh.Matrix.Position;
            FrameObjectBase parent = (SceneData.FrameResource.EntireFrame[mesh.ParentIndex1.Index] as FrameObjectBase);

            while(parent != null)
            {
                if (parent.GetType() == typeof(FrameObjectFrame))
                {
                    if ((parent as FrameObjectFrame).Item != null)
                        curPos += (parent as FrameObjectFrame).Item.Position;
                }
                else
                {
                    curPos += parent.Matrix.Position;
                }

                if (parent.ParentIndex1.Index != -1)
                    parent = (SceneData.FrameResource.EntireFrame[parent.ParentIndex1.Index] as FrameObjectBase);
                else
                    parent = null;
            }

            return curPos;
        }

        private void OnSelectedChanged(object sender, EventArgs e)
        {
            FrameResourceGrid.SelectedObject = FrameResourceListBox.SelectedItem;
        }
        private void OnClickLoad3D(object sender, EventArgs e)
        {
            string[] fileNames = new string[mesh.Count];
            Vector3[] filePos = new Vector3[mesh.Count];
            Matrix33[] rotPos = new Matrix33[mesh.Count];

            CustomEDD frameEDD = new CustomEDD();
            frameEDD.EntryCount = mesh.Count;
            frameEDD.Entries = new CustomEDD.Entry[frameEDD.EntryCount];

            Parallel.For(0, mesh.Count, i =>
            {
                CustomEDD.Entry entry = new CustomEDD.Entry();

                Model newModel = new Model((mesh[i]), SceneData.VertexBufferPool, SceneData.IndexBufferPool, SceneData.FrameResource);

                if (mesh[i].ParentIndex1.Index != -1)
                {
                    FrameObjectBase parent = (SceneData.FrameResource.EntireFrame[mesh[i].ParentIndex1.Index] as FrameObjectBase);
                    filePos[i] = RetrieveParent1Position(mesh[i]);
                }

                if (((mesh[i].ParentIndex1.Index != -1)) && ((mesh[i].ParentIndex1.Index == mesh[i].ParentIndex2.Index)))
                {
                    FrameObjectFrame frame = SceneData.FrameResource.EntireFrame[mesh[i].ParentIndex1.Index] as FrameObjectFrame;
                    if (frame.Item != null)
                    {
                        filePos[i] = frame.Item.Position;
                    }
                }

                entry.LodCount = newModel.Lods.Length;
                entry.LODNames = new string[entry.LodCount];

                for (int c = 0; c != newModel.Lods.Length; c++)
                {
                    string edmName;
                    FrameGeometry meshGeom;

                    if (mesh[i].Name.String != "")
                    {
                        edmName = mesh[i].Name.String;
                    }
                    else
                    {
                        if (mesh[i].Mesh == null)
                        {
                            meshGeom = SceneData.FrameResource.EntireFrame[mesh[i].MeshIndex] as FrameGeometry;
                            edmName = meshGeom.LOD[c].VertexBufferRef.String;
                            edmName.Remove(edmName.Length - 5);
                        }
                        else
                        {
                            meshGeom = mesh[i].Mesh;
                            edmName = meshGeom.LOD[c].VertexBufferRef.String;
                        }
                    }

                    Console.WriteLine(edmName);

                    if (!File.Exists("exported/" + edmName + "_lod" + c + ".edm"))
                    {
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        newModel.ExportToEDM(newModel.Lods[c], edmName + "_lod" + c);
                        Debug.WriteLine("Mesh: {0} and time taken was {1}", edmName + "_lod" + c, watch.Elapsed);
                        watch.Stop();
                    }
                    entry.LODNames[c] = edmName + "_lod" + c;
                    Console.WriteLine("{0}/{1}", i, mesh.Count);
                }
                entry.Position = mesh[i].Matrix.Position;
                entry.Rotation = mesh[i].Matrix.Rotation;

                frameEDD.Entries[i] = entry;

            });

            using (BinaryWriter writer = new BinaryWriter(File.Create("exported/frame.edd")))
            {
                frameEDD.WriteToFile(writer);
            }
        }
        private void OnNodeSelect(object sender, TreeViewEventArgs e)
        {
            FrameResourceGrid.SelectedObject = treeView1.SelectedNode.Tag;
        }
        private void SwitchView(object sender, EventArgs e)
        {
            treeView1.Visible = (!treeView1.Visible) ? true : false;
            FrameResourceListBox.Visible = (!FrameResourceListBox.Visible) ? true : false;
        }

        private void LoadMaterialTool(object sender, EventArgs e)
        {
            if (!MaterialData.HasLoaded)
                return; 

            MaterialTool tool = new MaterialTool();
            tool.ShowDialog();
        }

        private void CollisionEditor_Click(object sender, EventArgs e)
        {
            CollisionEditor editor = new CollisionEditor();
        }

        private void OverwriteBuffer_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            if ((treeView1.SelectedNode.Tag.GetType() == typeof(FrameObjectSingleMesh) || (treeView1.SelectedNode.Tag.GetType() == typeof(FrameObjectModel))))
            {
                ulong indexRef;
                ulong vertexRef;

                int[] iIndex;
                int[] iVertex;

                CustomEDM edm;

                FrameObjectSingleMesh mesh = treeView1.SelectedNode.Tag as FrameObjectSingleMesh;
                FrameGeometry geom = SceneData.FrameResource.EntireFrame[mesh.MeshIndex] as FrameGeometry;
                FrameMaterial mat = SceneData.FrameResource.EntireFrame[mesh.MaterialIndex] as FrameMaterial;

                indexRef = geom.LOD[0].IndexBufferRef.uHash;
                vertexRef = geom.LOD[0].VertexBufferRef.uHash;

                iIndex = SceneData.IndexBufferPool.SearchBuffer(indexRef);
                iVertex = SceneData.VertexBufferPool.SearchBuffer(vertexRef);

                if (iIndex[0] == -1 || iVertex[0] == -1)
                    return;

                edmBrowser.ShowDialog();

                using (BinaryReader reader = new BinaryReader(File.Open(edmBrowser.FileName, FileMode.Open)))
                {
                    edm = new CustomEDM(reader);
                    edm.BufferIndexHash = indexRef;
                    edm.BufferVertexHash = vertexRef;
                    edm.BufferFlags = geom.LOD[0].VertexDeclaration;
                    edm.CalculateBounds(true);
                    edm.BuildBuffers();
                }
                geom.LOD[0].BuildNewPartition();
                geom.LOD[0].BuildNewMaterialSplit();
                geom.DecompressionFactor = edm.PositionFactor;
                geom.DecompressionOffset = edm.PositionOffset;
                mesh.Boundings = edm.Bound;
                geom.LOD[0].SplitInfo.NumVerts = edm.Parts[0].Vertices.Length;
                geom.LOD[0].NumVertsPr = edm.Parts[0].Vertices.Length;
                geom.LOD[0].SplitInfo.NumFaces = edm.Parts[0].Indices.Count;
                geom.LOD[0].SplitInfo.MaterialBursts[0].SecondIndex = Convert.ToUInt16(edm.Parts[0].Indices.Count - 1);
                mat.Materials[0][0].NumFaces = edm.Parts[0].Indices.Count;

                SceneData.IndexBufferPool.BufferPools[iIndex[0]].Buffers[iIndex[1]] = edm.IndexBuffer;
                SceneData.VertexBufferPool.BufferPools[iIndex[0]].Buffers[iIndex[1]] = edm.VertexBuffer;
                SceneData.IndexBufferPool.WriteToFile();
                SceneData.VertexBufferPool.WriteToFile();
            }
            else
            {
                MessageBox.Show("Click on a \"Single Mesh\" type of \"Model\" type in the tree view.", "Error");
            }
        }

        private void OnExit(object sender, FormClosingEventArgs e)
        {
            SaveChanges();
        }

        private void SaveClick(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            SaveChanges();
            Application.Exit();
        }

        private void SaveChanges()
        {
            DialogResult result = MessageBox.Show("Do you want to save your changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open("FrameResource_0.bin", FileMode.Create)))
                {
                    SceneData.FrameResource.WriteToFile(writer);
                }
                MessageBox.Show("Your saved file has been stored in the same folder as the executable.", "Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ReloadClick(object sender, EventArgs e)
        {
            SceneData.Reload();
            treeView1.Nodes.Clear();
            FrameResourceListBox.Items.Clear();
            ReadFrameResource();
        }

        private void OpenClick(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string path = ini.Read("SDSPath", "Directories");
                path = folderBrowser.SelectedPath;
                SceneData.ScenePath = path;
                ini.Write("SDSPath", path, "Directories");
            }
            SceneData.Reload();
            treeView1.Nodes.Clear();
            FrameResourceListBox.Items.Clear();
            ReadFrameResource();
        }

        private void ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "contextExtract3D")
            {
                FrameObjectSingleMesh fObject = treeView1.SelectedNode.Tag as FrameObjectSingleMesh;
                Model newModel = new Model((fObject), SceneData.VertexBufferPool, SceneData.IndexBufferPool, SceneData.FrameResource);
                newModel.ExportToEDM(newModel.Lods[0], fObject.Name.String);
            }
        }
    }
}
