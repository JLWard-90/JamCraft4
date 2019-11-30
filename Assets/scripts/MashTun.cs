using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashTun : MonoBehaviour
{
    public bool mashing = true;
    Inventory inventory;
    Yeast yeast;
    Recipe recipe;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("CompanyControler").GetComponent<CompanyController>().inventory;

    }



    public void OnTimeStepForward()
    {

    }
}
