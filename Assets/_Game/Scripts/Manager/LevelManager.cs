using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Manager
{
    [CreateAssetMenu(fileName = "FloorSO", menuName = "ScriptableObjects/FloorData")]
    public class LevelManager : ScriptableObject
    {
        public List<Transform> floors;
        
        public static LevelManager Instance { get; private set; }
        public Transform GetFloor(int floor)
        {
            return floors[floor];
        }
    }
}
