using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : Plant
{
    public GameObject fruitModel;
    public GameObject flowerModel;

    void Flower()
    {
        Debug.Log(gameObject.name + " grew a fruit!");
    }

    void Fruit()
    {
        Debug.Log(gameObject.name + " grew a fruit!");
    }
}
