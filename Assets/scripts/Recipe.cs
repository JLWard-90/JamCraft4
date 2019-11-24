using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recipe
{
    public int batchsize; //size of the batch
    public string yeast; //yeast used
    public int yeastIndex; //index of yeast used
    public int[] maltIndeces; //Index of each malt being used
    public string[] malts; //names of the malts used
    public float[] maltQuantities; //quanities of all malts used
    public string[] hops; //Hops used
    public int[] hopIndeces; //Index of each hop used
    public int[] hopTimes; //Times for all hop additions in minutes
    public float[] hopAmounts; //Amounts for each hop variety
    public float[] hopIBUs; //IBU of each hop addition
    public float colour; //EBC converted to a simple 0 to 100 scale
    public float iBUs; //International Bitterness Units
    public string name; //Name of the recipe
    public string[] flavours; //Just an array of all of the flavours //Used to calculate tastiness
    public string[] aromas; //An array of all of the aromas //Used to calculate tastiness
    public float gravityIBURatio; //ratio of gravity to IBUs //Used to calculate tastiness
    public float tastiness; //A tastiness score based on appropriate percentages of 
    public float quality; //Quality will equal average malt quality + average hop quality + yeast quality for a range of 0 to 30 //Used to calculate tastiness
    public int cost; // The cost to brew the recipe
    public int mouthFeel; //A scale from 0 to 100 where 100 is very full bodied and 0 is very dry. //Used to calculate tastiness
    public float startingGravity;//SG
    public float finalGravity; //FG
    public float alcoholByVolume; //ABV
    public Recipe()
    {
        name = "Untitled Recipe";
        yeast = "Basic Ale Yeast";
        yeastIndex = 0;
        maltIndeces = new int[] { 0 };
        malts = new string[] { "Pale Ale Malt" };
        maltQuantities = new float[] { 5};
        hops = new string[] { "Fuggles" };
        hopIndeces = new int[] { 0 };
        hopTimes = new int[] { 30 };
        hopAmounts = new float[] { 30 };
        hopIBUs = new float[] { 20 };
        colour = 2;
        iBUs = 20;
        flavours = new string[] { "Beer" };
        aromas = new string[] { "Beer" };
        gravityIBURatio = 1;
        quality = 5;
        cost = 5;
        mouthFeel = 50;
        startingGravity = 1.045f;
        finalGravity = 1.012f;
        alcoholByVolume = 4.5f;
        tastiness = 10;
    }
    public Recipe(string name, string yeast, int yeastIndex, int[] maltIndeces, string[] malts, float[] maltQuantities, string[] hops, int[] hopIndeces, int[] hopTimes, 
        float[] hopAmounts, float[] hopIBUs, float colour, float iBUs, string[] flavours, string[] aromas, float gravityIBURatio, float quality, int cost,int mouthFeel, float startingGravity,
        float finalGravity, float alcoholByVolume)
    {
        this.name = name;
        this.yeast = yeast;
        this.yeastIndex = yeastIndex;
        this.maltIndeces = maltIndeces;
        this.malts = malts;
        this.maltQuantities = maltQuantities;
        this.hops = hops;
        this.hopIndeces = hopIndeces;
        this.hopTimes = hopTimes;
        this.hopAmounts = hopAmounts;
        this.hopIBUs = hopIBUs;
        this.colour = colour;
        this.iBUs = iBUs;
        this.flavours = flavours;
        this.aromas = aromas;
        this.gravityIBURatio = gravityIBURatio;
        this.quality = quality;
        this.cost = cost;
        this.mouthFeel = mouthFeel;
        this.startingGravity = startingGravity;
        this.finalGravity = finalGravity;
        this.alcoholByVolume = alcoholByVolume;
        int gravRatgood;
        if (gravityIBURatio > 0.8 && gravityIBURatio < 1.2)
        {
            gravRatgood = 10;
        }
        else if (gravityIBURatio < 0.6 || gravityIBURatio > 1.4)
        {
            gravRatgood = -20;
        }
        else
        {
            gravRatgood = 0;
        }
        this.tastiness = quality-15 + ((mouthFeel-50)*(colour-50))/100  + gravRatgood + (aromas.Length * 2) + (flavours.Length * 3); //This will probably need some tweaks in the future...
    }
}
