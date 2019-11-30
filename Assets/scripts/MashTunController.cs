using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashTunController : MonoBehaviour
{
    [SerializeField]
    GameObject loadRecipePrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadRecipe(int recipeIndex)
    {
        Debug.Log(recipeIndex);
    }

    public void OnLoadButtonPressed()
    {
        GameObject loadRecipeDialogue = GameObject.Instantiate(loadRecipePrefab);
        loadRecipeDialogue.transform.SetParent(GameObject.Find("Canvas").transform);
        loadRecipeDialogue.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        loadRecipeDialogue.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        loadRecipeDialogue.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        loadRecipeDialogue.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        loadRecipeDialogue.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        loadRecipeDialogue.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        loadRecipeDialogue.GetComponent<LoadRecipeDialogue>().openerType = "mash tun";
    }
}
