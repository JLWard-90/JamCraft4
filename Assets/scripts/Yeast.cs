using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeast
{
    public string name;
    public string type; //Lager, Ale, or Kveik
    public string[] flavours; //neutral, banana, esters, etc.
    public float minGravity; //The minimum gravity that this yeast will reach
    public float speed; //gravity drop per day
    public Vector2 temperatureRange;
    public int quality;
    public int price;
    public Yeast(string name, string type, string[] flavours, float minGravity, float speed, Vector2 temperatureRange, int quality, int price)
    {
        this.name = name;
        this.type = type;
        this.flavours = flavours;
        this.minGravity = minGravity;
        this.speed = speed;
        this.temperatureRange = temperatureRange;
        this.quality = quality;
        this.price = price;
    }
}
