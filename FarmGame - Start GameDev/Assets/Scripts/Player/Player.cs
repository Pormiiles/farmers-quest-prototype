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
    private bool _isPlayerAttacking;

    private Rigidbody2D rig;
    private Vector2 _playerDirection;

    private bool canRoll = true;
    [SerializeField] private float rollCooldown = 1.5f;

    [SerializeField] private int handlingTool;

    private PlayerItems playerItems;

    public float totalPlayerHealth;
    public float currentPlayerHealth;
    private bool isPlayerDead;

    public AudioClip playerDeathBGMClip;

    private DungeonUI dungeonUI;

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
    public bool IsPlayerAttacking { get => _isPlayerAttacking; set => _isPlayerAttacking = value; }

    private void Start()
    {
        currentPlayerHealth = totalPlayerHealth;
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();
        playerInitialSpeed = playerSpeed; // It storages the player´s initial walk speed when the game starts 
        dungeonUI = FindObjectOfType<DungeonUI>();
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

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HandlingTool = 4;
            }

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
        if(!isPlayerSpeedPaused)
        {
            onMove();
        }
    }

    #region Movement

    void onSwordAttack() // Ataque do player
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

    public void TakeDamage(float amount)
    {
        if (isPlayerDead) return;

        currentPlayerHealth -= amount;

        Debug.Log("Player levou dano! Vida atual: " + currentPlayerHealth);

        if (currentPlayerHealth <= 0)
        {
            isPlayerDead = true;
            Time.timeScale = 0f;
            dungeonUI.gameOverPanel.SetActive(true);
            AudioManager.instance.playBGM(playerDeathBGMClip);
        }
    }

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
        } else
        {
            isPlayerDigging = false;
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
        } else
        {
            isPlayerCutting = false;
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
        } else
        {
            IsPlayerWatering = false;
        }
    }

    void onInput()
    {
        _playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void onMove()
    {
        // Normaliza a direção para evitar que a velocidade aumente na diagonal
        Vector2 normalizedDirection = _playerDirection.normalized;

        // Move o player com a direção normalizada
        rig.MovePosition(rig.position + normalizedDirection * playerSpeed * Time.fixedDeltaTime);
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
        if (Input.GetMouseButtonDown(1) && canRoll) // Se o botão direito do mouse for pressionado e o player puder rolar
        {
            _isPlayerRolling = true; // Ativa a rolagem
            playerSpeed = playerRollSpeed; // Altera a velocidade para a de rolagem
            canRoll = false; // Bloqueia a rolagem até que o cooldown termine

            StartCoroutine(EndRoll()); // Finaliza a rolagem após o tempo da animação
        }
    }

    // Método para finalizar a rolagem após um tempo fixo
    private IEnumerator EndRoll()
    {
        yield return new WaitForSeconds(0.1f); // Duração da animação de rolagem (ajuste conforme necessário)
        _isPlayerRolling = false; // Finaliza a rolagem
        playerSpeed = playerInitialSpeed; // Restaura a velocidade inicial do player

        StartCoroutine(RollCooldown()); // Inicia o cooldown para liberar a rolagem novamente
    }

    // Método para controlar o cooldown
    private IEnumerator RollCooldown()
    {
        yield return new WaitForSeconds(rollCooldown); // Tempo do cooldown
        canRoll = true; // Permite que o player role novamente
    }

    public void DisablePlayerControls()
    {
        IsPlayerSpeedPaused = true;

        // Cancela qualquer ação ativa
        isPlayerDigging = false;
        isPlayerCutting = false;
        IsPlayerWatering = false;
        IsPlayerAttacking = false;
        _isPlayerRolling = false;
        _isPlayerRunning = false;

        // Zera movimento do player imediatamente
        _playerDirection = Vector2.zero;
        playerSpeed = 0f;
    }

    public void EnablePlayerControls()
    {
        IsPlayerSpeedPaused = false;

        // Restaura a velocidade normal
        playerSpeed = playerInitialSpeed;
    }

    #endregion

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Skeleton skeleton = collision.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(1f);
            }
        }
    }
}
