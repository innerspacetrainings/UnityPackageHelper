using System.IO;
using Blue.Json;
using NUnit.Framework;
using PackageHelper.Editor.PackageHandler;
using UnityEditor;

namespace PackageHelper.Editor.Tests
{
    public class VersioningTests
    {
        private PackageReader packageReader;

        [SetUp]
        public void Setup()
        {
            packageReader = new PackageReader(VersionManager.PackageJson);
        }

        [Test]
        public void VersionsShouldBeTheSame()
        {
            Assert.AreEqual(packageReader.PackageVersion, PlayerSettings.bundleVersion,
                "Package's version and project setting version must be the same");
        }

        [Test]
        public void UnityVersionShouldBeCorrectlySetup()
        {
            Assert.AreEqual(PackageReader.GetUnityVersion(), packageReader.GetPackageUnityVersion(),
                "Package's minimum unity version and project current unity version must be the same");
        }

        [Test]
        public void DependencyVersionsShouldMatch()
        {
            var packageContent = File.ReadAllText(VersionManager.PackageJson);
            var requiredPackages = BlueParser.Json.Parse(packageContent).Get<JsonDictionary>("dependencies");
            var manifestContent = File.ReadAllText("Packages/manifest.json");
            var installedPackages = BlueParser.Json.Parse(manifestContent).Get<JsonDictionary>("dependencies");

            foreach (var package in requiredPackages)
            {
                Assert.IsTrue(installedPackages.ContainsKey(package.Key),
                    $"Package ${package.Key} is not installed in the project");
                Assert.AreEqual(package.Value, installedPackages.Get<string>(package.Key),
                    $"Versions doesn't match in requirement: {package.Key}.\n" +
                    "Be sure that the project uses the same required version");
            }
        }
    }
}