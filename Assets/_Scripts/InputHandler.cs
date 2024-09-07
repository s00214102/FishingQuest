using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;

    [SerializeField] private string actionMapName = "Explore";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction toolAction;
    private InputAction sprintAction;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool JumpTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }
    public float SprintValue { get; private set; }

    public float deadZoneThreshold = 0.2f;

    public static InputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction("Move");
        lookAction = playerControls.FindActionMap(actionMapName).FindAction("Look");
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction("Jump");
        interactAction = playerControls.FindActionMap(actionMapName).FindAction("Interact");
        toolAction = playerControls.FindActionMap(actionMapName).FindAction("Tool");
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction("Sprint");
        RegisterInputActions();
    }

    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        interactAction.performed += context => InteractTriggered = true;
        interactAction.canceled += context => InteractTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
    }

    public float GetHorizontalMovementInput()
    {
        float _input = MoveInput.x;

        // Apply dead zone logic first
        if (Mathf.Abs(_input) < deadZoneThreshold)
        {
            _input = 0f;
        }
        else
        {
            // Apply Mathf.Sign after dead zone check
            _input = Mathf.Sign(_input);
        }

        Debug.Log($"Vertical movement input: {_input}");
        return _input;
    }

    public float GetVerticalMovementInput()
    {
        float _input = MoveInput.y;

        // Apply dead zone logic first
        if (Mathf.Abs(_input) < deadZoneThreshold)
        {
            _input = 0f;
        }
        else
        {
            // Apply Mathf.Sign after dead zone check
            _input = Mathf.Sign(_input);
        }

        Debug.Log($"Vertical movement input: {_input}");
        return _input;
    }

    public float GetHorizontalCameraInput()
    {
        float _input = LookInput.x;

        // Apply dead zone logic first
        if (Mathf.Abs(_input) < deadZoneThreshold)
        {
            _input = 0f;
        }
        else
        {
            // Apply Mathf.Sign after dead zone check
            _input = Mathf.Sign(_input);
        }
        Debug.Log($"Horizontal camera input: {_input}");
        return _input;
    }

    public float GetVerticalCameraInput()
    {
        float _input = LookInput.y;

        // Apply dead zone logic first
        if (Mathf.Abs(_input) < deadZoneThreshold)
        {
            _input = 0f;
        }
        else
        {
            // Apply Mathf.Sign after dead zone check
            _input = Mathf.Sign(_input);
        }
        Debug.Log($"Vertical camera input: {_input}");
        return _input;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        interactAction.Enable();
        toolAction.Enable();
        sprintAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        interactAction.Disable();
        toolAction.Disable();
        sprintAction.Disable();
    }
}
