using UnityEditor;
using UnityEngine;

namespace Database
{
    [CustomEditor(typeof(EnemyDatabase))]
    public class EnemyDatabaseEditor : Editor
    {
        [MenuItem("Assets/Create/Databases/Enemy Database")]
        public static void CreateDatabase()
        {
            string assetPath = EditorUtility.SaveFilePanelInProject("New Enemy Database", "Enemies", "asset", "Create a new Enemy Database.");
            var asset = ScriptableObject.CreateInstance("EnemyDatabase") as EnemyDatabase;
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            AssetDatabase.Refresh();
        }
    }
}