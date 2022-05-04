// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Inputs
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""6425964a-3424-42b8-8ee5-957f903f7c36"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""a5bc1b68-5d02-48e5-92cc-cdf40c9ddc8c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""b0442180-356f-4588-800b-d69ce14378d9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""10bf794f-1189-4897-b70e-6e1083f90153"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""c5fbd33b-a514-4e80-ba38-5637ad150612"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookAround"",
                    ""type"": ""PassThrough"",
                    ""id"": ""59411571-11b5-42f5-89a6-6e540c28c4be"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""cb3a299c-5688-42e4-8705-e35833f08687"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""V2"",
                    ""id"": ""603fe7dd-89d1-4686-b7f2-8361ac57e61b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5616946f-e47e-49d4-ae81-3768c930c2af"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7c8ee24b-eaed-4494-b988-1cadf5d94ab1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c165e17b-a3bd-447e-8f41-a9ee65c524a7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e221d0dc-de4e-45bf-a816-503635bd9d16"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6af02f68-24e8-46b5-94f5-1350bf742c0e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""28bc6339-baa7-4605-bb78-70d092bdc617"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9bca33d-5f53-4b6d-a0f2-7aba97bf8f2c"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38f83891-d4bc-49da-a71a-6745bdc3e813"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LookAround"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5dddba89-b0c9-4c4a-9994-c88b880e4432"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
            m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
            m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
            m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
            m_Gameplay_LookAround = m_Gameplay.FindAction("LookAround", throwIfNotFound: true);
            m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
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

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Move;
        private readonly InputAction m_Gameplay_Jump;
        private readonly InputAction m_Gameplay_Shoot;
        private readonly InputAction m_Gameplay_Attack;
        private readonly InputAction m_Gameplay_LookAround;
        private readonly InputAction m_Gameplay_Pause;
        public struct GameplayActions
        {
            private @Controls m_Wrapper;
            public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Gameplay_Move;
            public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
            public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
            public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
            public InputAction @LookAround => m_Wrapper.m_Gameplay_LookAround;
            public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                    @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                    @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @LookAround.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLookAround;
                    @LookAround.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLookAround;
                    @LookAround.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLookAround;
                    @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Shoot.started += instance.OnShoot;
                    @Shoot.performed += instance.OnShoot;
                    @Shoot.canceled += instance.OnShoot;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @LookAround.started += instance.OnLookAround;
                    @LookAround.performed += instance.OnLookAround;
                    @LookAround.canceled += instance.OnLookAround;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IGameplayActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnShoot(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnLookAround(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
    }
}
