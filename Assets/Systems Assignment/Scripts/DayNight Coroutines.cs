using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DayNightCoroutines : MonoBehaviour
{
    //the canvas that makes it appear dark in zoom in mode
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
        //starting the day coroutine, but stopping the night coroutine first
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
        //starting the night coroutine, but stopping the day coroutine first
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
        //while the slider is less than or equal to 1, make slider increase
        while (worldOverlaySlider.value <= 1)
        {
            worldOverlaySlider.value += 0.002f;
            yield return null;
        }
    }

    IEnumerator Day()
    {
        //while the slider is more than or equal to 0, make slider decrease
        while (worldOverlaySlider.value >= 0)
        {
            worldOverlaySlider.value -= 0.002f;
            yield return null;
        }
    }
}
