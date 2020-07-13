using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private LevelManager levelManager;
        
        public GameStateMachine GameStateMachine => gameStateMachine;
        public bool HasNextLevel => currentLevel < levelManager.Size;

        private int currentLevel = 0;

        public Level GetNextLevel() => levelManager.Get(currentLevel++);
    }
}