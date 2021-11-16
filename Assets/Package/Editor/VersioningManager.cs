using UnityEditor;
using UnityEngine;

namespace UnityPackageHelper.Editor
{
    public class VersionManager : EditorWindow
    {
        internal static string PackageJsonPath => "Assets/Package/package.json";
        private PackageDataManager packageDataManager;
        private string currentVersion;

        [MenuItem("Innerspace/Version Manager")]
        public static void ShowWindow()
        {
            GetWindow(typeof(VersionManager), true, "Version Manager");
        }

        private void OnGUI()
        {
            if (packageDataManager == null)
            {
                packageDataManager = new PackageDataManager(PackageJsonPath);
                currentVersion = packageDataManager.PackageVersion;
            }

            DrawVersionField();

            GUILayout.Label("Version Manager", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Utilities", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete User Prefs"))
            {
                PlayerPrefs.DeleteAll();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Delete Editor Prefs"))
            {
                EditorPrefs.DeleteAll();
            }

            EditorGUILayout.EndHorizontal();
        }

        public void DrawVersionField()
        {
            bool versionIsDifferent = currentVersion != packageDataManager.PackageVersion;
            GUILayout.Label("Version Manager", EditorStyles.boldLabel);
            currentVersion = EditorGUILayout.TextField("Package Version", currentVersion);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Project Version", PlayerSettings.bundleVersion);
            EditorGUILayout.TextField("Package version", packageDataManager.PackageVersion);
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!versionIsDifferent);
            if (GUILayout.Button("Save Version"))
            {
                packageDataManager.UpdateVersion(currentVersion).Persist();
                PlayerSettings.bundleVersion = currentVersion;
                AssetDatabase.SaveAssets();
            }

            EditorGUI.EndDisabledGroup();
        }
    }
}