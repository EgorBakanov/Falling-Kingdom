using System;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Enemy Preset", menuName = "MFGJS2020/Enemy Preset")]
    public class EnemyPreset : ScriptableObject
    {
        [Range(0, 10)] [SerializeField] private int cantBuildZoneSize = 0;
        [SerializeField] private bool initialActivity = false;
        [SerializeField] private GameObject visualPrefab;
        [SerializeField] private GameObject spawnerVisualPrefab;
        [TextArea][SerializeField] private string description;

        public int CantBuildZoneSize => cantBuildZoneSize;
        public bool InitialActivity => initialActivity;
        public GameObject VisualPrefab => visualPrefab;
        public string Description => description;
        public GameObject SpawnerVisualPrefab => spawnerVisualPrefab;
    }
}