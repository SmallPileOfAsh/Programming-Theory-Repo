using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private int thirstStart;
    [SerializeField] private int thirstDecline;
    [SerializeField] private int thirstMax;
    [SerializeField] private int maxGrowthPhase;
    [SerializeField] string plantName;

    [SerializeField] GameObject plantPanel;
    PlantPanelUI plantPanelScript;
    private Animator plantAnimator;

    private int currentThirst;
    private int growthPhase;
    private bool isThirsty;

    // Start is called before the first frame update
    void Start()
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
    void Grow()
    {
        if (growthPhase < maxGrowthPhase)
        {
            growthPhase += 1;
            plantAnimator.SetInteger("growStage", growthPhase);
        }
    }
}