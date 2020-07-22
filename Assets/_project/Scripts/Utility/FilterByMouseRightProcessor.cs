using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Nara.MFGJS2020.Utility
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class FilterByMouseRightProcessor : InputProcessor<Vector2>
    {
#if UNITY_EDITOR
        static FilterByMouseRightProcessor()
        {
            Initialize();
        }
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            InputSystem.RegisterProcessor<FilterByMouseRightProcessor>();
        }

        public override Vector2 Process(Vector2 value, InputControl control)
        {
            return Mouse.current.rightButton.isPressed ? value : Vector2.zero;
        }
    }
}