using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] protected int thirstStart;
    [SerializeField] protected int thirstDecline;
    [SerializeField] protected int thirstMax;
    [SerializeField] protected int maxGrowthStage;
    [SerializeField] protected string plantName;

    [SerializeField] GameObject plantPanel;
    protected PlantPanelUI plantPanelScript;
    protected Animator plantAnimator;

    protected int currentThirst;
    protected int growthStage;
    protected bool isThirsty;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Setup();

        InvokeRepeating("ThirstCountdown", thirstStart, thirstDecline);
        
    }

    private void OnMouseDown()
    {
        FocusThisPlant();
    }

    void Setup()
    {
        plantAnimator = GetComponent<Animator>();
        currentThirst = thirstMax;
        plantPanelScript = plantPanel.GetComponent<PlantPanelUI>();
    }

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

    public void SendPlantInfo()
    {
        plantPanelScript.ShowPlantInfo(plantName, isThirsty);
    }

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
    protected virtual void Grow()
    {
        if (growthStage < maxGrowthStage)
        {
            growthStage += 1;
            plantAnimator.SetInteger("growthStage", growthStage);
        }
    }
}