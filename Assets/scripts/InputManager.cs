using System.Collections;
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
        GameObject[] menuList = GameObject.FindGameObjectsWithTag("menu");
        //Debug.Log("click");
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit) && (menuList == null || menuList.Length == 0))
        {
            if (hit.collider != null)
            {
                //Debug.Log("hit collider");
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag == "mashTun")
                {
                    GameObject mashTunObject = hit.collider.gameObject;
                    SelectMashTun(mashTunObject);
                }
            }
        }
        
    }

    void SelectMashTun(GameObject mashTunObject)
    {
        Debug.Log("Selected Mash Tun");
        mashTunObject.GetComponent<MashTun>().OnSelectThisTun();
    }

    private void HandleInput1()
    {

    }
}
