using Detrav.TeraApi;
using Detrav.TeraApi.Interfaces;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Detrav.Teroniffer.Core
{
    class AssetManager : IAssetManager
    {

        static string assets = "assets";
        string modName = "TeraModLoader";

        public AssetManager(string modName = null)
        {
            if (modName != null)
                this.modName = modName;
        }


        public void serialize(string path, object f, AssetType assetType = AssetType.relative)
        {
            string file = "";
            if (assetType == AssetType.relative)
                file = Path.Combine(assets, modName, path);
            else if (assetType == AssetType.global)
                file = path;
            Logger.debug("Started deSerialize for {0}", file);
            if (!Directory.Exists(Path.GetDirectoryName(file))) Directory.CreateDirectory(Path.GetDirectoryName(file));
            using (TextWriter tw = new StreamWriter(file))
            {
                JsonSerializerSettings s = new JsonSerializerSettings();
                tw.Write(JsonConvert.SerializeObject(f));
            }
            Logger.debug("End deSerialize for {0}", file);
        }

        public object deSerialize(string path, Type t, AssetType assetType = AssetType.relative)
        {
            string file = "";
            if (assetType == AssetType.relative)
                file = Path.Combine(assets, modName, path);
            else if (assetType == AssetType.global)
                file = path;
            Logger.debug("Started deSerialize for {0}", file);
            if (File.Exists(file))
                using (TextReader tr = new StreamReader(file))
                {
                    Logger.debug("End deSerialize for {0}", file);
                    return JsonConvert.DeserializeObject(tr.ReadToEnd(), t);
                }
            Logger.debug("Error on deSerialize for {0}", file);
            return null;
        }


        public string[] getDirectories(string path, AssetType assetType = AssetType.relative)
        {

            string file = "";
            switch (assetType)
            {
                case AssetType.relative:
                    if (path != null)
                        file = Path.Combine(assets, modName, path);
                    else
                        file = Path.Combine(assets, modName, path);
                    break;
                case AssetType.global:
                    file = path;
                    break;
            }
            return Directory.GetDirectories(file);
        }

        public string[] getFiles(string path, string patern, AssetType assetType = AssetType.relative)
        {
            string file = "";
            switch (assetType)
            {
                case AssetType.relative:
                    if (path != null)
                        file = Path.Combine(assets, modName, path);
                    else
                        file = Path.Combine(assets, modName, path);
                    break;
                case AssetType.global:
                    file = path;
                    break;
            }
            return Directory.GetFiles(file, patern);
        }


        public object getOpenFileDialog()
        {
            string root = Path.Combine(assets, modName);
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = root;
            return ofd;
        }

        public object getSaveFileDialog()
        {
            string root = Path.Combine(assets, modName);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = root;
            return sfd;
        }


        public string getMyFolder()
        {
            return Path.GetFullPath(Path.Combine(assets, modName));
        }
    }
}
