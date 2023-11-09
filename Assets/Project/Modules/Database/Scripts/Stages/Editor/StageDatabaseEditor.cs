using System.Text;
using UnityEditor;
using UnityEngine;

namespace Database
{
    [CustomEditor(typeof(StageDatabase))]
    public class StageDatabaseEditor : Editor
    {
        [MenuItem("Assets/Create/Databases/Stage Database")]
        public static void CreateDatabase()
        {
            string assetPath = EditorUtility.SaveFilePanelInProject("New Stage Database", "Stages", "asset", "Create a new Stage Database.");
            var asset = ScriptableObject.CreateInstance("StageDatabase") as StageDatabase;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}