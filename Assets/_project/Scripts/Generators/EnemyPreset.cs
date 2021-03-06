﻿using System;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Enemy Preset", menuName = "MFGJS2020/Enemy Preset")]
    public class EnemyPreset : ScriptableObject
    {
        [SerializeField] private new string name;
        [Range(0, 10)] [SerializeField] private int cantBuildZoneSize = 0;
        [SerializeField] private bool initialActivity = false;
        [SerializeField] private GameObject visualPrefab;
        [SerializeField] private string spawnerName;
        [SerializeField] private GameObject spawnerVisualPrefab;
        [TextArea][SerializeField] private string description;

        public string Name => name;
        public string SpawnerName => spawnerName;
        public int CantBuildZoneSize => cantBuildZoneSize;
        public bool InitialActivity => initialActivity;
        public GameObject VisualPrefab => visualPrefab;
        public string Description => description;
        public GameObject SpawnerVisualPrefab => spawnerVisualPrefab;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(name))
                name = base.name;
        }
#endif
    }
}