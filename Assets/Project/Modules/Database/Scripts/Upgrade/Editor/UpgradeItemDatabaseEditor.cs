using UnityEditor;
using UnityEngine;

namespace Database
{
    internal class UpgradeItemDatabaseEditor : Editor
    {
        [MenuItem("Assets/Create/Databases/Upgrade Item Database")]
        internal static void CreateDatabase()
        {
            string assetPath = EditorUtility.SaveFilePanelInProject("New Upgrade Item Database", "Upgrade Items", "asset", "Create a new Upgrade Item Database.");
            var asset = ScriptableObject.CreateInstance("UpgradeItemDatabase") as UpgradeItemDatabase;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}