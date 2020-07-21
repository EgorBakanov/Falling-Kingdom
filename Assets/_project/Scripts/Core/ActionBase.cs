using System.Collections;
using UnityEngine;

namespace Nara.MFGJS2020.Core
{
    public abstract class ActionBase : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private int cost;
        [Range(0, 3)] [SerializeField] protected float animationTime = .2f;
        
        public string Name => name;
        public int Cost => cost;
        public abstract IEnumerator Execute();
        public abstract bool IsTarget { get; }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(name))
                name = base.name;
        }
#endif
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