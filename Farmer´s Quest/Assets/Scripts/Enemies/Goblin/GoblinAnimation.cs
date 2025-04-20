using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private PlayerAnimation playerAnim;
    private Goblin goblin;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnimation>();
        goblin = GetComponentInParent<Goblin>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void PlayHit()
    {
        anim.SetTrigger("isHit");
    }

    public void PlayDeath()
    {
        anim.ResetTrigger("isHit");
        anim.SetTrigger("isDead");
    }

    public void onGoblinAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
        if (hit != null)
        {
            playerAnim.onHit();
            hit.GetComponent<Player>().TakeDamage(goblin.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
