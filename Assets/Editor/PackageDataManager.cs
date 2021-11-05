using System.IO;
using Blue.Json;
using UnityEngine;

namespace PackageHelper.Editor
{
    public class PackageDataManager
    {
        private readonly string jsonPath;
        private readonly JsonDictionary packageData;

        public string PackageName => packageData.Get<string>(string.Empty);
        public string PackageVersion => packageData.Get<string>("version");

        public PackageDataManager(string jsonPath)
        {
            this.jsonPath = jsonPath;
            var packageContent = File.ReadAllText(jsonPath);
            packageData = BlueParser.Json.Parse(packageContent);
        }

        public PackageDataManager UpdateVersion(string newVersion)
        {
            packageData["version"] = newVersion;
            var (version, release) = GetUnityVersion();
            packageData["unity"] = version;
            packageData["unityRelease"] = release;
            return this;
        }

        public void Persist()
        {
            File.WriteAllText(jsonPath, BlueParser.Json.Serialize(packageData, true));
        }

        /// <summary> Returns the unity version in the package.json </summary>
        /// <returns></returns>
        public (string version, string release) GetPackageUnityVersion()
        {
            return (packageData.Get<string>("unity"), packageData.Get<string>("unityRelease"));
        }

        /// <summary> Returns the unity version that the project is running on </summary>
        public static (string version, string release) GetUnityVersion()
        {
            var version = Application.unityVersion;
            var idx = version.LastIndexOf('.');
            return (version.Substring(0, idx), version.Substring(idx + 1));
        }
    }
}