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
        SetupHopAdditionDropDownMenu();
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

    public void OnHopSliderSlide(Slider hopSlider)
    {
        //Should just need to update  hop quantity text here
        Text hopQuantityText = GameObject.Find("HopAmountText").GetComponent<Text>();
        hopQuantityText.text = string.Format("{0} g",(int)hopSlider.value);
    }

    public void UpdateRecipe()
    {
        float waterVolume = waterVolumeSwitch(GameObject.Find("VolumeDropdown").GetComponent<Dropdown>().value);
        float grainWeight = GameObject.Find("WeightSlider").GetComponent<Slider>().value;
        List<float> grainQuantities = new List<float>();
        List<int> grainIndeces = new List<int>();
        List<string> grainNames = new List<string>();
        for (int i=0;i<sliders.Length;i++)
        {
            int grainIndex = GetMaltDropDown(sliders[i].name).GetComponent<Dropdown>().value;
            if(grainIndex > 0) //If the type of grain is not none
            {
                grainIndeces.Add(grainIndex);
                grainQuantities.Add(sliders[i].GetComponent<Slider>().value / 100f); //Divide by 100 to turn percentage into fraction
                grainNames.Add(companyInventory.availableMalts[i].name); //get malt name from the malt held in the availableMalts list
            }
        }
    }

    public float CalculateColour(float volume, List<float> grainEBCs, List<float> grainMasses)
    {
        //EBC = 1.97 SRM
        //SRM color = 1.4922 * (MCU ** 0.6859)
        //MCU = (Weight of grain in lbs) * (Color of grain in degrees lovibond) / (volume in gallons)
        //SRM is approximately equal to degrees lovibond
        // 1 US Gallon == 3.78541 L
        //1 lbs = 0.454 kg
        float ebc = 0;
        if (grainEBCs.Count == 0 || grainEBCs ==null || grainMasses ==null)
        {
            return ebc;
        }
        for (int i=0; i<grainEBCs.Count; i++)
        {
            float MCU = (grainMasses / 0.454f) * (grainEBCs[i] / 1.97) / (volume / 3.78541f);
            ebc += 1.97f*(1.4922f * (Mathf.pow(MCU,0.6859f)));
        }
        return ebc;
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

    public void OnWeightSliderSlide(Slider thisSlider)
    {
        //Should just need to update  hop quantity text here
        Text weightText = GameObject.Find("WeightText").GetComponent<Text>();
        weightText.text = string.Format("{0} kg", thisSlider.value.ToString("F2"));
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

    void SetupHopAdditionDropDownMenu()
    {
        List<Hops> availableHops = companyInventory.availableHops;
        GameObject dropDownMenu = GameObject.Find("HopTypeDropDown1");
        Dropdown dropDownComponent = dropDownMenu.GetComponent<Dropdown>();
        dropDownComponent.options.Clear();
        for(int i=0; i<availableHops.Count; i++)
        {
            dropDownComponent.options.Add(new Dropdown.OptionData() { text = availableHops[i].name });
        }
        dropDownComponent.RefreshShownValue();
    }

    public void OnAddHopAdditionButton()
    {
        Dropdown hopTypedropdown = GameObject.Find("HopTypeDropDown1").GetComponent<Dropdown>();//first get the hop type index from the drop down menu
        int hopIndex = hopTypedropdown.value;
        //Then get the time from the drop down menu
        Dropdown timeDropDown = GameObject.Find("HopTimeDropDown").GetComponent<Dropdown>();
        float hopTime = timeDropDown.value * 5.0f; //Because the indeces all increase the time by 5
        //Get the batch size from the drop down menu
        Dropdown sizeDropDown = GameObject.Find("VolumeDropdown").GetComponent<Dropdown>();
        float waterVolume = waterVolumeSwitch(sizeDropDown.value);
        //Get the starting gravity from the recipe
        float startingGravity = currentRecipe.startingGravity;
        //Get the quantity of hops from the slider
        Slider hopSlider = GameObject.Find("hopSlider").GetComponent<Slider>();
        float hopQuantity = hopSlider.value;
        //Then calculate IBUs using CalculateIBUs()
        float hopAdditionIBUs = CalculateIBUs(hopIndex, hopQuantity, waterVolume, hopTime, startingGravity);
        string hopName = companyInventory.availableHops[hopIndex].name;
        //Now add the hop addition to the recipe and the display
        string listString = string.Format("{0} - {1} g - {2} min - {3} IBUs\n", hopName, (int)hopQuantity, (int)hopTime, (int)hopAdditionIBUs);
        currentRecipe.hops.Add(hopName);
        currentRecipe.hopIndeces.Add(hopIndex);
        currentRecipe.hopTimes.Add((int)hopTime);
        currentRecipe.hopAmounts.Add(hopQuantity);
        currentRecipe.hopIBUs.Add(hopAdditionIBUs);
        currentRecipe.iBUs += hopAdditionIBUs;
        if (hopTime < 30)
        {
            currentRecipe.aromas.Add(companyInventory.availableHops[hopIndex].flavours[0]); //This is a placeholder, I will need something a little more advanced later on
        }
        else
        {
        }
        //Get the text object to update:
        Text hopAdditionsText = GameObject.Find("HopAdditionsText").GetComponent<Text>();
        hopAdditionsText.text += listString;
    }

    float CalculateIBUs(int hopIndex, float hopQuantity, float waterVolume, float hoptime, float wortGravity)
    {
        float IBUout = 0;
        Hops hops = companyInventory.availableHops[hopIndex];
        float alphaacids = hops.alphaacids;
        float aaRating = (alphaacids * hopQuantity * 1000) / (waterVolume*1000);
        float bignessFactor = 1.65f * Mathf.Pow(0.000125f,(wortGravity - 1f));
        float boilTimeFactor = (1 - Mathf.Exp(-0.04f * hoptime)) / 4.15f;
        IBUout = aaRating * boilTimeFactor * bignessFactor; //This is all from https://realbeer.com/hops/research.html and should be roughly correct
        return IBUout;
    }

    float waterVolumeSwitch(int index)
    {
        float waterVolume = 50;
        switch(index)
        {
            case 0:
                waterVolume = 50;
                break;
            case 1:
                waterVolume = 75;
                break;
            case 2:
                waterVolume = 150;
                break;
        }
        return waterVolume;
    }
}

