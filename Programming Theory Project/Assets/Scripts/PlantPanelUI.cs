using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPanelUI : MonoBehaviour
{
    [SerializeField] public Text plantStatusText;
    [SerializeField] public GameObject waterButton;
    [SerializeField] public Text plantTypeText;

    public GameObject focus;
    public Plant focusScript;


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
    }

    public void WaterFocusPlant()
    {
        focusScript.Water();
    }
}
