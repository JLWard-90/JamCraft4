using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grainWeightSlider : MonoBehaviour
{
    Slider thisSlider;
    RecipePlannerController recipePlannerController;
    // Start is called before the first frame update
    void Start()
    {
        recipePlannerController = GameObject.Find("RecipePanel").GetComponent<RecipePlannerController>();
        thisSlider = gameObject.GetComponent<Slider>();
        thisSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    void ValueChangeCheck()
    {
        Debug.Log("weight slider sliding...");
        recipePlannerController.OnWeightSliderSlide(thisSlider);
        recipePlannerController.UpdateResultsText();
    }

}
