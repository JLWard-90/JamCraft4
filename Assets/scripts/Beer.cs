using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Beer
{
    public int nBottles; //The number of bottles of this kind of beer
    public int recipeIndex; //Store the recipe index so that I can check if any of this beer is already in the inventory

}
