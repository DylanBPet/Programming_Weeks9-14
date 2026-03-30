using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCoroutines : MonoBehaviour
{
    public Slider worldOverlaySlider;

    private Coroutine turningDay;
    private Coroutine turningNight;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //how to access the slider value
        worldOverlaySlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnToDay()
    {
        if (turningDay != null)
        {
            StopCoroutine(turningDay);
        }
        if (turningNight != null)
        {
            StopCoroutine(turningNight);
        }
        turningDay = StartCoroutine(Day());
    }

    public void TurnToNight()
    {
        if (turningDay != null)
        {
            StopCoroutine(turningDay);
        }
        if (turningNight != null)
        {
            StopCoroutine(turningNight);
        }
        turningNight = StartCoroutine(Night());
    }

    IEnumerator Night()
    {
        while (worldOverlaySlider.value <= 1)
        {
            worldOverlaySlider.value += 0.002f;
            yield return null;
        }
    }

    IEnumerator Day()
    {
        while (worldOverlaySlider.value >= 0)
        {
            worldOverlaySlider.value -= 0.002f;
            yield return null;
        }
    }
}
