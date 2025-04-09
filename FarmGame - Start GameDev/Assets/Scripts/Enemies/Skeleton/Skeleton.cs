using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    private Transform player;
    [SerializeField] private SkeletonAnimation anim;
    [SerializeField] public LayerMask enemyLayer;

    [SerializeField] public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;
    
    void Start()
    {
        currentHealth = totalHealth;

        player = FindObjectOfType<Player>().transform;

        // Desativa comportamentos 3D desnecessários
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.updatePosition = false;
    }

    void Update()
    {
        if(!isDead)
        {
            agent.SetDestination(player.position);

            // Move apenas se estiver fora da distância mínima
            if (Vector3.Distance(transform.position, agent.destination) > agent.stoppingDistance)
            {
                Vector3 direction = (agent.steeringTarget - transform.position).normalized;
                transform.position += direction * agent.speed * Time.deltaTime;
            }

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                // Skeleton chegou no limite próximo ao Player
                anim.PlayAnim(2);
            }
            else
            {
                // Skeleton anda em direção ao player
                anim.PlayAnim(1);
            }

            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0) // Mantém o sprite do esqueleto na direção normal (direita)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else // Vira o sprite do esqueleto em 180° (direção contrária, esquerda)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
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
}
