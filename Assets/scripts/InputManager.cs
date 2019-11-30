﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput0();
        }
        if (Input.GetMouseButton(1))
        {
            HandleInput1();
        }
    }

    private void HandleInput0()
    {
        //Debug.Log("click");
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            if (hit.collider != null)
            {
                //Debug.Log("hit collider");
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "mashTun")
                {
                    SelectMashTun();
                }
            }
        }
        
    }

    void SelectMashTun()
    {
        Debug.Log("Selected Mash Tun");
    }

    private void HandleInput1()
    {

    }
}
