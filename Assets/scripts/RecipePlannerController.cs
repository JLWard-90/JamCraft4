using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipePlannerController : MonoBehaviour
{
    GameController gameController;
    float[] sliderValues;
    GameObject[] sliders;
    public bool[] toAdjust;
    Recipe currentRecipe;
    Yeast currentYeastToUse;
    Inventory companyInventory;
    CompanyController companyController;
    private void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        companyController = GameObject.Find("CompanyController").GetComponent<CompanyController>();
    }

    private void Start()
    {
        sliders = GameObject.FindGameObjectsWithTag("maltSlider");
        sliderValues = new float[sliders.Length];
        toAdjust = new bool[sliders.Length];
        for (int i=0; i<sliderValues.Length; i++)
        {
            sliderValues[i] = sliders[i].GetComponent<Slider>().value;
            if(float.IsNaN(sliderValues[i]))
            {
                sliders[i].GetComponent<Slider>().value = 0;
                sliderValues[i] = 0;
            }
            toAdjust[i] = true;
        }
        companyInventory = companyController.GetComponent<CompanyController>().inventory;
        //Will also need to get the company inventory. We will then draw all our malt hops and yeast details from there.
        SetupMaltDropDownMenus();
        SetupYeastDropDownMenu();
        currentRecipe = new Recipe();
    }

    public void OnExitButtonPress()
    {
        gameController.GetComponent<TimeController>().paused = false;
        GameObject.Destroy(this.gameObject);
    }

    public void OnSaveButtonPress()
    {
        companyController.recipes.Add(currentRecipe);
    }

    public void onSliderSlide(string sliderName, float sliderValue)
    {
        GameObject dropDownMenu = GetMaltDropDown(sliderName);
        if(dropDownMenu.GetComponent<Dropdown>().value != 0)
        {
            int returnSliderCode = UpdateOtherSliders(GameObject.Find(sliderName), sliderValue);
        }
        else
        {
            sliders[returnMyIndex(sliderName)].GetComponent<Slider>().value = 0;
        }
        UpdateSliderTexts();
        for (int i=0; i<sliders.Length; i++)
        {
            toAdjust[i] = true;
        }
     }

    public void UpdateRecipe()
    {

    }

    public int UpdateOtherSliders(GameObject currentSlider, float sliderValue)
    {
        string sliderName = currentSlider.name;
        int countMalts = 0;
        for (int i = 0; i < sliders.Length; i++)//each (GameObject slider in sliders)
        {
            GameObject slider = sliders[i];
            //Adjust each slider accordingly if the dropdown menu is not none
            Dropdown dropDownMenu = GetMaltDropDown(slider.name).GetComponent<Dropdown>();
            if (dropDownMenu.value != 0) //None is at the index position 0
            {
                countMalts++;
            }
            if (slider.name == sliderName)
            {
                toAdjust[i] = false;
            }
        }
        //Debug.Log(countMalts);
        if (countMalts == 1)
        {
            return 0;
        }
        else if(countMalts == 0)
        {
            return 0;
        }
        else
        {
            Debug.Log("adjusting");
            float totalOther = 0;
            for (int i = 0; i<sliderValues.Length;i++)// (GameObject slider in sliders)
            {
                string thenameOfSlider = sliders[i].name;
                if (thenameOfSlider != sliderName)
                {
                    totalOther += sliderValues[i];
                }
                //Update each slider to it's new value
            }
            //float remainder = 100 - sliderValue;
            float correctionFactor = 100 / (totalOther + sliderValue);
            Debug.Log(correctionFactor);
            for (int i = 0; i < sliderValues.Length; i++)// (GameObject slider in sliders)
            {
                Debug.Log(toAdjust[i]);
                if (toAdjust[i] == true && sliders[i].GetComponent<Slider>().value != 0)
                {
                    toAdjust[i] = false;
                    float newValue = sliders[i].GetComponent<Slider>().value * correctionFactor;
                    sliders[i].GetComponent<Slider>().value = newValue;
                    sliderValues[i] = sliders[i].GetComponent<Slider>().value;
                }
            }

        }

        return 1;
    }

    public void UpdateSliderTexts()
    {
        foreach(GameObject slider in sliders)
        {
            int displayValue = (int)slider.GetComponent<Slider>().value;
            string sliderName = slider.name;
            Text textObject = GetMaltPercentageText(sliderName);
            textObject.text = string.Format("{0}%", displayValue);
        }
    }

    public GameObject GetMaltDropDown(string sliderName)
    {
        GameObject dropDownMenu = GameObject.Find("MaltDD1");
        switch (sliderName)
        {
            case "Maltslider1":
                dropDownMenu = GameObject.Find("MaltDD1");
                break;
            case "Maltslider2":
                dropDownMenu = GameObject.Find("MaltDD2");
                break;
            case "Maltslider3":
                dropDownMenu = GameObject.Find("MaltDD3");
                break;
            case "Maltslider4":
                dropDownMenu = GameObject.Find("MaltDD4");
                break;
            case "Maltslider5":
                dropDownMenu = GameObject.Find("MaltDD5");
                break;
            case "Maltslider6":
                dropDownMenu = GameObject.Find("MaltDD6");
                break;
            case "Maltslider7":
                dropDownMenu = GameObject.Find("MaltDD7");
                break;
        }
        return dropDownMenu;
    }

    public Text GetMaltPercentageText(string sliderName)
    {
        Text maltText = GameObject.Find("MaltsliderText1").GetComponent<Text>();
        switch(sliderName)
        {
            case "Maltslider1":
                maltText = GameObject.Find("MaltsliderText1").GetComponent<Text>();
                break;
            case "Maltslider2":
                maltText = GameObject.Find("MaltsliderText2").GetComponent<Text>();
                break;
            case "Maltslider3":
                maltText = GameObject.Find("MaltsliderText3").GetComponent<Text>();
                break;
            case "Maltslider4":
                maltText = GameObject.Find("MaltsliderText4").GetComponent<Text>();
                break;
            case "Maltslider5":
                maltText = GameObject.Find("MaltsliderText5").GetComponent<Text>();
                break;
            case "Maltslider6":
                maltText = GameObject.Find("MaltsliderText6").GetComponent<Text>();
                break;
            case "Maltslider7":
                maltText = GameObject.Find("MaltsliderText7").GetComponent<Text>();
                break;
        }
        return maltText;
    }

    public int returnMyIndex(string sliderName)
    {
        for (int i=0; i<sliders.Length;i++)
        {
            if(sliders[i].name == sliderName)
            {
                return i;
            }
        }
        return 0;
    }

    public float GetStartingGravity(float batchSize, float grainWeight)
    {
        float gravity = 1.0f;
        gravity += grainWeight * (20 / batchSize) * (0.045f / 5.0f);//Assume that a 20L batch size will have a SG of 1.045 with 5kg of grain
        return gravity;
    }

    void SetupMaltDropDownMenus()
    {
        List<Malt> availableMalts = companyInventory.availableMalts;
        GameObject[] dropDownMenus = GameObject.FindGameObjectsWithTag("maltDropDown");
        foreach(GameObject dropdown in dropDownMenus)
        {
            Dropdown dropDownComponent = dropdown.GetComponent<Dropdown>();
            dropDownComponent.options.Clear();
            dropDownComponent.options.Add(new Dropdown.OptionData() { text = "None" });
            for(int i=0; i<availableMalts.Count; i++)
            {
                dropDownComponent.options.Add(new Dropdown.OptionData() { text = availableMalts[i].name });
            }
            dropDownComponent.RefreshShownValue();
        }
    }

    void SetupYeastDropDownMenu()
    {
        List<Yeast> availableYeasts = companyInventory.availableYeasts;
        GameObject dropDownMenu = GameObject.Find("YeastDropdown");
        Dropdown dropDownComponent = dropDownMenu.GetComponent<Dropdown>();
        dropDownComponent.options.Clear();
        for(int i=0; i<availableYeasts.Count; i++)
        {
            dropDownComponent.options.Add(new Dropdown.OptionData() { text = availableYeasts[i].name });
        }
        dropDownComponent.RefreshShownValue();
        
    }
}
