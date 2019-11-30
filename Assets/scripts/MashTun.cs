using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MashTun : MonoBehaviour
{
    public bool mashing = false;
    public bool readyToTransfer = false;
    public Recipe recipe;
    [SerializeField]
    GameObject mashTunInterfacePrefab;
    [SerializeField]
    int mashTime = 4; //Time to mash in timesteps
    [SerializeField]
    int currentMashTime;

    public void OnSelectThisTun()
    {
        GameObject newMashTunInterface = GameObject.Instantiate(mashTunInterfacePrefab);
        newMashTunInterface.name = "MashTunInterface";
        newMashTunInterface.transform.SetParent(GameObject.Find("Canvas").transform);
        newMashTunInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        newMashTunInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        newMashTunInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        newMashTunInterface.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        newMashTunInterface.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        newMashTunInterface.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        newMashTunInterface.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }
    private void Start()
    {
        mashing = false;
        readyToTransfer = false;
    }

    public void OnTimeStepForward()
    {
        if (mashing)
        {
            currentMashTime++;
            if (currentMashTime > mashTime)
            {
                mashing = false;
                readyToTransfer = true;
            }
            GameObject mashTunInterface = GameObject.Find("MashTunInterface");
            if (mashTunInterface != null)
            {
                mashTunInterface.GetComponent<MashTunController>().SetStatusText();
            }
        }
    }

    public void OnStartMash()
    {
        if (!mashing && !readyToTransfer)
        {

        }
        else
        {
            Debug.Log("Mash Tun already full!");
        }
    }

    public void OnTransferToKettle()
    {
        GameObject kettle = GameObject.Find("Kettle");
        kettle.GetComponent<Kettle>().OnWortTransferIn(recipe);
        mashing = false;
        readyToTransfer = false;
    }
}
