// GENERATED AUTOMATICALLY FROM 'Assets/_project/Settings/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Nara.MFGJS2020.Control
{
    public class @InputManager : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputManager()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""3a9243d1-fdea-4477-a711-5a0ee3f716b0"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""5980722f-d343-4d0c-9a29-db83e5498097"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""d9a5c264-6e2f-4b35-88ac-f576d7fe87e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""EndTurn"",
                    ""type"": ""Button"",
                    ""id"": ""90ffc69f-bc74-4939-ace8-6e5f2f43ebb9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8d6444ed-c204-42a8-8753-a9f3c22d80b3"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfae5946-7b01-40da-ad35-0236b1cf9d0d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2bcb1fc6-ce39-4f4e-a99b-010fe435c2a1"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa8830a3-0b5f-4137-9404-41fc3635cc9e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""01aefd3a-b86b-41af-99c5-149f3cfe1287"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""EndTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Default
            m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
            m_Default_Submit = m_Default.FindAction("Submit", throwIfNotFound: true);
            m_Default_Cancel = m_Default.FindAction("Cancel", throwIfNotFound: true);
            m_Default_EndTurn = m_Default.FindAction("EndTurn", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Default
        private readonly InputActionMap m_Default;
        private IDefaultActions m_DefaultActionsCallbackInterface;
        private readonly InputAction m_Default_Submit;
        private readonly InputAction m_Default_Cancel;
        private readonly InputAction m_Default_EndTurn;
        public struct DefaultActions
        {
            private @InputManager m_Wrapper;
            public DefaultActions(@InputManager wrapper) { m_Wrapper = wrapper; }
            public InputAction @Submit => m_Wrapper.m_Default_Submit;
            public InputAction @Cancel => m_Wrapper.m_Default_Cancel;
            public InputAction @EndTurn => m_Wrapper.m_Default_EndTurn;
            public InputActionMap Get() { return m_Wrapper.m_Default; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
            public void SetCallbacks(IDefaultActions instance)
            {
                if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
                {
                    @Submit.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSubmit;
                    @Cancel.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnCancel;
                    @EndTurn.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEndTurn;
                    @EndTurn.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEndTurn;
                    @EndTurn.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnEndTurn;
                }
                m_Wrapper.m_DefaultActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                    @EndTurn.started += instance.OnEndTurn;
                    @EndTurn.performed += instance.OnEndTurn;
                    @EndTurn.canceled += instance.OnEndTurn;
                }
            }
        }
        public DefaultActions @Default => new DefaultActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        public interface IDefaultActions
        {
            void OnSubmit(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnEndTurn(InputAction.CallbackContext context);
        }
    }
}
