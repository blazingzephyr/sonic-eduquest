
namespace SonicEduquest
{
    public static class UnityExtension
    {
        public static bool UnityEquals(this object obj, object other)
        {
            if (obj is UnityEngine.Object uObj1)
            {
                return uObj1.Equals(other);
            }

            if (other is UnityEngine.Object uObj2)
            {
                return uObj2.Equals(obj);
            }

            return Equals(obj, other);
        }

        public static bool IsUnityNull(this object obj) => UnityEquals(obj, null);
    }
}