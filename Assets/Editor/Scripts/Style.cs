
using UnityEditor;
using UnityEngine;

namespace SonicEduquest
{
    public class Style<TSelf> : ScriptableObject where TSelf : Style<TSelf>
    {
        private static TSelf _styleInstance;

        public static TSelf GetInstance()
        {
            if (Style<TSelf>._styleInstance == null)
            {
                string[] availableStyles = AssetDatabase.FindAssets("t:" + typeof(TSelf).Name);
                if (availableStyles.Length == 0)
                {
                    TSelf style = CreateInstance<TSelf>();
                    AssetDatabase.CreateAsset(style, "Assets/Settings/DrawerUtilityStyle.asset");
                }
                else
                {
                    string path = AssetDatabase.GUIDToAssetPath(availableStyles[0]);
                    Style<TSelf>._styleInstance = AssetDatabase.LoadAssetAtPath<TSelf>(path);
                }
            }

            return Style<TSelf>._styleInstance;
        }

        private void Awake()
        {
            if (Style<TSelf>._styleInstance == null)
            {
                Style<TSelf>._styleInstance = this as TSelf;
            }
            else
            {
                DestroyImmediate(this, true);
            }
        }
    }
}