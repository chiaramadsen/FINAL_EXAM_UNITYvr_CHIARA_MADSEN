using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeCutter : MonoBehaviour
{

    public float duration = 1.5f;

    public PlayerResources playerResources;

    public float scaleThreshold = 0.1f;
    
    public GameObject Sparks;
    
    public int WoodAmountReceived = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckAndDestroyIfSmall();
    }

    public void TreeCut(float percentage)
    {
        // Ensure the percentage is between 0 and 1
        percentage = Mathf.Clamp01(percentage);

        // Calculate the new scale
        Vector3 newScale = transform.localScale * percentage;

        // On the first tween complete, add a second tween that jiggles the object
        transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f), .75f).OnComplete(() =>
        {
            transform.DOScale(newScale, duration).OnComplete(() =>
                {
                    playerResources.AddWood(WoodAmountReceived);
                    InstantiatePrefab();
                }
            );
        });


    }

    public void CheckAndDestroyIfSmall()
    {
        // Check if the magnitude of the scale is below the threshold
        if (transform.localScale.magnitude < scaleThreshold)
        {
            // If so, destroy the object
            Destroy(gameObject);

        }
    }
    
    public void InstantiatePrefab()
    {
        // Instantiate the prefab at the center of the current object
        Instantiate(Sparks, transform.position, Quaternion.identity);
    }
}
