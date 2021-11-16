using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace UnityPackageHelper.Editor
{
    public class PackageDataManager
    {
        private readonly string jsonPath;
        private readonly JObject packageData;

        public string PackageName => packageData.GetValue("name")?.ToString();
        public string PackageVersion => packageData.GetValue("version")?.ToString();

        public PackageDataManager(string jsonPath)
        {
            this.jsonPath = jsonPath;
            var packageContent = File.ReadAllText(jsonPath);
            packageData = JObject.Parse(packageContent);
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
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(packageData, Formatting.Indented));
        }

        /// <summary> Returns the unity version in the package.json </summary>
        /// <returns></returns>
        public (string version, string release) GetPackageUnityVersion()
        {
            return (packageData.GetValue("unity")?.ToString(), packageData.GetValue("unityRelease")?.ToString());
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