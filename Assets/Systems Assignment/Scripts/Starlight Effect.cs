using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StarlightEffect : MonoBehaviour
{
    public SpriteRenderer overlay;

    // a list of the stars so i can get ahold of all of them
    public List<GameObject> starList;

    private Vector2 mousePos;

    private Color currentStarCol;
    private Color currentAlpha;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void TouchedStar(InputAction.CallbackContext context)
    {

        //Only do this when attack context has been started
            for (int i = 0; i < starList.Count; i++)
            {
                if (context.started == true)
                {
                    SpriteRenderer sr = starList[i].GetComponent<SpriteRenderer>();


                    //if star sprite renderer contains mouse pos && player attack then invoke onstarClick
                    if (sr.bounds.Contains(mousePos))
                    {
                        StarOnClick(i);
                    }
                }
        }
    }

    public void StarOnClick(int i)
    {
        SpriteRenderer sr = starList[i].GetComponent<SpriteRenderer>();
        //Increase Size
        starList[i].transform.localScale = starList[i].transform.localScale * (Vector2.one * 1.2f);

        if (starList[i].transform.localScale.x >= 7)
        {
            StarDestroyed(i);
        }

        //Increase Brightness of the star
        currentStarCol = sr.color;
        currentStarCol.r += 0.25f;
        currentStarCol.g += 0.25f;
        sr.color = currentStarCol;

        //increase brightness of the screen
        currentAlpha = overlay.color;
        currentAlpha.a -= 0.10f;
        overlay.color = currentAlpha;

       

    }

    public void StarDestroyed(int i)
    {
        starList[i].SetActive(false);
        currentAlpha = overlay.color;
        currentAlpha.a += 0.40f;
        overlay.color = currentAlpha;
    }

    //called as an event in the zoom in and out manager script to reset stars when zoomed in
    public void StarHide()
    {
        for (int i = 0; i < starList.Count; i++)
        {
            starList[i].SetActive(false);
        }

    }

    //called as an event in the zoom in and out manager script to hide stars when zoomed out
    public void StarShow()
    {
        overlay.color = new Color(0, 0, 0, 0.75f);
        for (int i = 0; i < starList.Count; i++)
        {
            starList[i].transform.localScale = Vector2.one * 3;
            SpriteRenderer sr = starList[i].GetComponent<SpriteRenderer>();
            sr.color = new Color(0.60f, 0.60f, 0);
            starList[i].SetActive(true);
        }
        
    }

}
