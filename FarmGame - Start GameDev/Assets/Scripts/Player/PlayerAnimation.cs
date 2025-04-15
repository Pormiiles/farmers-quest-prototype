using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;
    private SkeletonAnimation enemyAnim;
    private Casting castingScript;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private bool isHitting;
    [SerializeField] private float timeCount;
    [SerializeField] private float recoveryTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        castingScript = FindObjectOfType<Casting>();
        enemyAnim = GetComponent<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
        onRun();
        onSwordAttack();
  

        if (isHitting)
        {
            timeCount += Time.deltaTime;

            // Espera o tempo de cooldown acabar para o esqueleto bate no player novamente
            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement 

    public void onIdle()
    {
        anim.SetInteger("transition", 0);
    }

    void onAttackEnemy()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            Skeleton skeleton = hit.GetComponentInParent<Skeleton>();
            if (skeleton != null)
            {
                skeleton.TakeDamage(1f); // Aplica 1 ponto de dano
            }
        }
    }


    void onSwordAttack()
    {
        if(player.IsPlayerAttacking)
        {
            anim.SetInteger("transition", 6);
        }
    }

    void onMove()
    {
        if (player.playerDirection.sqrMagnitude > 0) // Se o player estiver se movimentando
        {
            anim.SetInteger("transition", 1);
        }
        else
        {
            anim.SetInteger("transition", 0); // Se não estiver andando, ele entra na animação de Idle
        }

        // Verifica a direção horizontal
        if (player.playerDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Direita
        }
        else if (player.playerDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f); // Esquerda (espelha no eixo X - o sprite é invertido)
        }

        if (player.isPlayerCutting)
        {
            anim.SetInteger("transition", 3);
        }

        if (player.isPlayerDigging)
        {
            anim.SetInteger("transition", 4);
        }

        if (player.IsPlayerWatering)
        {
            anim.SetInteger("transition", 5);
        }
    }

    void onRun()
    {
        if (player.isPlayerRunning && player.playerDirection.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2);
        }
    }

    public void onCastingStart()
    {
        anim.SetTrigger("isCasting");
        player.IsPlayerSpeedPaused = true;
    }

    public void onCastingEnd()
    {
        castingScript.onCasting();
        player.IsPlayerSpeedPaused = false;
    }

    public void onBuildingStart()
    {
        anim.SetBool("isHammering", true);
    }

    public void onBuildingEnd()
    {
        anim.SetBool("isHammering", false);
    }

    public void onHit() // Recebe o dano
    {
        if (!isHitting)
        {
            anim.SetTrigger("isHit");
            isHitting = true;
        }
    }
    #endregion
}
