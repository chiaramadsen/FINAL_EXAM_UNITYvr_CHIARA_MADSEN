using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class Builder_v2 : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    [Header("Building")]
    public GameObject Building; // The house prefab that will be instantiated
    public Transform DeadPosition; // Starting position for the house building animation
    public Transform AlivePosition; // End position for the house building animation
    public UnityEvent OnHouseBuiltEvent; // Event that will be called when the house is built
    
    [Header("Dependencies")]
    public PlayerResources playerResources;
    public TextMessageController TextMessanger;

    [Header("Configuration")]
    public int woodCost = 50; // Wood cost to build the house
    public float buildTime = 2.0f; // Time it takes to build the house

    [Header("Messages")]
    public string notEnoughWoodMessage = "Not enough wood to build the house!";
    public string houseBuiltMessage = "House built successfully!";

    ///////////////////////// Unity Methods /////////////////////////////

    void Start(){}

    ///////////////////////// Control Methods //////////////////////////
    public void TryToBuildHouse()
    {
        if (playerResources.Wood >= woodCost)
        {
            playerResources.Wood -= woodCost;
            
            BuildHouse();
            
            TextMessanger.SendTextWithTween(houseBuiltMessage);
        }
        else
        {
            TextMessanger.SendTextWithTween(notEnoughWoodMessage);
        }
    }
    
    ///////////////////////// Visualization Logic //////////////////////////

    private void BuildHouse()
    {
        Building.transform.position = DeadPosition.position;
        Building.SetActive(true);

        Building.transform.DOMove(AlivePosition.position, buildTime)
            .OnComplete(() =>
            {
                Building.transform.position = AlivePosition.position;
                
                OnHouseBuiltEvent.Invoke();
            });
    }
}
