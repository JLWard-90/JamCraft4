using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KettleInterface : MonoBehaviour
{
    public Recipe recipe;
    public Kettle thisKettle;
    [SerializeField]
    GameObject SelectFermenterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        UpdateRecipeText();
        UpdateStatusText();
    }

    public void UpdateStatusText()
    {
        string statusString = "";
        if (thisKettle.boiling == true)
        {
            statusString = "Boiling...";
        }
        else if (thisKettle.readyToTransfer == true)
        {
            statusString = "Done!\n Ready for transfer!";
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
            Text recipeText = GameObject.Find("RecipeText").GetComponent<Text>();
            recipeText.text = string.Format("Recipe: {0}\nStarting Gravity: {1}\nFinal Gravity: {2}\nABV: {3}\nColour: {4}\nBitterness: {5}", recipe.name, recipe.startingGravity.ToString("F3"), recipe.finalGravity.ToString("F3"), recipe.alcoholByVolume.ToString("F1"), (int)recipe.colour, (int)recipe.iBUs);
        }
    }


    public void OnCloseButton()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void TransferWortToFermenter(Fermenter fermenter)
    {
        if (fermenter.empty == true)
        {
            Recipe currentRecipe = thisKettle.recipe;
            fermenter.OnTransferWortIn(currentRecipe);
            thisKettle.boiling = false;
            thisKettle.readyToTransfer = false;
            thisKettle.empty = true;
        }
        else
        {
            Debug.Log("Cannot transfer to fermenter: fermenter not empty!");
        }
    }

    public void OnTransferButton()
    {
        if (thisKettle.readyToTransfer == true)
        {
            GameObject[] fermenters = GameObject.FindGameObjectsWithTag("fermenter");
            List<GameObject> availableFermenters = new List<GameObject>();
            foreach(GameObject fermenter in availableFermenters)
            {
                Fermenter fermenterComponent = fermenter.GetComponent<Fermenter>();
                Debug.Log(fermenterComponent.empty);
                if (fermenterComponent.empty == true)
                {
                    availableFermenters.Add(fermenter);
                }
            }
            if (availableFermenters.Count < 1)
            {
                //If there are no available fermenters
                Debug.Log("There are no empty fermenters available");
            }
            else
            {
                SpawnFermenterSelectMenu(availableFermenters);
            }
        }
        else
        {
            Debug.Log("This wort is not ready for transfer yet!");
        }
    }

    void SpawnFermenterSelectMenu(List<GameObject> availableFermenters)
    {
        GameObject selectorMenu = GameObject.Instantiate(SelectFermenterPrefab);
        selectorMenu.name = "SelectFermenterInterface";
        selectorMenu.transform.SetParent(GameObject.Find("Canvas").transform);
        selectorMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        selectorMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        selectorMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        selectorMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        selectorMenu.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        selectorMenu.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        selectorMenu.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        selectorMenu.GetComponent<FermenterSelectMenu>().kettleInterface = this;
        selectorMenu.GetComponent<FermenterSelectMenu>().availableFermenters = availableFermenters;
    }
}
