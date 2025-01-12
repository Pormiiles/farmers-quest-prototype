using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image waterBarUI;
    [SerializeField] private Image woodBarUI;
    [SerializeField] private Image carrotBarUI;

    private PlayerItems playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterBarUI.fillAmount = 0f;
        woodBarUI.fillAmount = 0f;
        carrotBarUI.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        waterBarUI.fillAmount = playerItems.waterTotal / playerItems.WaterLimit1;
        carrotBarUI.fillAmount = playerItems.seedCarrotTotal / playerItems.SeedCarrotsLimit;
        woodBarUI.fillAmount = playerItems.woodTotal / playerItems.WoodLimit;
    }
}
