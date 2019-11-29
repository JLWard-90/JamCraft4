using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRecipeDialogue : MonoBehaviour
{
    CompanyController companyController;
    List<Recipe> savedRecipes;
    RecipePlannerController recipePlannerController;
    // Start is called before the first frame update
    void Start()
    {
        recipePlannerController = this.GetComponentInParent<RecipePlannerController>();
        companyController = GameObject.Find("CompanyController").GetComponent<CompanyController>();
        savedRecipes = companyController.recipes;
        SetupDropDownList();
    }

    public void OnExitButtonPress()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void OnLoadRecipeButton()
    {
        if (savedRecipes.Count > 0)
        {
            int recipeIndex = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>().value;
            recipePlannerController.LoadRecipe(recipeIndex);
        }
        
    }

    void SetupDropDownList()
    {
        Dropdown recipeDropdown = GameObject.Find("RecipeDropdown").GetComponent<Dropdown>();
        recipeDropdown.options.Clear();
        for (int i=0; i<savedRecipes.Count;i++)
        {
            recipeDropdown.options.Add(new Dropdown.OptionData() { text = savedRecipes[i].name });
        }
        recipeDropdown.RefreshShownValue();
    }
}
