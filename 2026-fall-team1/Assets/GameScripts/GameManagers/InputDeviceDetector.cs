using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceDetector : MonoBehaviour
{
    public static event Action<InputDevice> OnDeviceChanged;
    private InputDevice currentDevice;

    private void OnEnable()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -= OnActionChange;
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change != InputActionChange.ActionStarted) return;

        InputAction action = (InputAction)obj;
        if (action.activeControl == null) return;

        InputDevice device = action.activeControl.device;
        if (device != currentDevice)
        {
            currentDevice = device;
            OnDeviceChanged?.Invoke(currentDevice);
            Debug.Log($"Input device changed to: {currentDevice.displayName}");

        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}





