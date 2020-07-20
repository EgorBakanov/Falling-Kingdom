using System.Collections;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public abstract class ActionBase : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private int cost;
        
        public string Name => name;
        public int Cost => cost;
        public abstract IEnumerator Execute();
        public abstract bool IsTarget { get; }
    }

    public abstract class TargetAction : ActionBase
    {
        public sealed override bool IsTarget => true;
        public abstract bool CorrectTarget(Tile tile);
    }

    public abstract class NonTargetAction : ActionBase
    {
        public sealed override bool IsTarget => false;
    }
}