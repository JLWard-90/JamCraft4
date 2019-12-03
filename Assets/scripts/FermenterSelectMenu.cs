using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FermenterSelectMenu : MonoBehaviour
{
    Text infoText;
    Fermenter fermenter;
    public List<GameObject> availableFermenters;
    public KettleInterface kettleInterface;
    // Start is called before the first frame update
    void Start()
    {
        infoText = GameObject.Find("FermenterInfoText").GetComponent<Text>();
        if (availableFermenters == null || availableFermenters.Count < 1)
        {
            Debug.Log("FermenterSelectMenu Error: no available fermenters parsed");
        }
        else
        {
            SetupFermenterList();
        }
    }

    void SetupFermenterList()
    {
        //In the future I will change this to a viewport with srollbar but for now I'm just going to use a dropdown menu for simplicity
        Dropdown dropDownmenu = GameObject.Find("FermenterSelectDropdown").GetComponent<Dropdown>();
        dropDownmenu.options.Clear();
        foreach (GameObject fermenter in availableFermenters)
        {
            dropDownmenu.options.Add(new Dropdown.OptionData() { text = string.Format("Fermenter {0}",fermenter.GetComponent<Fermenter>().fermenterNumber) });
        }
        dropDownmenu.RefreshShownValue();
    }

    void UpdateInfoText()
    {
        string infoTextString = string.Format(" Fermenter {0} \n Capacity: {1}", fermenter.fermenterNumber, fermenter.capacity);
        infoText.text = infoTextString;
    }

    public void OnTransferButtonPress()
    {
        if (fermenter.empty == true)
        {
            kettleInterface.TransferWortToFermenter(fermenter);
        }
        else
        {
            Debug.Log("Sorry, selected fermenter does not appear to be empty");
        }
    }

    public void OnDropDownMenuChange()
    {
        Dropdown dropDownmenu = GameObject.Find("FermenterSelectDropdown").GetComponent<Dropdown>();
        int fermIndex = dropDownmenu.value;
        fermenter = availableFermenters[fermIndex].GetComponent<Fermenter>();
        UpdateInfoText();
    }

    public void OnCloseButton()
    {
        GameObject.Destroy(this.gameObject);
    }
}
