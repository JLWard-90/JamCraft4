using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fermenter : MonoBehaviour
{
    public bool fermenting = false;
    public bool readyToBottle = false;
    public bool empty = true;
    public Recipe recipe;
    TimeController timeController;
    int dayLength;
    float gravityDropPerTimeStep;
    public float currentGravity;
    // Start is called before the first frame update
    private void Start()
    {
        timeController = GameObject.Find("GameController").GetComponent<TimeController>();
        dayLength = timeController.gameTimeDayLength;
    }
    public void OnTransferWortIn(Recipe recipeIn)
    {
        recipe = recipeIn;
        Yeast yeast = GameObject.Find("CompanyController").GetComponent<CompanyController>().inventory.availableYeasts[recipe.yeastIndex];
        gravityDropPerTimeStep = (yeast.speed / (float)dayLength);
        currentGravity = recipe.startingGravity;
        fermenting = true;
    }

    public void OnTimeStepForward()
    {
        if(fermenting)
        {
            currentGravity -= gravityDropPerTimeStep;
            if (currentGravity <= recipe.finalGravity)
            {
                currentGravity = recipe.finalGravity;
                fermenting = false;
                readyToBottle = true;
            }
        }
    }

}
