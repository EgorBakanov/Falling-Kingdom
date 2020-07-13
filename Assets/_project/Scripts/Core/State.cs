using System.Collections;
using Nara.MFGJS2020.Holders;
using UnityEngine.EventSystems;

namespace Nara.MFGJS2020.Core
{
    public abstract class State
    {
        public GameStateMachine GameStateMachine { get; }
        
        public State(GameStateMachine gameStateMachine)
        {
            GameStateMachine = gameStateMachine;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator OnTileClick(Tile tile, PointerEventData eventData)
        {
            yield break;
        }

        public virtual IEnumerator OnUISubmit()
        {
            yield break;
        }
    }
}