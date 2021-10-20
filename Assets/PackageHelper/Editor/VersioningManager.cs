using PackageHelper.Editor.PackageHandler;
using UnityEditor;
using UnityEngine;

namespace PackageHelper.Editor
{
    public class VersionManager : EditorWindow
    {
        internal static string PackageJson => "Assets/" + PlayerSettings.productName + "/package.json";
        private PackageReader reader;
        private string currentVersion;

        [MenuItem("Innerspace/Version Manager")]
        public static void ShowWindow()
        {
            GetWindow(typeof(VersionManager), true, "Version Manager");
        }

        private void OnGUI()
        {
            if (reader == null)
            {
                reader = new PackageReader(PackageJson);
                currentVersion = reader.PackageVersion;
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
            bool versionIsDifferent = currentVersion != reader.PackageVersion;
            GUILayout.Label("Version Manager", EditorStyles.boldLabel);
            currentVersion = EditorGUILayout.TextField("Package Version", currentVersion);

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.TextField("Project Version", PlayerSettings.bundleVersion);
            EditorGUILayout.TextField("Package version", reader.PackageVersion);
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!versionIsDifferent);
            if (GUILayout.Button("Save Version"))
            {
                reader.UpdateVersion(currentVersion).Persist();
                PlayerSettings.bundleVersion = currentVersion;
                AssetDatabase.SaveAssets();
            }

            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndFadeGroup();
        }
    }
}