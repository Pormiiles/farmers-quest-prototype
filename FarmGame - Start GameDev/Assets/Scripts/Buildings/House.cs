using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private bool isPlayerDetected;
    private bool hasBegunBuilding;
    private float timeCount;

    [Header("Amounts")]
    [SerializeField] private float timeAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private int woodAmount;

    [Header("Components")]
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private Transform pointPosition;

    private Player player;
    private PlayerAnimation playerAnim;
    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnimation>();
        playerItems = player.GetComponent<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerDetected && Input.GetKeyDown(KeyCode.E) && playerItems.woodTotal >= woodAmount)
        {
            // Começa a animação de construir a casa
            hasBegunBuilding = true;
            houseSprite.color = startColor;
            playerAnim.onBuildingStart();
            player.transform.position = pointPosition.position;
            player.IsPlayerSpeedPaused = true;
            houseSprite.gameObject.SetActive(true);
            playerItems.woodTotal -= woodAmount;
        }

        if(hasBegunBuilding)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                // Finaliza a construção da casa
                houseSprite.color = endColor;
                playerAnim.onBuildingEnd();
                player.IsPlayerSpeedPaused = false;
                houseCollider.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }
}
