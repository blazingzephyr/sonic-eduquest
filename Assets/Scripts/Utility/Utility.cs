
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FunkyQuest
{
    internal static class Utility
    {
        public struct ComparisonInformation
        {
            public bool Contains { get; set; }
            public int Index { get; set; }

            public static implicit operator bool(ComparisonInformation info) => info.Contains;
            public static implicit operator int(ComparisonInformation info) => info.Index;
        }

        public static void OnTriggerEvent<T>(Collider2D collision, LayerMask objectLayer, List<T> objectList)
            where T : Component
        {
            GameObject gameObject = collision.gameObject;
            T[] triggerObjects = gameObject.GetComponents<T>();
            int layer = 1 << gameObject.layer;
            bool isIntersecting = (objectLayer & layer) == layer;

            for (int i = 0; i < triggerObjects.Length && isIntersecting; i++)
            {
                T triggerObject = triggerObjects[i];
                bool contains = objectList.Contains(triggerObject);

                if (contains)
                {
                    objectList.Remove(triggerObject);
                }
                else
                {
                    objectList.Add(triggerObject);
                }
            }
        }

        public static ComparisonInformation Contains<T>(this T[] sequence, T element)
            where T : IEquatable<T>
        {
            bool found = false;
            int i = 0;

            for (; i < sequence.Length && !found; i++)
            {
                found = sequence[i].Equals(element);
            }

            return new ComparisonInformation { Contains = found, Index = found ? i - 1 : -1 };
        }

        public static bool All<T>(this T[] sequence, Predicate<T> predicate)
        {
            bool isTrueForAll = true;
            for (int i = 0; i < sequence.Length && isTrueForAll; i++)
            {
                isTrueForAll = predicate(sequence[i]);
            }

            return isTrueForAll;
        }
    }
}