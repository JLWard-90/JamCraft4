using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public List<Malt> availableMalts;
    public List<Hops> availableHops;
    public List<Yeast> availableYeasts;
    public Inventory()
    {
        availableMalts = new List<Malt>();
        availableHops = new List<Hops>();
        availableYeasts = new List<Yeast>();
    }
}
