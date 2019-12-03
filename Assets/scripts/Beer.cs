using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Beer
{
    //Inherited from the recipe:
    public int recipeIndex; //Store the recipe index so that I can check if any of this beer is already in the inventory
    public float tastiness;
    public float quality;
    public string name;
    public float ABV;
    public float mouthfeel;
    //Based on recipe but calculated by the fermenter:
    public int nBottles; //The number of bottles of this kind of beer
    //Set in the controller:
    public float pricePerBottle;

    public Beer(string name, int nBottles, int recipeIndex, float tastiness, float quality, float pricePerBottle, float ABV, float mouthfeel)
    {
        this.name = name;
        this.nBottles = nBottles;
        this.recipeIndex = recipeIndex;
        this.tastiness = tastiness;
        this.quality = quality;
        this.pricePerBottle = pricePerBottle;
        this.ABV = ABV;
        this.mouthfeel = mouthfeel;
    }

    public Beer()
    {
        Debug.Log("Blank constructor used to initialise Beer Class. Using placeholder values");
        this.name = "Beer with no name";
        this.nBottles = 0;
        this.recipeIndex = -1;
        this.tastiness = 0;
        this.quality = 0;
        this.pricePerBottle = 0;
        this.ABV = 0;
        this.mouthfeel = 0;
    }

    public void ChangePricePerBottle(float newPrice)
    {
        pricePerBottle = newPrice;
    }

    public void AddMoreInventory(int additionalBottles)
    {
        nBottles += additionalBottles;
    }

    public float BulkSellInventory(int bottlesToSell, float pricePerBottle)
    {
        float price = 0;
        if (nBottles >= bottlesToSell)
        {
            nBottles -= bottlesToSell;
            price = pricePerBottle * nBottles;
        }
        return price;
    }
}
