using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Gameplay.Character
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] 
        private UnityEvent<Vector2> _updateJoystickInputValue;
        
        [FormerlySerializedAs("_onActivateGlider")] [SerializeField] 
        private UnityEvent _onHoldJoystick;
        
        [FormerlySerializedAs("_onDeactivateGlider")] [SerializeField] 
        private UnityEvent _onReleaseJoystick;
        
        [SerializeField] 
        private UnityEvent _onRunButtonClicked;

        [SerializeField] 
        private bool _startEnabled;

        private VariableJoystick _joystick;
        private bool _inputsEnabled;
        
        private PlayerInput _playerInputs;

        private void Awake()
        {
            _playerInputs = new PlayerInput();
            _joystick = FindObjectOfType<VariableJoystick>();
            _joystick.OnJoystickDown += JoystickDown;
            _joystick.OnJoystickUp += JoystickUp;
            
            if (_startEnabled)
            {
                _playerInputs.Player.Enable();
            }
            else
            {
                _playerInputs.Player.Disable();
            }
        }

        private void JoystickUp()
        {
            if (_inputsEnabled == false) return;
            _onReleaseJoystick?.Invoke();
        }

        private void JoystickDown()
        {
            if (_inputsEnabled == false) return;
            _onHoldJoystick?.Invoke();
        }

        private void OnEnable()
        {
            _playerInputs.Player.Run.performed += OnRunButtonClicked;
        }
        
        private void OnDisable()
        {
            _inputsEnabled = false;
            _playerInputs.Player.Run.performed -= OnRunButtonClicked;
        }
        
        public void EnableInputs()
        {
            _inputsEnabled = true;
            _playerInputs.Player.Enable();
        }

        public void DisableInputs()
        {
            _inputsEnabled = false;
            _playerInputs.Player.Disable();
        }

        private void Update()
        {
            var movementValue = _joystick.Direction;
            _updateJoystickInputValue?.Invoke(movementValue);
        }
        
        private void OnRunButtonClicked(InputAction.CallbackContext obj)
        {
            _onRunButtonClicked?.Invoke();
        }
    }
}