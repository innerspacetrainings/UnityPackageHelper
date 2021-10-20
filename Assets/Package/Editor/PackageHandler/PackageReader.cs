using System.IO;
using Blue.Json;
using UnityEngine;
using Obj = System.Collections.Generic.Dictionary<string, object>;

namespace Editor.PackageHandler
{
    public class PackageReader
    {
        private readonly string packageAddress;
        private readonly JsonDictionary package;

        public string PackageVersion => package.Get<string>("version");

        public PackageReader(string packageJson)
        {
            packageAddress = packageJson;
            var packageContent = File.ReadAllText(packageJson);
            package = BlueParser.Json.Parse(packageContent);
        }

        public PackageReader UpdateVersion(string newVersion)
        {
            package["version"] = newVersion;
            var (version, release) = GetUnityVersion();
            package["unity"] = version;
            package["unityRelease"] = release;
            return this;
        }

        public void Persist()
        {
            File.WriteAllText(packageAddress, BlueParser.Json.Serialize(package, true));
        }

        /// <summary> Returns the unity version in the package.json </summary>
        /// <returns></returns>
        public (string version, string release) GetPackageUnityVersion()
        {
            return (package.Get<string>("unity"), package.Get<string>("unityRelease"));
        }

        /// <summary> Returns the unity version that the project is running on </summary>
        public static (string version, string release) GetUnityVersion()
        {
            var version = Application.unityVersion;
            int idx = version.LastIndexOf('.');
            return (version.Substring(0, idx), version.Substring(idx + 1));
        }
    }
}