using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite seedCarrot;
    [SerializeField] private Sprite defaultSoil;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotPickedUp;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private int initialDigAmount;
    [SerializeField] private bool detecting;
    [SerializeField] private float plotWaterAmount;
    private float currentWaterAmount;
    private bool wasHoleDug;
    private bool wasCarrotPickedUp;
    private bool isPlayerColliding;

    private PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;   
    }

    private void Update()
    {
        if(wasHoleDug)
        {
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
                currentWaterAmount = 0f;
                wasCarrotPickedUp = false;

                // Reseta as condições para o Player poder reiniciar o ciclo
                digAmount = initialDigAmount;
                currentWaterAmount = 0f;
                wasCarrotPickedUp = false;
                wasHoleDug = false;
            }
        }
        
    }

    public void OnHit()
    {
        digAmount--;

        if(digAmount <= initialDigAmount/2)
        {
            spriteRenderer.sprite = hole;
            wasHoleDug = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if(collision.CompareTag("Watering"))
        {
            detecting = true;
        }

        if(collision.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Watering"))
        {
            detecting = false;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }
}
