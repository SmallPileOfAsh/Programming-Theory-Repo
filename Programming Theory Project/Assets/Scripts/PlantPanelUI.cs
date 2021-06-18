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

    private GameObject myFocus;

    //ENCAPSULATION
    public GameObject focus
    {
        get { return myFocus; }
        set
        {
            if (!value.CompareTag("Plant") && !value.CompareTag("Fruit"))
            {
                Debug.LogError("PlantPanelUI focus must be a plant");
            }
            else
            {
                myFocus = value;
            }
        }
    }

    public Plant focusScript;

    //ENCAPSULATION
    private int pepperCountNumber = 0;
    private int pepperCount
    {
        get {return pepperCountNumber;}
        set
        {
            if (value < 0)
            {
                Debug.LogError("Cannot have negative number of peppers!");
            }
            else
            {
                pepperCountNumber = value;
            }
        }
    }

    //Hide Plant Panel UI
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //Show Relevant Plant Info & Buttons in Panel
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
            if (myFocus.GetComponent<Pepper>().isHarvestable)
            {
                pickFruitButton.SetActive(true);
            }
            else
            {
                pickFruitButton.SetActive(false);
            }
            
        }
    }

    //Harvest Fruit from focus plant
    private void HarvestFocusPlant()
    {
        myFocus.GetComponent<Pepper>().Harvest();
    }

    //Water focus plant
    private void WaterFocusPlant()
    {
        focusScript.Water();
    }

    //Increase pepper count
    public void IncreasePepperCount()
    {
        pepperCount++;
        pepperCountText.text = pepperCount.ToString();
    }
}
