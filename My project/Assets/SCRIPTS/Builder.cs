using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Builder : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    [Header("Dependencies")]
    public TMP_Text messageText; // UI text element for displaying messages
    public PlayerResources playerResources;
    public GameObject housePrefab; // The house prefab that will be instantiated
    public Transform startHousePosition; // Starting position for the house building animation
    public Transform endHousePosition; // End position for the house building animation

    [Header("Configuration")]
    public int woodCost = 50; // Wood cost to build the house
    public float buildTime = 2.0f; // Time it takes to build the house

    [Header("Messages")]
    public string pressEToBuildMessage = "Press E to build the house";
    public string notEnoughWoodMessage = "Not enough wood to build the house!";
    public string houseBuiltMessage = "House built successfully!";

    ///////////////////////// Private Variables //////////////////////////
    private bool isPlayerInTrigger = false;
    private GameObject houseInstance;

    ///////////////////////// Unity Methods /////////////////////////////
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = pressEToBuildMessage;
            isPlayerInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "";
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            TryToBuildHouse();
        }
    }

    ///////////////////////// Control Methods //////////////////////////
    private void TryToBuildHouse()
    {
        if (playerResources.Wood >= woodCost)
        {
            playerResources.Wood -= woodCost;
            BuildHouse();
            messageText.text = houseBuiltMessage;
        }
        else
        {
            messageText.text = notEnoughWoodMessage;
        }
    }
    
    ///////////////////////// Visualization Logic //////////////////////////

    private void BuildHouse()
    {
        houseInstance = Instantiate(housePrefab, startHousePosition.position, Quaternion.identity);
        
        houseInstance.transform.DOMove(endHousePosition.position, buildTime)
            .OnComplete(() =>
            {
                //Destroy the Building Spot Object (this object itself)
                Destroy(gameObject);
            });
    }
}
