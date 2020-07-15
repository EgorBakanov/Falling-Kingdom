﻿using System;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Enemy Preset", menuName = "Enemy Preset")]
    public class EnemyPreset : ScriptableObject
    {
        [Range(0, 10)] [SerializeField] private int cantBuildZoneSize = 0;
        [SerializeField] private bool initialActivity = false;
        [SerializeField] private GameObject visualPrefab;
        [SerializeField] private String description;

        public int CantBuildZoneSize => cantBuildZoneSize;
        public bool InitialActivity => initialActivity;
        public GameObject VisualPrefab => visualPrefab;
        public string Description => description;
    }
}