using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HopSliderController : MonoBehaviour
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

    // Update is called once per frame
    void ValueChangeCheck()
    {
        Debug.Log("hopSlider sliding...");
        recipePlannerController.OnHopSliderSlide(thisSlider);
    }
}
