using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float timesteplength = 1.0f; //Length of each time step in seconds
    float mainTimer = 0;
    public int gameTime = 0;
    public int gameTimeDayLength = 8;
    public int gameDay = 0;
    UIController uIController;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        mainTimer = 0;
        gameTime = 0;
        gameDay = 0;
        uIController = GameObject.Find("UIController").GetComponent<UIController>();
        uIController.UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            mainTimer += Time.deltaTime;
        }
        if (mainTimer > timesteplength)
        {
            mainTimer = 0;
            TimeStep();
        }
    }

    void TimeStep()
    {
        //This method will execute everything that happens at each timestep
        gameTime++;
        if(gameTime > gameTimeDayLength)
        {
            newDay();
        }
        uIController.UpdateTimeText();
    }

    void newDay()
    {
        gameDay++;
        gameTime = 0;
    }
}
