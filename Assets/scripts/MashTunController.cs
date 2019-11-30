using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashTunController : MonoBehaviour
{
    Recipe recipe;
    MashTun mashTun;
    CompanyController companyController;
    // Start is called before the first frame update
    void Start()
    {
        mashTun = GameObject.Find("MashTun").GetComponent<MashTun>();
        companyController = GameObject.Find("CompanyController").GetComponent<CompanyController>();
        SetupDropDown();
        SetStatusText();
    }

    public void SetStatusText()
    {
        Text statusText = GameObject.Find("StatusText").GetComponent<Text>();
        string statusString = "";
        if (mashTun.mashing == true)
        {
            statusString = "mashing...";
        }
        else if(mashTun.readyToTransfer == true)
        {
            statusString = "Done!\n ready to transfer to Kettle!";
        }
        else
        {
            statusString = "Idle";
        }
        statusText.text = "Status: " + statusString;
    }

    public void SetRecipe()
    {
        if (companyController.recipes.Count > 0)
        {
            recipe = companyController.recipes[GameObject.Find("RecipeDropdown").GetComponent<Dropdown>().value];
            mashTun.recipe = recipe;
            UpdateRecipeText(recipe);
        }
    }

    public void UpdateRecipeText(Recipe recipe)
    {
        Text recipeText = GameObject.Find("RecipeInfo").GetComponent<Text>();
        recipeText.text = string.Format("Recipe: {0}\nStarting Gravity: {1}\nFinal Gravity: {2}\nABV: {3}\nColour: {4}\nBitterness: {5}", recipe.name, recipe.startingGravity.ToString("F3"),recipe.finalGravity.ToString("F3"), recipe.alcoholByVolume.ToString("F1"), (int)recipe.colour, (int)recipe.iBUs);
    }

    public void SetupDropDown()
    {
        Dropdown recipeDropDown = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>();
        List<Recipe> availableRecipes = companyController.recipes;
        recipeDropDown.options.Clear();
        if (availableRecipes != null && availableRecipes.Count > 0)
        {
            for (int i=0; i<availableRecipes.Count;i++)
            {
                recipeDropDown.options.Add(new Dropdown.OptionData() { text = availableRecipes[i].name });
            }
            recipeDropDown.RefreshShownValue();
        }
        SetRecipe();
    }

    public void OnStartButton()
    {
        if (recipe != null && !mashTun.mashing)
        {
            mashTun.recipe = recipe;
            mashTun.mashing = true;
            UpdateRecipeText(recipe);
            SetStatusText();
        }
    }

    public void OnCloseButton()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnTransferButton()
    {
        mashTun.OnTransferToKettle();
        GameObject.Destroy(this.gameObject);
    }
}
