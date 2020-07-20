using System;
using Nara.MFGJS2020.Core;
using UnityEngine;

namespace Nara.MFGJS2020.Generators
{
    [CreateAssetMenu(fileName = "New Tower Preset", menuName = "MFGJS2020/Tower Preset")]
    public class TowerPreset : ScriptableObject
    {
        [SerializeField] private new string name;
        [Range(0, 100)] [SerializeField] private int cost;
        [Range(1, 15)] [SerializeField] private int startHealth = 1;
        [Range(1, 15)] [SerializeField] private int maxHealth = 1;
        [Range(0, 10)] [SerializeField] private int cantBuildZoneSize = 0;
        [Range(0, 10)] [SerializeField] private int expandBuildZoneSize = 0;
        [SerializeField] private bool initialActivity = false;
        [SerializeField] private GameObject visualPrefab;
        [TextArea][SerializeField] private string description;
        [SerializeField] private ActionBase[] activeActions;
        [SerializeField] private NonTargetAction endPlayerTurnAction;
        [SerializeField] private NonTargetAction beginPlayerTurnAction;

        public string Name => name;
        public int Cost => cost;
        public int StartHealth => startHealth;
        public int MaxHealth => maxHealth;
        public bool InitialActivity => initialActivity;
        public int CantBuildZoneSize => cantBuildZoneSize;
        public int ExpandBuildZoneSize => expandBuildZoneSize;
        public GameObject VisualPrefab => visualPrefab;
        public string Description => description;
        public ActionBase[] ActiveActions => activeActions;
        public NonTargetAction BeginPlayerTurnAction => beginPlayerTurnAction;
        public NonTargetAction EndPlayerTurnAction => endPlayerTurnAction;
    }
}