using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettle : MonoBehaviour
{
    public bool boiling = false;
    public bool empty = true;
    public bool readyToTransfer = false;
    public Recipe recipe;
    int boilTime = 4; //Time for boil in timesteps
    int currentBoilTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTimestepForward()
    {
        if (boiling)
        {
            currentBoilTime++;
            if (currentBoilTime > boilTime)
            {
                boiling = false;
                empty = false;
                readyToTransfer = true;
            }
        }
    }

    public void OnWortTransferIn(Recipe recipe)
    {
        this.recipe = recipe;
        boiling = true;
        empty = false;
    }




}
