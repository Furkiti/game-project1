using System;
using UnityEngine;

namespace ObjectPool
{
    public class Pools : MonoBehaviour
    {
        public enum Types
        {
            Tile,
            Cross,
        }
        
        public static string GetTypeStr(Types poolType)
        {
            return Enum.GetName(typeof(Types), poolType);
        }
    }
}

