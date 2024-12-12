using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    [SerializeField] private int treeLife;           // Vida da árvore
    [SerializeField] private Animator anim;         // Referência ao Animator
    [SerializeField] private GameObject woodPrefab; // Prefab da madeira a ser dropada
    [SerializeField] private Transform baseLog;     // Referência ao filho Base Log

    private bool isTreeCut;

    // Método chamado quando o Player atinge a árvore
    public void OnHit()
    {
        treeLife--;
        anim.SetTrigger("cuttingTree");

        if (treeLife <= 0)
        {
            // Usa o centro do Base Log para instanciar o WoodItem
            Vector3 baseLogCenter = GetBaseLogCenter();

            // Instancia a madeira na posição correta
            GameObject wood = Instantiate(
                woodPrefab,
                baseLogCenter, // Agora instanciamos no centro do Base Log
                Quaternion.identity
            );

            // Define a direção de movimento para a madeira
            Vector3 moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, 0f);
            wood.GetComponent<WoodItem>().SetMoveDirection(moveDirection);

            // Ativa a animação da árvore sendo cortada
            anim.SetTrigger("isTreeCut");

            isTreeCut = true;
        }
    }

    // Obtém o centro do Base Log com base no Renderer
    private Vector3 GetBaseLogCenter()
    {
        if (baseLog != null)
        {
            Renderer baseLogRenderer = baseLog.GetComponent<Renderer>();

            if (baseLogRenderer != null)
            {
                return baseLogRenderer.bounds.center; // Centro do sprite do Base Log
            }

            return baseLog.position; // Fallback para a posição do Base Log
        }

        Debug.LogError("Base Log não foi atribuído no inspector!");
        return transform.position; // Caso o Base Log não esteja definido
    }

    // Detecta colisão com o machado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isTreeCut)
        {
            OnHit();
        }
    }
}
