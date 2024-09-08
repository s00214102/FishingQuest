using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class InputHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    private string exploreActionMapName = "Generic";
    
    // Analog sticks
    private InputAction moveAction;
    private InputAction lookAction;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    
    // Face buttons
    private InputAction btnSouthAction;
    private InputAction btnWestAction;
    private InputAction btnEastAction;
    private InputAction btnNorthAction;
    public bool btnSouthTriggered { get; private set; }
    public bool btnWestTriggered { get; private set; }
    public bool btnEastTriggered { get; private set; }
    public float btnNorthValue { get; private set; }
    

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
        
        // Action map setup
        moveAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Move");
        lookAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Look");
        btnSouthAction = playerControls.FindActionMap(exploreActionMapName).FindAction("BtnSouth");
        btnWestAction = playerControls.FindActionMap(exploreActionMapName).FindAction("BtnWest");
        btnEastAction = playerControls.FindActionMap(exploreActionMapName).FindAction("BtnEast");
        btnNorthAction = playerControls.FindActionMap(exploreActionMapName).FindAction("BtnNorth");
        RegisterInputActions();
    }
    private void RegisterInputActions()
    {
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        btnSouthAction.performed += context => btnSouthTriggered = true;
        btnSouthAction.canceled += context => btnSouthTriggered = false;

        btnWestAction.performed += context => btnWestTriggered = true;
        btnWestAction.canceled += context => btnWestTriggered = false;
        
        btnEastAction.performed += context => btnEastTriggered = true;
        btnEastAction.canceled += context => btnEastTriggered = false;

        btnNorthAction.performed += context => btnNorthValue = context.ReadValue<float>();
        btnNorthAction.canceled += context => btnNorthValue = 0f;
    }
    
    #region InputAction methods
    private float CalculateVector2Input(float input)
    {
        float _input = input;

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

        return _input;
    }
    public float GetHorizontalMovementInput()
    {
        return CalculateVector2Input(MoveInput.x);
    }
    public float GetVerticalMovementInput()
    {
        return CalculateVector2Input(MoveInput.y);
    }
    public float GetHorizontalCameraInput()
    {
        return CalculateVector2Input(LookInput.x);
    }
    public float GetVerticalCameraInput()
    {
        return CalculateVector2Input(LookInput.y);;
    }
    
    #endregion
    
    // make this an event that other dudes can sub to
    // public bool ToolButtonPressed()
    // {
    //     return ToolTriggered;
    // }

    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        btnSouthAction.Enable();
        btnWestAction.Enable();
        btnEastAction.Enable();
        btnNorthAction.Enable();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        btnSouthAction.Disable();
        btnWestAction.Disable();
        btnEastAction.Disable();
        btnNorthAction.Disable();
    }
}
