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
    [SerializeField]
    bool testing  =true;
    public Inventory inventory;
    public List<Recipe> recipes;
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
        recipes = new List<Recipe>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cash = gameController.startingCash;
        reputation = startingReputation;
        if(testing)
        {
            InitStartingInventory();
        }
    }
    public void AddCash(int amount)
    {
        cash += amount;
    }
    public void addReputation(int amount)
    {
        reputation += amount;
    }
    public void InitStartingInventory()
    {
        inventory = new Inventory();
        inventory.availableMalts.Add(new Malt("Dummy Malt", "Base", 5, 0.8f, 15, new string[] { "beer falvour1", "beer flavour2" }, 2));
        inventory.availableMalts.Add(new Malt("2 Row Pale Ale", "Base", 5, 0.8f, 15, new string[] { "beer falvour1", "beer flavour2" }, 2));
        inventory.availableMalts.Add(new Malt("Cara120 EBC", "Crysal/Cara", 120, 0.7f, 15, new string[] { "Caramel" }, 3));
        inventory.availableHops.Add(new Hops("Brewer's Gold", "Bittering", new string[] { "Fruity", "Spicy" }, 50, 15, 5));
        inventory.availableHops.Add(new Hops("Fuggles", "Universal", new string[] { "Fuity", "Spicy" }, 30, 15, 5));
        inventory.availableYeasts.Add(new Yeast("Basic Ale Yeast", "Ale", new string[] { "Esters", "Bread" }, 1.010f, 5, new Vector2 (18, 25), 15, 2));
    }
}
