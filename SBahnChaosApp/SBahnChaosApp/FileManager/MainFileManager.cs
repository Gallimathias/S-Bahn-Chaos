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
        public static List<ChannelFile> channelFiles { get; private set; }

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

            if (channelFiles != null)
                foreach (var item in channelFiles)
                    tmp.Add(item.Line);

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
            channelFiles = new List<ChannelFile>();
            ChannelFolder = RootFolder.CreateFolderAsync("channels", CreationCollisionOption.OpenIfExists).Result;

            foreach (var item in ConfigFile.FileOfLines)
                channelFiles.Add(new ChannelFile(ChannelFolder.GetFileAsync(item).Result));
        }

        internal static void SaveLine(Line ve)
        {
            var channel = ChannelFile.Create(ve, ChannelFolder);
            ConfigFile.InsertLine(channel);
            channelFiles.Add(channel);
        }
    }
}
