﻿using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SBahnChaosApp.Core;

namespace SBahnChaosApp.FileManager
{
    public class ConfigFile
    {
        public string Name { get { return baseFile.Name; } }
        public string Path { get { return baseFile.Path; } }
        public List<string> FileOfLines { get; private set; }

        private IFile baseFile;

        public ConfigFile(IFile iFile)
        {
            baseFile = iFile;
            FileOfLines = new List<string>();
            GetConfig();
        }

        public void GetConfig()
        {
            string configString;

            using (StreamReader reader = new StreamReader(baseFile.OpenAsync(PCLStorage.FileAccess.Read).Result))
                configString = reader.ReadToEnd();

            int pos1 = configString.IndexOf("<channels>") + ("<channels>").Length;
            int pos2 = configString.IndexOf("</channels>") - 1;

            string listOfChannelsRaw = configString.Substring(pos1, pos2 - pos1);
            var tmp = listOfChannelsRaw.Split().ToList();

            foreach (var item in tmp)
            {
                if (item != "")
                    FileOfLines.Add(item);
            }
        }

        public void InsertLine(ChannelFile channelFile)
        {
            string entry = $"\n{channelFile.Name}";

            string configString;
            using (StreamReader reader = new StreamReader(baseFile.OpenAsync(PCLStorage.FileAccess.Read).Result))
                configString = reader.ReadToEnd();

            int pos2 = configString.IndexOf("</channels>") - 1;
            configString = configString.Insert(pos2, entry);

            using (StreamWriter writer = new StreamWriter(baseFile.OpenAsync(PCLStorage.FileAccess.ReadAndWrite).Result))
                writer.Write(configString);
        }

        public static ConfigFile Create(IFolder folder)
        {
            IFile file = folder.CreateFileAsync("sbca.config", CreationCollisionOption.ReplaceExisting).Result;

            StringBuilder b = new StringBuilder();

            b.AppendLine("<Config>");
            b.AppendLine("<Title>ConfigFile for SBahnChaosApp</Title>");
            b.AppendLine("<Vesion>1.0</Version>");
            b.AppendLine("<channels>");
            b.AppendLine("</channels>");
            b.AppendLine("</Config>");

            using (StreamWriter writer = new StreamWriter(file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite).Result))
                writer.Write(b.ToString());

            return new ConfigFile(file);
        }
 }
}