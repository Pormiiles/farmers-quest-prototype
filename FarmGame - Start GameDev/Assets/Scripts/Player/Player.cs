using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player attributes
    private float playerInitialSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float playerRollSpeed;
    private bool _isPlayerRunning;
    private bool _isPlayerRolling;
    private bool _isPlayerCutting;

    private Rigidbody2D rig;
    private Vector2 _playerDirection;

    // Player properties
    public Vector2 playerDirection
    {
        get { return _playerDirection; }
        set { _playerDirection = value; }
    }

    // 
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

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerInitialSpeed = playerSpeed; // It storages the player큦 initial walk speed when the game starts 
    }

    private void Update()
    {
        onInput();
        onRun();
        onRoll();
        onCutting();
    }

    private void FixedUpdate()
    {
        onMove();  
    }

    #region Movement

    void onCutting()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isPlayerCutting = true;
            playerSpeed = 0f;
        } else if(Input.GetMouseButtonUp(0))
        {
            isPlayerCutting = false;
            playerSpeed = playerInitialSpeed;
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
            _isPlayerRunning = false; // No, the player큦 not running
        }
    }

    void onRoll()
    {
        if(Input.GetMouseButtonDown(1)) // If mouse큦 right button is pressed, the player rolls and its speed is inscreased
        {
            playerSpeed = playerRollSpeed;
            _isPlayerRolling = true;
        } 
        
        if(Input.GetMouseButtonUp(1)) { // If the mouse큦 right button stops being pressed, the player stops rolling
            playerSpeed = playerInitialSpeed;
            _isPlayerRolling = false;
        }
    }
    #endregion
}
