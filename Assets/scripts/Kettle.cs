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
    [SerializeField]
    GameObject kettleInterfacePrefab;
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
                GameObject kettleInterfaceObject = GameObject.Find("KettleInterface");
                if (kettleInterfaceObject != null)
                {
                    kettleInterfaceObject.GetComponent<KettleInterface>().UpdateStatusText();
                }
            }
        }
    }

    public void OnWortTransferIn(Recipe recipe)
    {
        this.recipe = recipe;
        boiling = true;
        empty = false;
    }

    public void OnSelectThisKettle()
    {
        GameObject newKettleInterface = GameObject.Instantiate(kettleInterfacePrefab);
        newKettleInterface.name = "KettleInterface";
        newKettleInterface.GetComponent<KettleInterface>().thisKettle = this;
        newKettleInterface.GetComponent<KettleInterface>().recipe = recipe;
        newKettleInterface.transform.SetParent(GameObject.Find("Canvas").transform);
        newKettleInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        newKettleInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        newKettleInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        newKettleInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        newKettleInterface.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newKettleInterface.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        newKettleInterface.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }




}
