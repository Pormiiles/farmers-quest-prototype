using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image waterBarUI;
    [SerializeField] private Image woodBarUI;
    [SerializeField] private Image carrotBarUI;

    [SerializeField] private List<Image> toolsUI = new List<Image>();
    [SerializeField] private Color selectedToolColor;
    [SerializeField] private Color uncheckedToolColor;

    private Player player;
    private PlayerItems playerItems;

    private void Awake()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        player = playerItems.GetComponent<Player>(); // usando a vai�vel playerItems para obter o componente Player 
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
        // Atualiza as barras de quantidade na UI de itens colet�veis
        waterBarUI.fillAmount = playerItems.waterTotal / playerItems.WaterLimit1;
        carrotBarUI.fillAmount = playerItems.seedCarrotTotal / playerItems.SeedCarrotsLimit;
        woodBarUI.fillAmount = playerItems.woodTotal / playerItems.WoodLimit;

        // Atualiza o valor Alpha da ferramenta que foi selecionado ou n�o na UI do invent�rio
        for (int i = 0; i < toolsUI.Count; i++)
        {
            if(i == player.HandlingTool - 1)
            {
                toolsUI[i].color = selectedToolColor;
            } else
            {
                toolsUI[i].color = uncheckedToolColor;
            }
        }
    }
}
