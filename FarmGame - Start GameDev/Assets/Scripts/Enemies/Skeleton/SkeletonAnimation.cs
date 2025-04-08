using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private PlayerAnimation playerAnim;
    private Skeleton skeleton;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindObjectOfType<PlayerAnimation>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void onSkeletonAttack() // Ataque do esqueleto
    {
        if(!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                // Skeleton detecta e começa a bater no player (animação do player tomando hit)
                playerAnim.onHit();
            }
        }  
    }

    public void onSkeletonHit() // Hit sofrido pelo esqueleto
    {
        if(skeleton.currentHealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("isDead");

            Destroy(skeleton.gameObject, 1f);
        } else
        {
            anim.SetTrigger("hit");
            skeleton.currentHealth--;

            // Fill Amount do LifeBar recebe a vida atual dividida pela vida total do inimigo
            skeleton.healthBar.fillAmount = skeleton.currentHealth / skeleton.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
