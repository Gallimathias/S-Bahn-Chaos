using PCLStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SBahnChaosApp.Core;

namespace SBahnChaosApp.FileManager
{
    public class ChannelFile
    {
        public string Name { get { return baseFile.Name; } }
        public string Path { get { return baseFile.Path; } }

        public bool IsLoaded { get; private set; }

        public Line Line
        {
            get
            {
                if (line == null)
                    Load();

                return line;

            }
        }

        private Line line;

        private IFile baseFile;

        public ChannelFile(IFile file)
        {
            baseFile = file;
        }

        public static ChannelFile Create(Line line, IFolder folder)
        {
            IFile file;
            file = folder.CreateFileAsync($"{line.ID}.line", CreationCollisionOption.ReplaceExisting).Result;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<Line>");
            sb.AppendLine($"<Name>{line.Name}</Name>");
            sb.AppendLine($"<Type>{(byte)line.VehicleType}</Type>");
            sb.AppendLine("</Line>");

            using (StreamWriter writer = new StreamWriter(file.OpenAsync(PCLStorage.FileAccess.ReadAndWrite).Result))
                writer.Write(sb.ToString());

            return new ChannelFile(file);
        }

        public void Load()
        {
            string rawData;
            using (StreamReader reader = new StreamReader(baseFile.OpenAsync(PCLStorage.FileAccess.Read).Result))
                rawData = reader.ReadToEnd();

            var a = rawData.Split();

            var name = helpSubstring(a.First(s => s.StartsWith("<Name>")));
            var type = helpSubstring(a.First(s => s.StartsWith("<Type>")));

            line = new Line(ushort.Parse(name), (VehicleType)byte.Parse(type));
        }

        private string helpSubstring(string data)
        {
            int pos1 = data.IndexOf(">") + 1;
            int length = data.IndexOf("</") - pos1;

            return data.Substring(pos1, length);
        }
    }
}
