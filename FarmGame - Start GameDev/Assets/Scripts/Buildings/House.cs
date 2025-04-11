using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    private bool isPlayerDetected;
    private bool isBuilding;

    [Header("Configurações da Construção")]
    [SerializeField] private float buildTime = 3f;
    [SerializeField] private int woodRequired = 10;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Referências")]
    [SerializeField] private GameObject houseSprite;
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private Transform playerBuildPoint;

    private Player player;
    private PlayerAnimation playerAnim;
    private PlayerItems playerItems;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnimation>();
        playerItems = player.GetComponent<PlayerItems>();
    }

    void Update()
    {
        if (isPlayerDetected && !isBuilding &&
            Input.GetKeyDown(KeyCode.E) &&
            GameManager.instance.estadoLayla == EstadoLayla.DepoisDaCaverna &&
            playerItems.woodTotal >= woodRequired)
        {
            StartCoroutine(BuildHouse());
        }
    }

    IEnumerator BuildHouse()
    {
        isBuilding = true;

        // Preparativos
        player.transform.position = playerBuildPoint.position;
        player.IsPlayerSpeedPaused = true;
        playerAnim.onBuildingStart();
        houseSprite.SetActive(true);
        houseSprite.GetComponent<SpriteRenderer>().color = startColor;
        playerItems.woodTotal -= woodRequired;

        // Tempo de construção
        yield return new WaitForSeconds(buildTime);

        // Finaliza construção
        houseSprite.GetComponent<SpriteRenderer>().color = endColor;
        playerAnim.onBuildingEnd();
        player.IsPlayerSpeedPaused = false;
        houseCollider.SetActive(true);

        // Atualiza estado da Layla
        GameManager.instance.estadoLayla = EstadoLayla.DepoisDeConstruirCasa;
        Debug.Log("Casa da Layla construída!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerDetected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerDetected = false;
    }
}
