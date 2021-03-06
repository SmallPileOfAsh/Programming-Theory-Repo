using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : Plant //INHERITANCE
{
    [SerializeField] private GameObject fruitModel;
    [SerializeField] private GameObject flowerModel;

    [SerializeField] private Material myMaterial;

    private Animator flowerAnimator;
    private Animator fruitAnimator;
    private Color startColor;

    private int flowerStage = 0;
    private int maxFlowerStage = 1;
    private int fruitStage = 0;
    private int maxFruitStage = 3;

    private bool isFlowering;
    private bool isFruiting;
    public bool isHarvestable {get; private set;}

    //Add Pepper Specific Setup
    protected override void Setup()
    {
        base.Setup();
        flowerModel = GameObject.Find("Flower");
        startColor = myMaterial.color;
        flowerAnimator = flowerModel.GetComponent<Animator>();
        fruitAnimator = fruitModel.GetComponent<Animator>();
        flowerModel.SetActive(false);
        fruitModel.SetActive(false);
    }

    //POLYMORPHISM
    //Add flowering and fruiting to grow process
    protected override void Grow()
    {
        if (growthStage == maxGrowthStage && !isFlowering && !isFruiting)
        {
            StartFlowering();
        }
        else if (growthStage == maxGrowthStage && flowerStage == maxFlowerStage && !isFruiting)
        {
            StartFruiting();
        }
        else if (isFlowering)
        {
            GrowFlower();
        }
        else if (isFruiting)
        {
            GrowFruit();
        }
        else
        {
            base.Grow();
        } 
    }

    //Start flowering
    void StartFlowering()
    {
        flowerModel.SetActive(true);
        isFlowering = true;

        flowerStage = 0;
        flowerAnimator.Play("Grow Stage 0");
    }

    //Start Fruiting
    void StartFruiting()
    {
        isFlowering = false;
        isFruiting = true;

        flowerModel.SetActive(false);
        fruitModel.SetActive(true);

        fruitStage = 0;

        fruitAnimator.SetInteger("growthStage", fruitStage);
        myMaterial.SetColor("_Color", Color.green);
    }

    //Grow Flower after it has been started
    void GrowFlower()
    { 
        if (flowerStage < maxFlowerStage)
        {
            flowerStage++;
            flowerAnimator.SetInteger("growthStage", flowerStage);
        }
    }

    //Grow fruit after it has been started
    void GrowFruit()
    {
        if (fruitStage < maxFruitStage)
        {
            fruitStage++;
            fruitAnimator.SetInteger("growthStage", fruitStage);
        }
        if (fruitStage == maxFruitStage)
        {
            myMaterial.SetColor("_Color", Color.red);
            isHarvestable = true;
        }
        
    }

    //harvest fruit
    public void Harvest()
    {
        if (isHarvestable)
        {
            isHarvestable = false;
            myMaterial.SetColor("_Color", startColor);

            flowerStage = 0;
            fruitStage = 0;

            isFruiting = false;
            
            fruitModel.SetActive(false);
            plantPanelScript.IncreasePepperCount();
        }
    }
}
