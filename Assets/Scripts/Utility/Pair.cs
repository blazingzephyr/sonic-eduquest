
using System;
using UnityEngine;

namespace FunkyQuest
{
    [Serializable]
    internal struct Pair<T1, T2>
    {
        [field: SerializeField]
        public T1 Value1 { get; set; }

        [field: SerializeField]
        public T2 Value2 { get; set; }
    }
}