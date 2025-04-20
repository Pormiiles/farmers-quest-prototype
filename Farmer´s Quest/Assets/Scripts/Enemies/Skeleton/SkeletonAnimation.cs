using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    private Animator anim;
    private PlayerAnimation playerAnim;
    private Player player;
    private Skeleton skeleton;

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

    public void PlayHit()
    {
        anim.SetTrigger("hit");
    }

    public void PlayDeath()
    {
        anim.ResetTrigger("hit");
        anim.SetTrigger("isDead");
    }

    public void onSkeletonAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);
        if (hit != null)
        {
            playerAnim.onHit();
            hit.GetComponent<Player>().TakeDamage(skeleton.damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
