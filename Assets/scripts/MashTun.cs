using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashTun : MonoBehaviour
{
    public bool mashing = false;
    public bool readyToTransfer = false;
    public Recipe recipe;
    [SerializeField]
    GameObject mashTunInterfacePrefab;
    [SerializeField]
    int mashTime = 4; //Time to mash in timesteps
    int currentMashTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnSelectThisTun()
    {
        GameObject.Instantiate(mashTunInterfacePrefab);
        mashTunInterfacePrefab.transform.SetParent(GameObject.Find("Canvas").transform);
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
        }
    }

    public void OnTransferToKettle()
    {
        GameObject kettle = GameObject.Find("Kettle");

    }
}
