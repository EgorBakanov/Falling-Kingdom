using System;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Tower Preset", menuName = "Tower Preset")]
    public class TowerPreset : ScriptableObject
    {
        [Range(1, 15)] [SerializeField] private int startHealth = 1;
        [Range(1, 15)] [SerializeField] private int maxHealth = 1;
        [Range(0, 10)] [SerializeField] private int cantBuildZoneSize = 0;
        [Range(0, 10)] [SerializeField] private int expandBuildZoneSize = 0;
        [SerializeField] private bool initialActivity = false;
        [SerializeField] private GameObject visualPrefab;
        [SerializeField] private String description;

        public int StartHealth => startHealth;
        public int MaxHealth => maxHealth;
        public bool InitialActivity => initialActivity;
        public int CantBuildZoneSize => cantBuildZoneSize;
        public int ExpandBuildZoneSize => expandBuildZoneSize;
        public GameObject VisualPrefab => visualPrefab;
        public string Description => description;
    }
}