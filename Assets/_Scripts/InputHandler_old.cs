using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class InputHandler_old : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    // public enum ActionMapType
    // {
    //     Explore, // moving around the world
    //     Dialogue, // talking to an NPC
    //     Fishing, // fishing
    // }
    
    private string exploreActionMapName = "Explore";
    private string fishingActionMapName = "Fishing";

    [SerializeField] private InputActionMap currentActionMap;
    
    // SHARED ACTIONS
    private InputAction moveAction;
    private InputAction lookAction;
    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    
    // EXPLORE ACTIONS
    private InputAction jumpAction;
    private InputAction interactAction;
    private InputAction toolAction;
    private InputAction sprintAction;
    public bool JumpTriggered { get; private set; }
    public bool InteractTriggered { get; private set; }
    public bool ToolTriggered { get; private set; }
    public float SprintValue { get; private set; }

   
    
    // FISHING ACTIONS
    private InputAction castAction;
    private InputAction catchAction;
    private InputAction reelAction;
    private InputAction cancelAction;
    private InputAction wiggleAction; // these could be the same as move and look, but il separate them incase ppl want to rebind
    private InputAction aimAction;
    
    public bool CastTriggered { get; private set; }
    public bool catchTriggered { get; private set; }
    public bool ReelTriggered { get; private set; }
    public bool cancelTriggered { get; private set; }
    public Vector2 WiggleInput { get; private set; }
    public Vector2 AimInput { get; private set; }

    public float deadZoneThreshold = 0.2f;
    public static InputHandler_old Instance { get; private set; }

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
        
        // Explore action map setup
        moveAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Move");
        lookAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Look");
        jumpAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Jump");
        interactAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Interact");
        toolAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Tool");
        sprintAction = playerControls.FindActionMap(exploreActionMapName).FindAction("Sprint");
        RegisterExploreInputActions();
        
        // Fishing action map setup
        castAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Cast");
        catchAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Catch");
        reelAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Reel");
        cancelAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Cancel");
        wiggleAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Wiggle");
        aimAction = playerControls.FindActionMap(fishingActionMapName).FindAction("Aim");
        RegisterFishingInputActions();

        
        // Initialize with the default action map
        SwitchActionMap("Explore");  // Default action map when the game starts
    }
    private void RegisterExploreInputActions()
    {
        //EXPLORE 
        moveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => MoveInput = Vector2.zero;

        lookAction.performed += context => LookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => LookInput = Vector2.zero;

        jumpAction.performed += context => JumpTriggered = true;
        jumpAction.canceled += context => JumpTriggered = false;

        interactAction.performed += context => InteractTriggered = true;
        interactAction.canceled += context => InteractTriggered = false;
        
        toolAction.performed += context => ToolTriggered = true;
        toolAction.canceled += context => ToolTriggered = false;

        sprintAction.performed += context => SprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => SprintValue = 0f;
    }
    private void RegisterFishingInputActions()
    {
        castAction.performed += context => CastTriggered = true;
        castAction.canceled += context => CastTriggered = false;
        
        catchAction.performed += context => catchTriggered = true;
        catchAction.canceled += context => catchTriggered = false;
        
        reelAction.performed += context => ReelTriggered = true;
        reelAction.canceled += context => ReelTriggered = false;
        
        cancelAction.performed += context => cancelTriggered = true;
        cancelAction.canceled += context => cancelTriggered = false;
        
        wiggleAction.performed += context => WiggleInput = context.ReadValue<Vector2>();
        wiggleAction.canceled += context => WiggleInput = Vector2.zero;
        
        aimAction.performed += context => AimInput = context.ReadValue<Vector2>();
        aimAction.canceled += context => AimInput = Vector2.zero;
    }
    
    // Method to switch action maps
    public void SwitchActionMap(string newActionMapName)
    {
        // Disable the current action map
        if (currentActionMap != null)
        {
            currentActionMap.Disable();
        }

        // Find the new action map
        currentActionMap = playerControls.FindActionMap(newActionMapName);

        if (currentActionMap != null)
        {
            // Enable the new action map
            currentActionMap.Enable();

            // Switch based on the new action map
            switch (newActionMapName)
            {
                case "Explore":
                    DisableFishingActions();
                    
                    EnableExploreActions();
                    break;

                case "Fishing":
                    DisableExploreActions();
                    
                    EnableFishingActions();
                    break;
                

                // Add more cases for other action maps as needed
                default:
                    Debug.LogWarning($"Action map '{newActionMapName}' not recognized.");
                    break;
            }

            
        }
        else
        {
            Debug.LogError($"Action map '{newActionMapName}' not found in {playerControls.name}");
        }
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
    // EXPLORE
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
    // FISHING
    public float GetHorizontalAimInput()
    {
        return CalculateVector2Input(AimInput.x);
    }
    public float GetVerticalAimInput()
    {
        return CalculateVector2Input(AimInput.y);
    }
    public float GetHorizontalWiggleInput()
    {
        return CalculateVector2Input(WiggleInput.x);
    }
    public float GetVerticalWiggleInput()
    {
        return CalculateVector2Input(WiggleInput.y);
    }
    #endregion
    // make this an event that other dudes can sub to
    // public bool ToolButtonPressed()
    // {
    //     return ToolTriggered;
    // }
    private void EnableExploreActions()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        interactAction.Enable();
        toolAction.Enable();
        sprintAction.Enable();
    }

    private void DisableExploreActions()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        interactAction.Disable();
        toolAction.Disable();
        sprintAction.Disable();
    }
    
    private void EnableFishingActions()
    {
        castAction.Enable();
        catchAction.Enable();
        reelAction.Enable();
        cancelAction.Enable();
        wiggleAction.Enable();
        aimAction.Enable();
    }

    private void DisableFishingActions()
    {
        castAction.Disable();
        catchAction.Disable();
        reelAction.Disable();
        cancelAction.Disable();
        wiggleAction.Disable();
        aimAction.Disable();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        DisableExploreActions();
        DisableFishingActions();
    }
}
