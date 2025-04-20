using UnityEngine;

public class PlotSlot : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite seedCarrot;
    [SerializeField] private Sprite defaultSoil;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotPickedUp;

    [Header("Configurações")]
    [SerializeField] private int digAmount;
    [SerializeField] private int initialDigAmount;
    [SerializeField] private bool detecting;
    [SerializeField] private float plotWaterAmount;

    private float currentWaterAmount;
    private bool wasHoleDug;
    private bool wasCarrotPickedUp;
    private bool isPlayerColliding;

    private PlayerItems playerItems;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
    }

    void Update()
    {
        if (!wasHoleDug) return;

        if (detecting)
        {
            currentWaterAmount += 0.02f;
        }

        if (currentWaterAmount >= plotWaterAmount && !wasCarrotPickedUp)
        {
            spriteRenderer.sprite = seedCarrot;
            audioSource.PlayOneShot(holeSFX);
            wasCarrotPickedUp = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && wasCarrotPickedUp && isPlayerColliding)
        {
            spriteRenderer.sprite = defaultSoil;
            audioSource.PlayOneShot(holeSFX);
            playerItems.seedCarrotTotal++;
            ResetPlot();
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            wasHoleDug = true;
        }
    }

    private void ResetPlot()
    {
        digAmount = initialDigAmount;
        currentWaterAmount = 0f;
        wasCarrotPickedUp = false;
        wasHoleDug = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig")) OnHit();
        if (collision.CompareTag("Watering")) detecting = true;
        if (collision.CompareTag("Player")) isPlayerColliding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Watering")) detecting = false;
        if (collision.CompareTag("Player")) isPlayerColliding = false;
    }
}
