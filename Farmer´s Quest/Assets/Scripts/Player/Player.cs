using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Atributos e Componentes

    private Rigidbody2D rig;
    private PlayerItems playerItems;
    private DungeonUI dungeonUI;

    [Header("Movimento e status")]
    private bool isPlayerSpeedPaused;
    private float playerInitialSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float playerRollSpeed;
    private Vector2 _playerDirection;
    private bool canRoll = true;
    [SerializeField] private float rollCooldown = 1.5f;

    [Header("Ferramentas e ações")]
    [SerializeField] private int handlingTool;
    private bool _isPlayerRunning;
    private bool _isPlayerRolling;
    private bool _isPlayerCutting;
    private bool _isPlayerDigging;
    private bool _isPlayerWatering;
    private bool _isPlayerAttacking;

    [Header("Vida e morte")]
    public float totalPlayerHealth;
    public float currentPlayerHealth;
    public bool isPlayerDead;
    public AudioClip playerDeathBGMClip;

    #endregion

    #region Propriedades

    public Vector2 playerDirection { get => _playerDirection; set => _playerDirection = value; }
    public bool isPlayerRunning { get => _isPlayerRunning; set => _isPlayerRunning = value; }
    public bool isPlayerRolling { get => _isPlayerRolling; set => _isPlayerRolling = value; }
    public bool isPlayerCutting { get => _isPlayerCutting; set => _isPlayerCutting = value; }
    public bool isPlayerDigging { get => _isPlayerDigging; set => _isPlayerDigging = value; }
    public bool IsPlayerWatering { get => _isPlayerWatering; set => _isPlayerWatering = value; }
    public int HandlingTool { get => handlingTool; set => handlingTool = value; }
    public bool IsPlayerSpeedPaused { get => isPlayerSpeedPaused; set => isPlayerSpeedPaused = value; }
    public bool IsPlayerAttacking { get => _isPlayerAttacking; set => _isPlayerAttacking = value; }

    #endregion

    #region Ciclo de Vida

    private void Start()
    {
        currentPlayerHealth = totalPlayerHealth;
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        playerInitialSpeed = playerSpeed;
        dungeonUI = FindObjectOfType<DungeonUI>();
    }

    private void Update()
    {
        if (!isPlayerSpeedPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) HandlingTool = 1;
            if (Input.GetKeyDown(KeyCode.Alpha2)) HandlingTool = 2;
            if (Input.GetKeyDown(KeyCode.Alpha3)) HandlingTool = 3;
            if (Input.GetKeyDown(KeyCode.Alpha4)) HandlingTool = 4;

            onInput();
            onRun();
            onRoll();
            onCutting();
            onDigging();
            onWatering();
            onSwordAttack();
        }
    }

    private void FixedUpdate()
    {
        if (!isPlayerSpeedPaused)
        {
            onMove();
        }
    }

    #endregion

    #region Ações do Jogador

    void onInput()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void onMove()
    {
        Vector2 normalizedDirection = _playerDirection.normalized;
        rig.MovePosition(rig.position + normalizedDirection * playerSpeed * Time.fixedDeltaTime);
    }

    void onRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerSpeed = playerRunSpeed;
            _isPlayerRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = playerInitialSpeed;
            _isPlayerRunning = false;
        }
    }

    void onRoll()
    {
        if (Input.GetMouseButtonDown(1) && canRoll)
        {
            _isPlayerRolling = true;
            GetComponent<Animator>().SetTrigger("isRolling");
            playerSpeed = playerRollSpeed;
            canRoll = false;
            StartCoroutine(EndRoll());
        }
    }

    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(0.09f);
        _isPlayerRolling = false;
        playerSpeed = playerInitialSpeed;
        GetComponent<Animator>().SetTrigger("isRolling");
        StartCoroutine(RollCooldown());
    }

    private IEnumerator RollCooldown()
    {
        yield return new WaitForSeconds(rollCooldown);
        canRoll = true;
    }

    void onCutting()
    {
        if (HandlingTool == 2)
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
        else
        {
            isPlayerCutting = false;
        }
    }

    void onDigging()
    {
        if (HandlingTool == 1)
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
        else
        {
            isPlayerDigging = false;
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

            if (IsPlayerWatering)
            {
                playerItems.waterTotal -= 0.02f;
            }
        }
        else
        {
            IsPlayerWatering = false;
        }
    }

    void onSwordAttack()
    {
        if (HandlingTool == 4)
        {
            if (Input.GetMouseButtonDown(0))
            {
                IsPlayerAttacking = true;
                playerSpeed = 0f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                IsPlayerAttacking = false;
                playerSpeed = playerInitialSpeed;
            }
        }
        else
        {
            IsPlayerAttacking = false;
        }
    }

    #endregion

    #region Vida e Morte

    public void TakeDamage(float amount)
    {
        if (isPlayerDead) return;

        currentPlayerHealth -= amount;

        Debug.Log("Player levou dano! Vida atual: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            isPlayerDead = true;
            DisablePlayerControls();
            StartCoroutine(playerDeathScreenAfterCutscene());
        }
    }

    private IEnumerator playerDeathScreenAfterCutscene()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        dungeonUI.gameOverPanel.SetActive(true);
        AudioManager.instance.playBGM(playerDeathBGMClip);
    }

    public void DisablePlayerControls()
    {
        IsPlayerSpeedPaused = true;
        isPlayerDigging = false;
        isPlayerCutting = false;
        IsPlayerWatering = false;
        IsPlayerAttacking = false;
        _isPlayerRolling = false;
        _isPlayerRunning = false;
        _playerDirection = Vector2.zero;
        playerSpeed = 0f;
    }

    public void EnablePlayerControls()
    {
        IsPlayerSpeedPaused = false;
        playerSpeed = playerInitialSpeed;
    }

    #endregion

    #region Interações

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Skeleton skeleton = collision.GetComponent<Skeleton>();
            Goblin goblin = collision.GetComponent<Goblin>();

            if (skeleton != null) skeleton.TakeDamage(1f);
            if (goblin != null) goblin.TakeDamage(1f);
        }
    }

    #endregion
}
