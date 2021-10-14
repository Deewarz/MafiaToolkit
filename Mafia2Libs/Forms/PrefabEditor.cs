﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Gibbed.Illusion.FileFormats.Hashing;
using ResourceTypes.Prefab;
using ResourceTypes.Prefab.CrashObject;
using Utils.Helpers;
using Utils.Helpers.Reflection;
using Utils.Language;
using Utils.StringHelpers;

namespace Mafia2Tool
{
    public partial class PrefabEditor : Form
    {
        private FileInfo prefabFile;
        private PrefabLoader prefabs;

        public PrefabEditor(FileInfo file)
        {
            InitializeComponent();
            Localise();
            prefabFile = file;
        }

        private void Localise()
        {
            Text = Language.GetString("PREFAB_EDITOR_TITLE");
            Button_File.Text = Language.GetString("$FILE");
            Button_Save.Text = Language.GetString("$SAVE");
            Button_Reload.Text = Language.GetString("$RELOAD");
            Button_Exit.Text = Language.GetString("$EXIT");
        }

        public void InitEditor(List<string> definitionNames)
        {
            prefabs = new PrefabLoader(prefabFile);

            Show();

            for (int i = 0; i < definitionNames.Count; i++)
            {
                var name = definitionNames[i];
                var hash = FNV64.Hash(name);

                foreach (var prefab in prefabs.Prefabs)
                {
                    if (hash == prefab.Hash)
                    {
                        prefab.AssignedName = name;
                    }
                    else
                    {
                        Console.WriteLine(prefab.Hash);
                    }
                }
            }

            foreach (var prefab in prefabs.Prefabs)
            {
                var name = string.IsNullOrEmpty(prefab.AssignedName) ? "Not Found!" : prefab.AssignedName;
                TreeNode node = new TreeNode();
                node.Tag = prefab;
                node.Text = name;
                node.Name = name;
                TreeView_Prefabs.Nodes.Add(node);
            }
        }

        private void OnNodeSelectSelect(object sender, TreeViewEventArgs e)
        {
            Grid_Prefabs.SelectedObject = e.Node.Tag;
        }

        private void Button_Export_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_Prefabs.SelectedNode;

            if (SelectedNode != null)
            {
                if (SelectedNode.Tag is PrefabLoader.PrefabStruct)
                {
                    PrefabLoader.PrefabStruct Prefab = (SelectedNode.Tag as PrefabLoader.PrefabStruct);
                    
                    if(Browser_ExportPRB.ShowDialog() == DialogResult.OK)
                    {
                        string FileName = Browser_ExportPRB.FileName;

                        using(BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Create)))
                        {
                            writer.WriteString16(Prefab.AssignedName);
                            Prefab.WriteToFile(writer, false);
                        }

                        using (BinaryWriter writer = new BinaryWriter(File.Open(FileName + "EDT", FileMode.Create)))
                        {
                            writer.WriteString16(Prefab.AssignedName);
                            Prefab.WriteToFile(writer, true);
                        }
                    }
                }
            }
        }

        private void Button_Import_Click(object sender, EventArgs e)
        {
            if (Browser_ImportPRB.ShowDialog() == DialogResult.OK)
            {
                string FileName = Browser_ImportPRB.FileName;
                PrefabLoader.PrefabStruct NewPrefab = new PrefabLoader.PrefabStruct();

                using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
                {
                    string AssignedName = StringHelpers.ReadString16(reader);
                    NewPrefab.ReadFromFile(reader);
                    NewPrefab.AssignedName = AssignedName;
                }

                TreeNode node = new TreeNode();
                node.Tag = NewPrefab;
                node.Text = NewPrefab.AssignedName;
                node.Name = NewPrefab.AssignedName;
                TreeView_Prefabs.Nodes.Add(node);
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_Prefabs.SelectedNode;

            if(SelectedNode != null)
            {
                SelectedNode.Remove();
            }
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            List<PrefabLoader.PrefabStruct> NewPrefabs = new List<PrefabLoader.PrefabStruct>();

            foreach(TreeNode Node in TreeView_Prefabs.Nodes)
            {
                if(Node.Tag is PrefabLoader.PrefabStruct)
                {
                    PrefabLoader.PrefabStruct Prefab = (Node.Tag as PrefabLoader.PrefabStruct);
                    NewPrefabs.Add(Prefab);
                }
            }

            // Create backup, set our new prefabs, and then save.
            File.Copy(prefabFile.FullName, prefabFile.FullName + "_old", true);
            prefabs.Prefabs = NewPrefabs.ToArray();
            prefabs.WriteToFile(prefabFile);
        }

        private void ContextStrip_Prefab_OnOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel if nothing is selected
            TreeNode SelectedNode = TreeView_Prefabs.SelectedNode;
            if(SelectedNode == null)
            {
                e.Cancel = true;
            }
        }

        private void Button_ExportAsXML_Clicked(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_Prefabs.SelectedNode;
            if (SelectedNode == null)
            {
                // fail
                return;
            }

            PrefabLoader.PrefabStruct PrefabObject = (SelectedNode.Tag as PrefabLoader.PrefabStruct);
            if (PrefabObject == null)
            {
                // fail
                return;
            }

            SaveFileDialog XMLExportDialog = new SaveFileDialog();
            XMLExportDialog.Title = "Export Prefab XML";
            XMLExportDialog.InitialDirectory = prefabFile.DirectoryName;
            XMLExportDialog.Filter = "XML File | *.xml";

            if (XMLExportDialog.ShowDialog() == DialogResult.OK)
            {
                XElement ConvertedXML = ReflectionHelpers.ConvertPropertyToXML(PrefabObject.InitData);

                string FileName = PrefabObject.Hash.ToString();
                if (!string.IsNullOrEmpty(PrefabObject.AssignedName))
                {
                    FileName = PrefabObject.AssignedName;
                }

                ConvertedXML.Save(XMLExportDialog.FileName);
            }
        }

        private void Button_ImportAsXML_Clicked(object sender, EventArgs e)
        {
            TreeNode SelectedNode = TreeView_Prefabs.SelectedNode;
            if (SelectedNode == null)
            {
                // fail
                return;
            }

            PrefabLoader.PrefabStruct PrefabObject = (SelectedNode.Tag as PrefabLoader.PrefabStruct);
            if (PrefabObject == null)
            {
                // fail
                return;
            }

            // Create and open dialog
            OpenFileDialog XMLImportDialog = new OpenFileDialog();
            XMLImportDialog.Title = "Import Prefab XML";
            XMLImportDialog.InitialDirectory = prefabFile.DirectoryName;
            XMLImportDialog.Filter = "XML File | *.xml";

            if(XMLImportDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                string Name = XMLImportDialog.FileName;
                XElement XMLContent = XElement.Load(Name);
                S_GlobalInitData InitData = ReflectionHelpers.ConvertToPropertyFromXML<S_GlobalInitData>(XMLContent);
                PrefabObject.InitData = InitData;

                Grid_Prefabs.Refresh();

                Cursor.Current = Cursors.Default;
            }
        }
    }
}