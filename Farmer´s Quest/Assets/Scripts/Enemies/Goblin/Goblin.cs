using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System;

public class Goblin : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform playerPosition;
    private Player player;
    [SerializeField] private GoblinAnimation anim;
    [SerializeField] public LayerMask enemyLayer;

    [SerializeField] public float totalHealth = 3;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;

    public float damage = 5f;

    public event Action onDeath;

    void Start()
    {
        currentHealth = totalHealth;
        playerPosition = FindObjectOfType<Player>().transform;
        anim = GetComponentInChildren<GoblinAnimation>();
        player = GetComponent<Player>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
    }

    void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(playerPosition.position);

            if (Vector3.Distance(transform.position, agent.destination) > agent.stoppingDistance)
            {
                Vector3 direction = (agent.steeringTarget - transform.position).normalized;
                transform.position += direction * agent.speed * Time.deltaTime;
            }

            if (Vector2.Distance(transform.position, playerPosition.transform.position) <= agent.stoppingDistance)
            {
                anim.PlayAnim(2);
            }
            else
            {
                anim.PlayAnim(1);
            }

            float posX = playerPosition.transform.position.x - transform.position.x;

            transform.eulerAngles = (posX > 0) ? new Vector2(0, 0) : new Vector2(0, 180);
        }

        AvoidOtherEnemies();
    }

    void AvoidOtherEnemies()
    {
        Collider2D[] others = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
        foreach (Collider2D other in others)
        {
            if (other.gameObject != this.gameObject)
            {
                Vector2 pushDir = (transform.position - other.transform.position).normalized;
                transform.position += (Vector3)(pushDir * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(float amount) // Goblin recebe dano
    {
        if (isDead) return;

        currentHealth -= amount;

        if (healthBar != null)
            healthBar.fillAmount = currentHealth / totalHealth;

        if (anim != null)
            anim.PlayHit(); // Toca animação de hit

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        Debug.Log("Goblin morreu");

        anim.PlayDeath(); // Toca a animação de morte

        onDeath?.Invoke(); // Notifica o WaveManager

        Destroy(gameObject, 1f); // Destroi após 1s
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
        }
    }
}
