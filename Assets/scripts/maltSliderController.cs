using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maltSliderController : MonoBehaviour
{
    Slider thisSlider;
    RecipePlannerController recipePlannerController;
    public void Start()
    {
        recipePlannerController = GameObject.Find("RecipePanel").GetComponent<RecipePlannerController>();
        thisSlider = gameObject.GetComponent<Slider>();
        thisSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    public void ValueChangeCheck()
    {
        string thisSliderName = gameObject.name;
        //Debug.Log(thisSlider.value);
        bool[] toAdjust = recipePlannerController.toAdjust;
        Debug.Log(toAdjust[recipePlannerController.returnMyIndex(thisSliderName)]);
        if (toAdjust[recipePlannerController.returnMyIndex(thisSliderName)] == true) //Only adjust sliders if use has clicked
        {
            Debug.Log("Sliding");
            recipePlannerController.onSliderSlide(thisSliderName, thisSlider.value);
        }
        recipePlannerController.UpdateResultsText();
    }
}
