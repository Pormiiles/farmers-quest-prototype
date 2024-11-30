using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player proprieties
    private float playerInitialSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float playerRollSpeed;
    private bool _isPlayerRunning;
    private bool _isPlayerRolling;

    private Rigidbody2D rig;
    private Vector2 _playerDirection;

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

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerInitialSpeed = playerSpeed; // It storages the player´s initial walk speed when the game starts 
    }

    private void Update()
    {
        onInput();
        onRun();
        onRoll();
    }

    private void FixedUpdate()
    {
        onMove();  
    }

    #region Movement
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
        if(Input.GetMouseButtonDown(1))
        {
            playerSpeed = playerRollSpeed;
            _isPlayerRolling = true;
        } 
        
        if(Input.GetMouseButtonUp(1)) {
            playerSpeed = playerInitialSpeed;
            _isPlayerRolling = false;
        }
    }
    #endregion
}
