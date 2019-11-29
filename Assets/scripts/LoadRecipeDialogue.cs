using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRecipeDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnExitButtonPress()
    {
        GameObject.Destroy(this.gameObject);
    }
}
