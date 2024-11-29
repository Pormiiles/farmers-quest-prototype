using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Player player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerDirection.sqrMagnitude > 0) // Se o player estiver se movimentando
        {
            anim.SetInteger("transition", 1);

            // Verifica a direção horizontal
            if (player.playerDirection.x > 0)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Direita
            }
            else if (player.playerDirection.x < 0)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Esquerda (espelha no eixo X - o sprite é invertido)
            }
        } else
        {
            anim.SetInteger("transition", 0); // Se não estiver andando, ele entra na animação de Idle
        }
    }
}
