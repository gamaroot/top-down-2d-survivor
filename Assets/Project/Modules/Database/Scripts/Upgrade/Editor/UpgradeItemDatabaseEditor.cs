using UnityEditor;
using UnityEngine;

namespace Database
{
    [CustomEditor(typeof(UpgradeItemDatabase))]
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

        public override void OnInspectorGUI()
        {
            var style = new GUIStyle(EditorStyles.label);
            style.normal.textColor = Color.yellow;

            EditorGUILayout.LabelField("*********************************   FORMULA   *********************************", style);
            EditorGUILayout.LabelField("   Damage = Max(BaseDamage, BaseDamage * (WeaponLevel ^ 1.01)", style);
            EditorGUILayout.LabelField("   Shoot Interval = Max(0.5, BaseShootInterval - (Max(0, WeaponLevel - 1) / 1000))", style);
            EditorGUILayout.LabelField("   Bullet Speed = Min(30, BaseBulletSpeed + (Max(0, WeaponLevel - 1) / 1000))", style);
            EditorGUILayout.LabelField("*******************************************************************************", style);
            EditorGUILayout.LabelField("", style);

            base.DrawDefaultInspector();
        }
    }
}