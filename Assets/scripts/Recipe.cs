using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Recipe
{
    public int batchsize; //size of the batch
    public string yeast; //yeast used
    public int yeastIndex; //index of yeast used
    public List<int> maltIndeces; //Index of each malt being used
    public List<string> malts; //names of the malts used
    public List<float> maltQuantities; //quanities of all malts used
    public List<string> hops; //Hops used
    public List<int> hopIndeces; //Index of each hop used
    public List<int> hopTimes; //Times for all hop additions in minutes
    public List<float> hopAmounts; //Amounts for each hop variety
    public List<float> hopIBUs; //IBU of each hop addition
    public float colour; //EBC converted to a simple 0 to 100 scale
    public float iBUs; //International Bitterness Units
    public string name; //Name of the recipe
    public List<string> flavours; //Just an array of all of the flavours //Used to calculate tastiness
    public List<string> aromas; //An array of all of the aromas //Used to calculate tastiness
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
        maltIndeces = new List<int>();
        malts = new List<string>();
        maltQuantities = new List<float>();
        hops = new List<string>();
        hopIndeces = new List<int>();
        hopTimes = new List<int>();
        hopAmounts = new List<float>();
        hopIBUs = new List<float>();
        colour = 0;
        iBUs = 0;
        flavours = new List<string>();
        aromas = new List<string>();
        gravityIBURatio = 1;
        quality = 5;
        cost = 5;
        mouthFeel = 50;
        startingGravity = 1.045f;
        finalGravity = 1.012f;
        alcoholByVolume = 4.5f;
        tastiness = 10;
    }
    public Recipe(string name, string yeast, int yeastIndex, List<int> maltIndeces, List<string> malts, List<float> maltQuantities, List<string> hops, List<int> hopIndeces, List<int> hopTimes,
        List<float> hopAmounts, List<float> hopIBUs, float colour, float iBUs, List<string> flavours, List<string> aromas, float gravityIBURatio, float quality, int cost,int mouthFeel, float startingGravity,
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
        this.tastiness = quality-15 + ((mouthFeel-50)*(colour-50))/100  + gravRatgood + (aromas.Count * 2) + (flavours.Count * 3); //This will probably need some tweaks in the future...
    }
}
