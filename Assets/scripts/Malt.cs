using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malt 
{
    public string name; //Name of malt type
    public int EBC; //Colour of malt type
    public string type; //Type of malt: base, special, roasted, etc.
    public float conv; //Conversion factor from kg to sugar
    public string[] flavours; //Flavours
    public int quality; //A quality rating from 0 (rotten) to 10 (best malt ever)
    public int price;
    public Malt(string name, string type, int EBC, int conv, int quality, string[] flavours, int price)
    {
        this.name = name;
        this.type = type;
        this.EBC = EBC;
        this.conv = conv;
        this.quality = quality;
        this.flavours = flavours;
        this.price = price;
    }
}
