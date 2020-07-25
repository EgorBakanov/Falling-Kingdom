using System;
using System.Collections;
using Nara.MFGJS2020.Core;
using Nara.MFGJS2020.Generators;
using Nara.MFGJS2020.Holders;
using Nara.MFGJS2020.States;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Nara.MFGJS2020.Control
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameStateMachine stateMachine;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GridHolder gridHolder;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private UiManager uiManager;
        [SerializeField] private TowerManager towerManager;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private SelectionManager selectionManager;

        public GridHolder GridHolder => gridHolder;
        public GameStateMachine StateMachine => stateMachine;
        public AudioManager AudioManager => audioManager;
        public UiManager UiManager => uiManager;
        public TowerManager TowerManager => towerManager;
        public EnemyManager EnemyManager => enemyManager;
        public CameraController CameraController => cameraController;
        public SelectionManager SelectionManager => selectionManager;
        public bool HasNextLevel => _currentLevel < levelManager.Size;

        public void InitMoney(int val)
        {
            CurrentMoney = val;
        }

        public IEnumerator SetMoney(int val)
        {
            var old = CurrentMoney;
            CurrentMoney = val;
            if (old != CurrentMoney)
            {
                AudioManager.PlayMoneyChange();
                yield return UiManager.UpdateMoneyCounter();
            }
        }

        public int CurrentMoney { get; private set; }
        public int CurrentTurn { get; set; }

        private int _currentLevel = -1;
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
            stateMachine.Init(new BeginState(), new LoseState());
        }

        private void OnEnable()
        {
            _inputManager.Default.Submit.performed += HandleSubmit;
            _inputManager.Default.Cancel.performed += HandleCancel;
            _inputManager.Default.EndTurn.performed += HandleEndTurn;
            _inputManager.Default.Enable();

            CameraController.FreeLookAction = _inputManager.Default.Camera;
        }

        private void OnDisable()
        {
            _inputManager.Default.Submit.performed -= HandleSubmit;
            _inputManager.Default.Cancel.performed -= HandleCancel;
            _inputManager.Default.EndTurn.performed -= HandleEndTurn;
            _inputManager.Default.Disable();

            CameraController.FreeLookAction = null;
        }

        private void HandleSubmit(InputAction.CallbackContext obj) => stateMachine.OnSubmit();
        private void HandleCancel(InputAction.CallbackContext obj) => stateMachine.OnCancel();
        private void HandleEndTurn(InputAction.CallbackContext obj) => StateMachine.OnEndTurn();

        private void Update()
        {
            // testing
            if (Keyboard.current.numpad0Key.isPressed)
                RunLevel(0);
            if (Keyboard.current.numpad1Key.isPressed)
                RunLevel(1);
            if (Keyboard.current.numpad2Key.isPressed)
                RunLevel(2);
            if (Keyboard.current.numpad3Key.isPressed)
                RunLevel(3);
            if (Keyboard.current.numpad4Key.isPressed)
                RunLevel(4);
            if (Keyboard.current.numpadPlusKey.isPressed)
                StartCoroutine(SetMoney(200));
        }

        private void RunLevel(int id)
        {
            _currentLevel = id;
            stateMachine.SetState(new InitLevelState());
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            if (stateMachine == null)
                stateMachine = FindObjectOfType<GameStateMachine>();

            if (levelManager == null)
                levelManager = FindObjectOfType<LevelManager>();

            if (audioManager == null)
                audioManager = FindObjectOfType<AudioManager>();

            if (gridHolder == null)
                gridHolder = FindObjectOfType<GridHolder>();

            if (uiManager == null)
                uiManager = FindObjectOfType<UiManager>();

            if (towerManager == null)
                towerManager = FindObjectOfType<TowerManager>();

            if (enemyManager == null)
                enemyManager = FindObjectOfType<EnemyManager>();

            if (selectionManager == null)
                selectionManager = FindObjectOfType<SelectionManager>();

            if (cameraController == null)
                cameraController = FindObjectOfType<CameraController>();
        }
#endif
    }
}