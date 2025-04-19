using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float npcSpeed;
    public float initialNPCSpeed;
    private int index;
    public List<Transform> paths = new List<Transform>();
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        initialNPCSpeed = npcSpeed; // Armazena a velocidade atual do jogador
    }

    // Update is called once per frame
    void Update()
    {
        // Se o Player interagir com o NPC, ele para completamente e inicia o diálogo
        if(DialogueController.instance.IsWindowBeingShowed)
        {
            npcSpeed = 0f; // Velocidade do NPC vai pra 0 e ele para
            anim.SetBool("isWalking", false); // Seta a animação de "walk" como false, o NPC ficará no estado de "idle"

        } else // Volta a caminhar normalmente
        {
            npcSpeed = initialNPCSpeed; // NPC recebe o antigo valor de sua velocidade inicial
            anim.SetBool("isWalking", true); // NPC volta a andar
        }

        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, npcSpeed * Time.deltaTime); // Faz com que o NPC siga na direção do componente "path"

        if(Vector2.Distance(transform.position, paths[index].position) < 0.1f) // Verifica se o NPC chegou próximo do ponto "path"
        {
            if(index < paths.Count - 1)
            {
                // index++; // Incrementa o valor do próximo ponto para o qual o NPC vai andar

                index = Random.Range(0, paths.Count); // Sorteia um índice de um ponto aleatório para o NPC caminhar na direção
            } else
            {
                index = 0; // Ao chegar no último ponto (paths.Count - 1), o NPC volta ao ponto inicial (índice 0)
            }
        }

        // Lógica para virar o sprite do NPC na direção em que ele está olhando
        Vector2 direction = paths[index].position - transform.position;

        if(direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        if(direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180); // Vira o eixo Y em 180 na direção esquerda (< 0)
        }
    }
}
