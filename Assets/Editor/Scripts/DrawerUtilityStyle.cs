
using UnityEditor;
using UnityEngine;

namespace SonicEduquest
{
    [CreateAssetMenu(fileName = "DrawerUtilityStyle.asset", menuName = "Create Drawer Utility Style")]
    public class DrawerUtilityStyle : Style<DrawerUtilityStyle>
    {
        [field: SerializeField]     public  Texture Null    { get; private set; }
    }
}