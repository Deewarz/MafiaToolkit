﻿using FileTypes.XBin.StreamMap.Commands;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using Utils.Helpers.Reflection;

namespace ResourceTypes.M3.XBin
{
    public class StreamMapTable : BaseTable
    {
        public class StreamMapLine
        {
            public EStreamMapLineType LineType { get; set; }
            public string GameID { get; set; }
            public string MissionID { get; set; }
            public string PartID { get; set; }
            [PropertyIgnoreByReflector]
            public int TableCommandsOffset_DEBUG { get; set; }
            [PropertyIgnoreByReflector]
            public int TableCommandsOffset { get; set; }
            public int NumTableCommands0 { get; set; }
            [PropertyIgnoreByReflector]
            public int NumTableCommands1 { get; set; }
            public ICommand[] TableCommands { get; set; }
            public int IsAsync { get; set; }

            public override string ToString()
            {
                return string.Format("{0} {1} {2}", GameID, MissionID, PartID);
            }
        }

        public StreamMapLine[] Lines { get; set; }

        public void ReadFromFile(BinaryReader reader)
        {
            uint count1 = reader.ReadUInt32();
            uint count2 = reader.ReadUInt32();
            Lines = new StreamMapLine[count1];

            for (int i = 0; i < count1; i++)
            {
                StreamMapLine Line = new StreamMapLine();
                Line.LineType = (EStreamMapLineType)reader.ReadInt32();
                Line.GameID = XBinCoreUtils.ReadStringPtrWithOffset(reader);
                Line.MissionID = XBinCoreUtils.ReadStringPtrWithOffset(reader);
                Line.PartID = XBinCoreUtils.ReadStringPtrWithOffset(reader);
                Line.TableCommandsOffset = reader.ReadInt32();
                Line.TableCommandsOffset_DEBUG = (int)(reader.BaseStream.Position + Line.TableCommandsOffset - 4);
                Line.NumTableCommands0 = reader.ReadInt32();
                Line.NumTableCommands1 = reader.ReadInt32();
                Line.IsAsync = reader.ReadInt32(); // Could be bAsync.

                Lines[i] = Line;
            }

            for (int i = 0; i < count1; i++)
            {
                StreamMapLine Line = Lines[i];

                // Debug here. Make sure we are actually at the same offset as the Tables offset in the line. 
                // If not, we will have big problems and undoubtedly fail. 
                //if (Line.NumTableCommands0 > 0)
                //{
                //    Debug.Assert(reader.BaseStream.Position == Line.TableCommandsOffset_DEBUG, "We did not reach the Commands Offset");
                //}
                reader.BaseStream.Seek(Line.TableCommandsOffset_DEBUG, SeekOrigin.Begin);

                // We have to read the declarations
                uint[] TableCommandOffsets = new uint[Line.NumTableCommands0];
                uint[] TableCommandMagics = new uint[Line.NumTableCommands0];

                // Create the array
                Line.TableCommands = new ICommand[Line.NumTableCommands0];

                // Iterate and read them
                for (int z = 0; z < Line.TableCommands.Length; z++)
                {
                    TableCommandOffsets[z] = reader.ReadUInt32();
                    uint ActualOffset = (uint)(reader.BaseStream.Position + TableCommandOffsets[z] - 4);
                    TableCommandOffsets[z] = ActualOffset;

                    TableCommandMagics[z] = reader.ReadUInt32();
                }

                // Construct the Command.
                for (int z = 0; z < Line.TableCommands.Length; z++)
                {
                    reader.BaseStream.Seek(TableCommandOffsets[z], SeekOrigin.Begin);
                    ICommand Command = Command_Factory.ReadCommand(reader, TableCommandMagics[z]);
                    Line.TableCommands[z] = Command;
                }
            }
        }

        public void WriteToFile(XBinWriter writer)
        {
            writer.Write(Lines.Length);
            writer.Write(Lines.Length);

            for(int i = 0; i < Lines.Length; i++)
            {
                StreamMapLine Line = Lines[i];
                writer.Write((uint)Line.LineType);
                writer.PushStringPtr(Line.GameID);
                writer.PushStringPtr(Line.MissionID);
                writer.PushStringPtr(Line.PartID);
                writer.Write(-1); // TableOffset
                writer.Write(Line.TableCommands.Length); // This file stores it twice.
                writer.Write(Line.TableCommands.Length);
                writer.Write(Line.IsAsync);
            }

            for (int i = 0; i < Lines.Length; i++)
            {
                StreamMapLine Line = Lines[i];
                
                for(int x = 0; x < Line.TableCommands.Length; x++)
                {
                    writer.Write(-1); // Offset
                    writer.Write(Line.TableCommands[x].GetMagic());
                }

                for (int x = 0; x < Line.TableCommands.Length; x++)
                {
                    Line.TableCommands[x].WriteToFile(writer);
                }
            }

            writer.FixUpStringPtrs();

        }

        public void ReadFromXML(string file)
        {
            //ReflectionHelpers.ConvertPropertyToXML<StreamMapLine[]>(Lines);
        }

        public void WriteToXML(string file)
        {
            XElement Elements = ReflectionHelpers.ConvertPropertyToXML(Lines);
            Elements.Save(file, SaveOptions.None);
        }

        public TreeNode GetAsTreeNodes()
        {
            return null;
        }

        public void SetFromTreeNodes(TreeNode Root)
        {
           // do stuff
        }
    }
}
