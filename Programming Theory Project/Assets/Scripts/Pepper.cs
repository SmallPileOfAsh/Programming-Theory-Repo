using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : Plant
{
    void Fruit()
    {
        Debug.Log(gameObject.name + " grew a fruit!");
    }
}
