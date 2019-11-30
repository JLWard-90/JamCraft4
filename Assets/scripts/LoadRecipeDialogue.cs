using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRecipeDialogue : MonoBehaviour
{
    CompanyController companyController;
    List<Recipe> savedRecipes;
    RecipePlannerController recipePlannerController;
    MashTunController mashTun;
    public string openerType; //string containing the typeof object that is using the loader [recipe,mash tun,etc.]
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(openerType);
        switch (openerType)
        {
            case "recipe":
                recipePlannerController = this.GetComponentInParent<RecipePlannerController>();
                break;
            case "mash tun":
                mashTun = GameObject.Find("MashTun").GetComponent<MashTunController>();
                break;
        }
        companyController = GameObject.Find("CompanyController").GetComponent<CompanyController>();
        savedRecipes = companyController.recipes;
        SetupDropDownList();
    }

    public LoadRecipeDialogue(string openerType)
    {
        this.openerType = openerType;
    }

    public void OnExitButtonPress()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnLoadRecipeButton()
    {
        switch (openerType)
        {
            case "recipe":
                if (savedRecipes.Count > 0)
                {
                    int recipeIndex = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>().value;
                    recipePlannerController.LoadRecipe(recipeIndex);
                }
                break;
            case "mash tun":
                if (savedRecipes.Count > 0)
                {
                    int recipeIndex = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>().value;
                    mashTun.LoadRecipe(recipeIndex);
                }
                break;
        }
        
        
    }

    void SetupDropDownList()
    {
        savedRecipes = companyController.recipes;
        Dropdown recipeDropdown = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>();
        recipeDropdown.options.Clear();
        for (int i=0; i<savedRecipes.Count;i++)
        {
            recipeDropdown.options.Add(new Dropdown.OptionData() { text = savedRecipes[i].name });
        }
        recipeDropdown.RefreshShownValue();
    }


}
