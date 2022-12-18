
using UnityEditor;
using UnityEngine;

namespace SonicEduquest
{
    [CreateAssetMenu(fileName = "EditorStyle.asset", menuName = "Create Editor Style")]
    public class EditorStyle : ScriptableObject
    {
                                    private static  EditorStyle _instance;

        [field: SerializeField]     public          Texture     Member                          { get; private set; }
        [field: SerializeField]     public          Texture     ReadOnly                        { get; private set; }
        [field: SerializeField]     public          Texture     Required                        { get; private set; }
        [field: SerializeField]     public          Texture     RequiredNull                    { get; private set; }
        [field: SerializeField]     public          Texture     Null                            { get; private set; }
        [field: SerializeField]     public          Color       BackgroundRequired              { get; private set; }
        [field: SerializeField]     public          Color       BackgroundRequiredNull          { get; private set; }
        [field: SerializeField]     public          Color       BackgroundNull                  { get; private set; }
        [field: SerializeField]     public          Color       ForegroundRequired              { get; private set; }
        [field: SerializeField]     public          Color       ForegroundRequiredNull          { get; private set; }
        [field: SerializeField]     public          Color       ForegroundNull                  { get; private set; }
        [field: SerializeField]     public          Color       HelpBoxBackgroundRequiredNull   { get; private set; }
        [field: SerializeField]     public          Color       HelpBoxBackgroundNull           { get; private set; }
        [field: SerializeField]     public          Color       HelpBoxForegroundRequiredNull   { get; private set; }
        [field: SerializeField]     public          Color       HelpBoxForegroundNull           { get; private set; }
        [field: SerializeField]     public          float       PreHeaderSpace                  { get; private set; }
        [field: SerializeField]     public          float       PostHeaderSpace                 { get; private set; }
        [field: SerializeField]     public          float       PostIconSpace                   { get; private set; }
        [field: SerializeField]     public          float       PostPropertySpace               { get; private set; }
        [field: SerializeField]     public          float       PreHelpBoxAreaSpace             { get; private set; }
        [field: SerializeField]     public          float       PostHelpBoxSpace                { get; private set; }
        [field: SerializeField]     public          float       PreCovariantFieldSpace          { get; private set; }
        [field: SerializeField]     public          float       PostCovariantFieldSpace         { get; private set; }

        public static EditorStyle GetInstance()
        {
            if (EditorStyle._instance == null)
            {
                string[] availableStyles = AssetDatabase.FindAssets("t:EditorStyle");
                if (availableStyles.Length == 0)
                {
                    EditorStyle style = CreateInstance<EditorStyle>();
                    AssetDatabase.CreateAsset(style, "Assets/Settings/EditorStyle.asset");
                }
                else
                {
                    string path = AssetDatabase.GUIDToAssetPath(availableStyles[0]);
                    EditorStyle._instance = AssetDatabase.LoadAssetAtPath<EditorStyle>(path);
                }
            }

            return EditorStyle._instance;
        }

        private void Awake()
        {
            if (EditorStyle._instance == null)
            {
                EditorStyle._instance = this;
            }
            else
            {
                DestroyImmediate(this, true);
            }
        }
    }
}