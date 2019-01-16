﻿using System;
using System.IO;
using System.Windows.Forms;
using SharpDX;
using System.ComponentModel;

namespace Mafia2Tool
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //begin form inits;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Log.DeleteOldLog();
            //Log.CreateFile();

            ////do vital inits;
            CheckINIExists();
            ToolkitSettings.ReadINI();

            Language.ReadLanguageXML();

            MaterialData.Load();

            TypeDescriptor.AddAttributes(typeof(BoundingBox), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));
            TypeDescriptor.AddAttributes(typeof(BoundingSphere), new TypeConverterAttribute(typeof(ExpandableObjectConverter)));

            Application.Run(new GameExplorer());
        }

        /// <summary>
        /// Check if ini exists.
        /// </summary>
        private static void CheckINIExists()
        {
            if (File.Exists("Mafia2Tool.ini"))
                return;
            else
                new IniFile();
        }
    }
}
