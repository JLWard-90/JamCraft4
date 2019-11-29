using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    TimeController timeController;
    GameController gameController;
    [SerializeField]
    Text timeText;
    [SerializeField]
    GameObject recipeScreenPrefab;
    public static UIController instance;
    private void Awake()
    {
        timeController = GameObject.Find("GameController").GetComponent<TimeController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        //check if instance exists
        if (instance == null)
        {
            //assign it to the current object:
            instance = this;
        }
        //make sure instance is the current object
        else if (instance != this)
        {
            //destroy the current game object
            Destroy(gameObject);
        }
        //don't destroy on changing scene
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if(!timeController.paused)
        {
            UpdateTimeText();
        }
        
    }
    public void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = string.Format("Day: {0} Time: {1}", timeController.gameDay, timeController.gameTime);
        }
        else
        {
            timeText = GameObject.Find("TimeText").GetComponent<Text>();
            if (timeText != null)
            {
                timeText.text = string.Format("Day: {0} Time: {1}", timeController.gameDay, timeController.gameTime);
            }
            else
            {
                Debug.Log("Error: cannot find timeText");
            }
        }
    }

     private void OnGUI()
    {
        List<string> sceneList = gameController.sceneList;
        List<string> sceneNames = gameController.sceneNames;
        for (int i = 0; i < sceneList.Count; i++)
        {
            if (GUI.Button(new Rect(Screen.width / 40, 200 + Screen.height / 15 + Screen.height / 12 * i, 100, 30), sceneNames[i]))
            {
                if(sceneNames[i] == "Recipe")
                {
                    LoadRecipeScreen();
                }
                else
                {
                    gameController.LoadScene(sceneList[i]);
                }
                
            }
        }
    }

    public void LoadRecipeScreen()
    {
        gameController.GetComponent<TimeController>().paused = true;
        Debug.Log("Loading recipe screen");
        GameObject canvasObject = GameObject.Find("Canvas");
        GameObject recipeScreen = GameObject.Instantiate(recipeScreenPrefab);
        recipeScreen.name = "RecipePanel";
        recipeScreen.transform.SetParent(canvasObject.transform);
        recipeScreen.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        recipeScreen.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
        recipeScreen.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        recipeScreen.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
        recipeScreen.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        recipeScreen.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        recipeScreen.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }
}
