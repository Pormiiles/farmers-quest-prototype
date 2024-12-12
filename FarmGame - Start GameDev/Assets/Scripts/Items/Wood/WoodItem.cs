using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodItem : MonoBehaviour
{
    [SerializeField] private float woodSpeed; // Velocidade de movimento da madeira
    [SerializeField] private float timeMove;  // Tempo de movimento lateral antes de parar

    private float timeCount;                  // Contador para o tempo
    private Vector2 moveDirection;           // Direção do movimento

    // Atualiza a posição da madeira ao longo do tempo
    private void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < timeMove)
        {
            transform.Translate(moveDirection * woodSpeed * Time.deltaTime);
        }
    }

    // Define a direção de movimento ao ser instanciado
    public void SetMoveDirection(Vector2 direction)
    {
        moveDirection = direction.normalized; // Normaliza a direção para manter consistência na velocidade
    }

    // Detecta colisão com o Player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().TotalWood++;
            Destroy(gameObject);
        }
    }
}
