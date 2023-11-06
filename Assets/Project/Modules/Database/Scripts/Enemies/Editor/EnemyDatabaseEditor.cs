using System.Text;
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

        public override void OnInspectorGUI()
        {
            var style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = Color.yellow;

            EditorGUILayout.LabelField("*************************   FORMULA   *************************", style);
            EditorGUILayout.LabelField("   Health = Max(BaseHealth, BaseHealth * (StageLevel ^ 2.5) * 2.5", style);
            EditorGUILayout.LabelField("   Damage = Max(BaseDamage, BaseDamage * (StageLevel ^ 1.01)", style);
            EditorGUILayout.LabelField("   Speed = Min(3f, this.BaseSpeed + (0.02f * StageLevel))", style);
            EditorGUILayout.LabelField("***************************************************************", style);
            EditorGUILayout.LabelField("", style);

            base.DrawDefaultInspector();
        }
    }
}