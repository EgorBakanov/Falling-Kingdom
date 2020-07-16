﻿using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.Holders;
using Nara.MFGJS2020.States;
using UnityEngine;
using UnityEngine.InputSystem;

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
        [SerializeField] private CameraController cameraController;

        public GridHolder GridHolder => gridHolder;
        public GameStateMachine StateMachine => stateMachine;
        public AudioManager AudioManager => audioManager;
        public UIManager UiManager => uiManager;
        public TowerManager TowerManager => towerManager;
        public EnemyManager EnemyManager => enemyManager;
        public CameraController CameraController => cameraController;
        public bool HasNextLevel => _currentLevel < levelManager.Size;
        public int CurrentMoney { get; set; }
        public int CurrentTurn { get; set; }
        
        private int _currentLevel = 0;
        private InputManager _inputManager;
        
        public void NextLevel() => _currentLevel++;
        public Level GetCurrentLevel() => levelManager.Get(_currentLevel);

        private void Awake()
        {
            if (Instance != null)
                return;
            if (Instance == this)
                return;

            Instance = this;

            if (_inputManager == null)
            {
                _inputManager = new InputManager();
            }
        }

        private void Start()
        {
            stateMachine.Init(new BeginState(),new LoseState());
        }

        private void OnEnable()
        {
            _inputManager.Default.Submit.performed += HandleSubmit;
            _inputManager.Default.Cancel.performed += HandleCancel;
            _inputManager.Default.EndTurn.performed += HandleEndTurn;
            _inputManager.Default.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Default.Submit.performed -= HandleSubmit;
            _inputManager.Default.Cancel.performed -= HandleCancel;
            _inputManager.Default.EndTurn.performed -= HandleEndTurn;
            _inputManager.Default.Disable();
        }

        private void HandleSubmit(InputAction.CallbackContext obj) => stateMachine.OnSubmit();
        private void HandleCancel(InputAction.CallbackContext obj) => stateMachine.OnCancel();
        private void HandleEndTurn(InputAction.CallbackContext obj) => StateMachine.OnEndTurn();


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