using System.Collections;
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

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        castingScript = FindObjectOfType<Casting>();
        enemyAnim = GetComponent<SkeletonAnimation>();
    }

    void Update()
    {
        onMove();
        onRun();
        onSwordAttack();

        if (player.isPlayerDead)
        {
            onPlayerDeath();
            return;
        }

        if (isHitting)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    void onMove()
    {
        if (player.playerDirection.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 1);
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.playerDirection.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (player.playerDirection.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (player.isPlayerCutting) anim.SetInteger("transition", 3);
        if (player.isPlayerDigging) anim.SetInteger("transition", 4);
        if (player.IsPlayerWatering) anim.SetInteger("transition", 5);
    }

    void onRun()
    {
        if (player.isPlayerRunning && player.playerDirection.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2);
        }
    }

    void onSwordAttack()
    {
        if (player.IsPlayerAttacking)
        {
            anim.SetInteger("transition", 6);
        }
    }

    void onPlayerDeath()
    {
        anim.ResetTrigger("isHit");
        anim.SetTrigger("isDead");
    }

    void onAttackEnemy()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            Skeleton skeleton = hit.GetComponentInParent<Skeleton>();
            Goblin goblin = hit.GetComponentInParent<Goblin>();

            if (skeleton != null) skeleton.TakeDamage(1f);
            if (goblin != null) goblin.TakeDamage(1f);
        }
    }

    public void onIdle()
    {
        anim.SetInteger("transition", 0);
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

    public void onHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("isHit");
            isHitting = true;
        }
    }
}
