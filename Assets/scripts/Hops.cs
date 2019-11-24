using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hops
{
    public string name;
    public string type; //type of hops either bittering, aroma, or multi-purpose
    public string[] flavours; //flavours like melon, citrussy, etc.
    public float alphaacids; //international bitterness unitys
    public int quality; //between 0 and 10
    public int price; //price per unit
    public Hops(string name, string type, string[] flavours, float alphaacids, int quality, int price) //Constructor for Hops class
    {
        this.name = name;
        this.type = type;
        this.flavours = flavours;
        this.alphaacids = alphaacids;
        this.quality = quality;
        this.price = price;
    }
}
