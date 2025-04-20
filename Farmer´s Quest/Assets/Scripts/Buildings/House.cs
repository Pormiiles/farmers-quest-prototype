using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    // Estados internos
    private bool isPlayerDetected;
    private bool isBuilding;

    [Header("Configurações")]
    [SerializeField] private float buildTime = 3f;
    [SerializeField] private int woodRequired = 10;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Posicionamento")]
    [SerializeField] private GameObject houseSprite;
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private Transform playerBuildPoint;

    [Header("Referências ao Player")]
    private Player player;
    private PlayerAnimation playerAnim;
    private PlayerItems playerItems;

    [Header("Outros Objetos")]
    public GameObject farmGatesObject;
    public GameObject keyAdvice;

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

        // Posiciona o jogador e inicia a animação de construção
        player.transform.position = playerBuildPoint.position;
        player.IsPlayerSpeedPaused = true;
        playerAnim.onBuildingStart();
        houseSprite.SetActive(true);
        houseSprite.GetComponent<SpriteRenderer>().color = startColor;
        playerItems.woodTotal -= woodRequired;

        // Aguarda o tempo de construção
        yield return new WaitForSeconds(buildTime);

        // Finaliza construção e atualiza o jogo
        houseSprite.GetComponent<SpriteRenderer>().color = endColor;
        playerAnim.onBuildingEnd();
        player.IsPlayerSpeedPaused = false;
        houseCollider.SetActive(true);

        GameManager.instance.estadoLayla = EstadoLayla.DepoisDeConstruirCasa;
        Debug.Log("Casa da Layla construída!");

        openFarmGates();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = true;

            if (GameManager.instance.estadoLayla == EstadoLayla.DepoisDaCaverna &&
                playerItems.woodTotal >= woodRequired)
            {
                keyAdvice.SetActive(true); // Exibe dica para pressionar "E"
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = false;
            keyAdvice.SetActive(false);
        }
    }

    public void openFarmGates()
    {
        farmGatesObject.SetActive(false);
    }
}
