using PCLStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SBahnChaosApp.Core;
using System.Collections.ObjectModel;
using System.IO;

namespace SBahnChaosApp.FileManager
{
    public static class MainFileManager
    {
        public static IFileSystem MainFileSystem { get { return FileSystem.Current; } }
        public static IFolder RootFolder { get { return MainFileSystem.LocalStorage; } }
        public static ConfigFile ConfigFile { get; private set; }
        public static IFolder ChannelFolder { get; private set; }
        public static IList<IFile> channelFiles { get; private set; }

        public static bool IsInitialized { get; private set; }

        public static void Initialize()
        {
            if (IsInitialized)
                return;

            OpenConfig();
            LoadChannels();

            IsInitialized = true;
        }

        public static ObservableCollection<Line> GetSubscribedChannels()
        {
            if (!IsInitialized)
                Initialize();

            ObservableCollection<Line> tmp = new ObservableCollection<Line>();

            if (ConfigFile != null)
                foreach (var item in ConfigFile.IDOfLines)
                {
                    var str = item.Split(',');
                    tmp.Add(new Line(ushort.Parse(str[1]), (VehicleType)byte.Parse(str[0])));
                }

            return tmp;
        }

        public static void OpenConfig()
        {
            var a = RootFolder.CheckExistsAsync("sbca.config").Result;
            
            if (a == ExistenceCheckResult.NotFound)
            {
                ConfigFile = ConfigFile.Create(RootFolder);
            }
            else
            {
                ConfigFile = new ConfigFile(
                    RootFolder.GetFileAsync("sbca.config").Result);
                
            }
        }

        public static void LoadChannels()
        {
            ChannelFolder = RootFolder.CreateFolderAsync("channels", CreationCollisionOption.OpenIfExists).Result;
            channelFiles = ChannelFolder.GetFilesAsync().Result;
        }

        internal static void AddLineToConfi(Line ve)
        {
            ConfigFile?.InsertLine(ve);
        }
    }
}
