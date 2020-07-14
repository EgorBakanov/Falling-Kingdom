using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.Holders;
using Nara.MFGJS2020.States;
using UnityEngine;

namespace Nara.MFGJS2020.Control
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameStateMachine stateMachine;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GridHolder gridHolder;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private EnemyManager enemyManager;

        public GridHolder GridHolder => gridHolder;
        public GameStateMachine StateMachine => stateMachine;
        public AudioManager AudioManager => audioManager;

        public UIManager UiManager => uiManager;

        public TowerManager TowerManager => towerManager;
        public bool HasNextLevel => _currentLevel < levelManager.Size;

        private int _currentLevel = 0;

        public void NextLevel() => _currentLevel++;
        public Level GetCurrentLevel() => levelManager.Get(_currentLevel);

        private void Awake()
        {
            if (Instance != null)
                return;
            if (Instance == this)
                return;

            Instance = this;
        }

        private void Start()
        {
            stateMachine.SetState(new BeginState());
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (stateMachine == null)
                stateMachine = GetComponent<GameStateMachine>();

            if (levelManager == null)
                levelManager = GetComponent<LevelManager>();

            if (audioManager == null)
                audioManager = GetComponent<AudioManager>();

            if (gridHolder == null)
                gridHolder = GetComponent<GridHolder>();

            if (uiManager == null)
                uiManager = GetComponent<UIManager>();
            
            if (towerManager == null)
                towerManager = GetComponent<TowerManager>();
            
            if (enemyManager == null)
                enemyManager = GetComponent<EnemyManager>();
        }
#endif
    }
}