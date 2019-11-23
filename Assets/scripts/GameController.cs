using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<string> sceneList;
    public List<string> sceneNames;
    public int startingCash = 20000;
    public static GameController instance;
    UIController uIController;
    TimeController timeController;
    private void Awake()
    {
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
        uIController = GameObject.Find("UIController").GetComponent<UIController>();
        timeController = gameObject.GetComponent<TimeController>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainScene")
        {
            timeController.paused = false;
        }
        else
        {
            timeController.paused = true;
        }
        SceneManager.LoadScene(sceneName);
    }

    
}
