using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KettleInterface : MonoBehaviour
{
    public Recipe recipe;
    public Kettle thisKettle;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRecipeText();
        UpdateStatusText();
    }

    void UpdateStatusText()
    {
        string statusString = "";
        if (thisKettle.boiling == true)
        {
            statusString = "Boiling...";
        }
        else if (thisKettle.readyToTransfer == true)
        {
            statusString = "Done!\n Ready fortransfer!";
        }
        else
        {
            statusString = "Idle";
        }
        Text statusText = GameObject.Find("StatusText").GetComponent<Text>();
        statusText.text = "Status: " + statusString;
    }

    void UpdateRecipeText()
    {
        if (recipe != null)
        {
            Text recipeText = GameObject.Find("RecipeInfo").GetComponent<Text>();
            recipeText.text = string.Format("Recipe: {0}\nStarting Gravity: {1}\nFinal Gravity: {2}\nABV: {3}\nColour: {4}\nBitterness: {5}", recipe.name, recipe.startingGravity.ToString("F3"), recipe.finalGravity.ToString("F3"), recipe.alcoholByVolume.ToString("F1"), (int)recipe.colour, (int)recipe.iBUs);
        }
    }


    public void OnCloseButton()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnTransferButton()
    {

    }
}
