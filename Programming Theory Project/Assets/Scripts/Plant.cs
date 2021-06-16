using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] private int thirstStart;
    [SerializeField] private int thirstDecline;
    [SerializeField] private int thirstMax;
    private int currentThirst;
    private int growthPhase;
    [SerializeField] private int maxGrowthPhase;

    [SerializeField] string plantName;
    [SerializeField] GameObject waterButton;
    [SerializeField] Text plantTypeText;
    public Text plantStatusText;

    private bool isThirsty;
    private Animator plantAnimator;

    // Start is called before the first frame update
    void Start()
    {
        plantAnimator = GetComponent<Animator>();
        currentThirst = thirstMax;
        InvokeRepeating("ThirstCountdown", thirstStart, thirstDecline);

        plantTypeText.text = plantName;
        UpdateThirstMessage();
    }

    private void OnMouseDown()
    {
        Water();
    }

    public void Water()
    {
        if (isThirsty)
        {
            isThirsty = false;
            currentThirst = thirstMax;
            UpdateThirstMessage();
            Grow();
            waterButton.SetActive(false);
        }
    }

    void UpdateThirstMessage()
    {
        if (currentThirst > 0)
        {
            plantStatusText.text = "Status: Happy";
        }
        else
        {
            plantStatusText.text = "Status: Thirsty";
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
            waterButton.SetActive(true);
        }
        UpdateThirstMessage();
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