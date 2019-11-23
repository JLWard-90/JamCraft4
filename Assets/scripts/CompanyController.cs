using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyController : MonoBehaviour
{
    public static CompanyController instance;
    GameController gameController;
    public int cash;
    public int reputation;
    [SerializeField]
    Vector2 reputationLimits;
    [SerializeField]
    int startingReputation;
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
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cash = gameController.startingCash;
        reputation = startingReputation;
    }
    public void AddCash(int amount)
    {
        cash += amount;
    }
    public void addReputation(int amount)
    {
        reputation += amount;
    }

}
