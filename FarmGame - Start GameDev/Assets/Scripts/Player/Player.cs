using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player attributes
    private bool isPlayerSpeedPaused;

    private float playerInitialSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float playerRollSpeed;
    private bool _isPlayerRunning;
    private bool _isPlayerRolling;
    private bool _isPlayerCutting;
    private bool _isPlayerDigging;
    private bool _isPlayerWatering;

    private Rigidbody2D rig;
    private Vector2 _playerDirection;

    private int handlingTool;

    private PlayerItems playerItems;

    // Player properties
    public Vector2 playerDirection
    {
        get { return _playerDirection; }
        set { _playerDirection = value; }
    }

    public bool isPlayerRunning
    {
        get { return _isPlayerRunning; }
        set { _isPlayerRunning = value;  }
    }

    public bool isPlayerRolling
    {
        get { return _isPlayerRolling; }
        set { _isPlayerRolling = value; }
    }

    public bool isPlayerCutting
    {
        get { return _isPlayerCutting; }
        set { _isPlayerCutting = value; }
    }

    public bool isPlayerDigging { 
        get => _isPlayerDigging; 
        set => _isPlayerDigging = value; 
    }

    public bool IsPlayerWatering { get => _isPlayerWatering; set => _isPlayerWatering = value; }
    public int HandlingTool { get => handlingTool; set => handlingTool = value; }
    public bool IsPlayerSpeedPaused { get => isPlayerSpeedPaused; set => isPlayerSpeedPaused = value; }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        playerInitialSpeed = playerSpeed; // It storages the player´s initial walk speed when the game starts 
    }

    private void Update()
    {
        if(!isPlayerSpeedPaused)
        {
            // Lógica simples de interface de Inventário
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandlingTool = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandlingTool = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HandlingTool = 3;
            }

            onInput();
            onRun();
            onRoll();
            onCutting();
            onDigging();
            onWatering();
        }
    }

    private void FixedUpdate()
    {
        if(!isPlayerSpeedPaused)
        {
            onMove();
        }
    }

    #region Movement

    void onDigging()
    {
        if(HandlingTool == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPlayerDigging = true;
                playerSpeed = 0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPlayerDigging = false;
                playerSpeed = playerInitialSpeed;
            }
        }
    }

    void onCutting()
    {
        if(HandlingTool == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isPlayerCutting = true;
                playerSpeed = 0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isPlayerCutting = false;
                playerSpeed = playerInitialSpeed;
            }
        }    
    }

    void onWatering()
    {
        if (HandlingTool == 3)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.waterTotal > 0)
            {
                IsPlayerWatering = true;
                playerSpeed = 0f;
            }
            else if (Input.GetMouseButtonUp(0) || playerItems.waterTotal < 0)
            {
                IsPlayerWatering = false;
                playerSpeed = playerInitialSpeed;
            }

            if(IsPlayerWatering)
            {
                playerItems.waterTotal -= 0.02f;
            }
        }
    }

    void onInput()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void onMove()
    {
        rig.MovePosition(rig.position + playerDirection * playerSpeed * Time.fixedDeltaTime);
    }

    void onRun()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed = playerRunSpeed;
            _isPlayerRunning = true; // Yes, the player's running
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = playerInitialSpeed;
            _isPlayerRunning = false; // No, the player´s not running
        }
    }

    void onRoll()
    {
        if(Input.GetMouseButtonDown(1)) // If mouse´s right button is pressed, the player rolls and its speed is inscreased
        {
            playerSpeed = playerRollSpeed;
            _isPlayerRolling = true;
        } 
        
        if(Input.GetMouseButtonUp(1)) { // If the mouse´s right button stops being pressed, the player stops rolling
            playerSpeed = playerInitialSpeed;
            _isPlayerRolling = false;
        }
    }
    #endregion
}
