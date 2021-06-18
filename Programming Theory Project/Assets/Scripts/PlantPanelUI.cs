using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPanelUI : MonoBehaviour
{
    [SerializeField] private Text plantStatusText;
    [SerializeField] private GameObject waterButton;
    [SerializeField] private GameObject pickFruitButton;
    [SerializeField] private Text plantTypeText;
    [SerializeField] private Text pepperCountText;

    public GameObject focus;
    public Plant focusScript;

    private int pepperCount = 0;


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ShowPlantInfo(string plantName, bool isThirsty)
    {
        plantTypeText.text = plantName;
        if (isThirsty)
        {
            plantStatusText.text = "Thirsty";
            waterButton.SetActive(true);
        }
        else
        {
            plantStatusText.text = "Not Thirsty";
            waterButton.SetActive(false);
        }

        if (!focus.CompareTag("Fruit"))
        {
            pickFruitButton.SetActive(false);
        }
        else
        {
            if (focus.GetComponent<Pepper>().isHarvestable)
            {
                pickFruitButton.SetActive(true);
            }
            else
            {
                pickFruitButton.SetActive(false);
            }
            
        }
    }

    public void HarvestFocusPlant()
    {
        focus.GetComponent<Pepper>().Harvest();
    }

    public void WaterFocusPlant()
    {
        focusScript.Water();
    }

    public void IncreasePepperCount()
    {
        pepperCount++;
        pepperCountText.text = pepperCount.ToString();
    }
}
