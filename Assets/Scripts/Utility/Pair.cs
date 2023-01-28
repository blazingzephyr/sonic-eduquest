
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SonicEduquest
{
    [Serializable]
    public struct Pair<TValue1, TValue2>
    {
        [field: SerializeField]
        public TValue1 Value1 { get; set; }

        [field: SerializeField]
        public TValue2 Value2 { get; set; }

        public static explicit operator KeyValuePair<TValue1, TValue2>(Pair<TValue1, TValue2> param)
        {
            return new KeyValuePair<TValue1, TValue2>(param.Value1, param.Value2);
        }
    }
}