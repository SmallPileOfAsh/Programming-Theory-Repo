using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private int thirstStart;
    [SerializeField] private int thirstDecline;
    [SerializeField] private int thirstMax;
    [SerializeField] protected int maxGrowthStage;
    [SerializeField] private string plantName;

    [SerializeField] private GameObject plantPanel;
    protected PlantPanelUI plantPanelScript;
    private Animator plantAnimator;

    private int currentThirst;
    protected int growthStage;
    private bool isThirsty;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //ABSTRACTION
        Setup();

        InvokeRepeating("ThirstCountdown", thirstStart, thirstDecline);
        
    }

    private void OnMouseDown()
    {
        //ABSTRACTION
        FocusThisPlant();
    }

    protected virtual void Setup()
    {
        plantAnimator = GetComponent<Animator>();
        currentThirst = thirstMax;
        plantPanelScript = plantPanel.GetComponent<PlantPanelUI>();
    }

    //Have PlantPanel focus this plant and relevant info
    private void FocusThisPlant()
    {
        if (plantPanelScript.focus == gameObject && plantPanel.activeSelf)
        {
            plantPanel.SetActive(false);
        }
        else
        {
            plantPanel.SetActive(true);
            plantPanelScript.focus = gameObject;
            plantPanelScript.focusScript = gameObject.GetComponent<Plant>();
            plantPanelScript.ShowPlantInfo(plantName, isThirsty);
        }
        
    }

    //Send plant information to PlantPanelUI
    public void SendPlantInfo()
    {
        plantPanelScript.ShowPlantInfo(plantName, isThirsty);
    }


    //Water plant and refresh panel display information accordingly
    public void Water()
    {
        if (isThirsty)
        {
            isThirsty = false;
            currentThirst = thirstMax;
            Grow();
            if (plantPanelScript.focus == gameObject)
            {
                SendPlantInfo();
            }
        }
    }

    //Decrease plant thirst
    void ThirstCountdown()
    {
        if (currentThirst > 0)
        {
            currentThirst -= thirstDecline;
        }
        else
        {
            isThirsty = true;
        }
        if (plantPanelScript.focus == gameObject)
        {
            SendPlantInfo();
        }
        
    }

    //Grow Plant
    protected virtual void Grow()
    {
        if (growthStage < maxGrowthStage)
        {
            growthStage += 1;
            plantAnimator.SetInteger("growthStage", growthStage);
        }
    }
}