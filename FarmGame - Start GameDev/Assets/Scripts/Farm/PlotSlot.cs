using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotSlot : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite seedCarrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private int initialDigAmount;
    [SerializeField] private bool detecting;
    [SerializeField] private float plotWaterAmount;
    private float currentWaterAmount;
    private bool wasHoleDug;

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

            if (currentWaterAmount >= plotWaterAmount)
            {
                spriteRenderer.sprite = seedCarrot;

                if(Input.GetKeyDown(KeyCode.E))
                {
                    spriteRenderer.sprite = hole;
                    playerItems.seedCarrotTotal++;
                    currentWaterAmount = 0f;
                }
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Watering"))
        {
            detecting = false;
        }
    }
}
