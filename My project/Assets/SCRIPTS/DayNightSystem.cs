using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DayNightSystem : MonoBehaviour
{
    ///////////////////////// Public Variables ////////////////////////////
    [Header("Light Settings")]
    [SerializeField] private Light directionalLight; // Reference to the Directional Light

    [Header("Time Settings")]
    [SerializeField] private int morningStartHour = 6; // Hour at which morning light intensity begins to increase
    [SerializeField] private int eveningStartHour = 18; // Hour at which evening light intensity begins to decrease
    [SerializeField] private float transitionDuration = 2; // Duration of the light transitions in hours

    //[Header("Events")]
    //public UnityEvent SunriseEvent;
    //public UnityEvent SunsetEvent;

    [Header("Environmental Settings")]
    [SerializeField] private float maxLightIntensity = 1f; // Maximum intensity of directional light
    [SerializeField] private float maxAmbientIntensity = 1f; // Maximum ambient light intensity
    [SerializeField] private float maxReflectionIntensity = 1f; // Maximum reflection intensity
    
    [SerializeField] private float minLightIntensity = 0f; // Minimum intensity of directional light
    [SerializeField] private float minAmbientIntensity = 0f; // Minimum ambient light intensity
    [SerializeField] private float minReflectionIntensity = 0f; // Minimum reflection intensity
    
    [Header("Night Objects")]
    public GameObject[] nightObjects;
    
    ///////////////////////// Private Variables //////////////////////////
    private float lastHourChecked = -1; // Cache the last hour checked to avoid redundant updates

    void Start()
    {
        directionalLight.intensity = 0;
        RenderSettings.ambientIntensity = 0;
        RenderSettings.reflectionIntensity = 0;
    }

    private void Update()
    {
        int currentHour = GameTimeManager.Instance.currentHour;
        float timeOfDay = GameTimeManager.Instance.timeOfDay;

        SunRotation(timeOfDay);

        // Trigger transitions only once per hour
        if (currentHour != lastHourChecked)
        {
            lastHourChecked = currentHour;
            CheckTimeEvents(currentHour);
        }
        
        // Toggle night objects on and off
        if (currentHour >= eveningStartHour || currentHour < morningStartHour)
        {
            ToggleNightObjects(true);
        }
        else
        {
            ToggleNightObjects(false);
        }
    }

    private void CheckTimeEvents(int currentHour)
    {
        if (currentHour == morningStartHour)
        {
            //SunriseEvent.Invoke();
            StartCoroutine(TransitionLighting(minLightIntensity, maxLightIntensity, transitionDuration));
            StartCoroutine(TransitionEnvironmentalLighting(minAmbientIntensity, maxAmbientIntensity, transitionDuration));
            StartCoroutine(TransitionEnvironmentalReflections(minReflectionIntensity, maxReflectionIntensity, transitionDuration));
        }
        else if (currentHour == eveningStartHour)
        {
            //SunsetEvent.Invoke();
            StartCoroutine(TransitionLighting(maxLightIntensity, minLightIntensity, transitionDuration));
            StartCoroutine(TransitionEnvironmentalLighting(maxAmbientIntensity, minAmbientIntensity, transitionDuration));
            StartCoroutine(TransitionEnvironmentalReflections(maxReflectionIntensity, minReflectionIntensity, transitionDuration));
        }
    }

    private void SunRotation(float timeOfDay)
    {
        // Calculate the rotation based on the current time of day
        float rotationDegrees = timeOfDay * 360f - 90f; // Offset by -90 degrees to start at dawn
        directionalLight.transform.rotation = Quaternion.Euler(rotationDegrees, 0f, 0);
    }

    IEnumerator TransitionLighting(float startIntensity, float endIntensity, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            directionalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, time / duration);
            yield return null;
        }
    }
    
    IEnumerator TransitionEnvironmentalLighting(float startMultiplier, float endMultiplier, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            RenderSettings.ambientIntensity = Mathf.Lerp(startMultiplier, endMultiplier, time / duration);
            yield return null;
        }
    }
    
    //Create a coroutine to transition also the intensity multuplier of the environment reflections
    IEnumerator TransitionEnvironmentalReflections(float startIntensity, float endIntensity, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            RenderSettings.reflectionIntensity = Mathf.Lerp(startIntensity, endIntensity, time / duration);
            yield return null;
        }
    }
    
    //write a method to toggle the night objects on and off
    public void ToggleNightObjects(bool isNight)
    {
        foreach (GameObject nightObject in nightObjects)
        {
            nightObject.SetActive(isNight);
        }
    }
    
}
