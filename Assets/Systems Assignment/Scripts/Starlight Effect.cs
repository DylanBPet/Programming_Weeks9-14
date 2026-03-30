using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StarlightEffect : MonoBehaviour
{
    //this is the overlay for the zoom in mode that will controll the "brightness"
    public SpriteRenderer overlay;

    // a list of the stars so i can get ahold of all of them
    public List<GameObject> starList;

    //mouse position for clicking on stars
    private Vector2 mousePos;

    //will be used to get the current colour of the stars
    private Color currentStarCol;

    //will be used to get the current alpha of the overlay
    private Color currentAlpha;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //getting mouse position to click on the stars
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void TouchedStar(InputAction.CallbackContext context)
    {

        //for each star in the list get the sprite renderer and check if mouse has clicked it
            for (int i = 0; i < starList.Count; i++)
            {
                //Only do this when attack context has been started
                if (context.started == true)
                {
                    //getting the sprite renderer
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
        //get the sprite renderer again
        SpriteRenderer sr = starList[i].GetComponent<SpriteRenderer>();

        //Increase Size of the star
        starList[i].transform.localScale = starList[i].transform.localScale * (Vector2.one * 1.2f);

        //if the star is too big, call function destroy
        if (starList[i].transform.localScale.x >= 7)
        {
            StarDestroyed(i);
        }

        //Increase Brightness of the star
            //get the current colour
        currentStarCol = sr.color;
            //add some red
        currentStarCol.r += 0.25f;
            //add some green
        currentStarCol.g += 0.25f;
            //change the colour to the new colour
        sr.color = currentStarCol;

        //increase brightness of the screen
            //get the current brightness
        currentAlpha = overlay.color;
            //change it to a new value
        currentAlpha.a -= 0.10f;
            //apply the new value
        overlay.color = currentAlpha;

       

    }

    //will be called when a star gets too big and destorys itself
    public void StarDestroyed(int i)
    {
        //hide the star
        starList[i].SetActive(false);
            //get the current brightness
        currentAlpha = overlay.color;
            //make it darker (less transparent)
        currentAlpha.a += 0.40f;
            //apply it to the overlay
        overlay.color = currentAlpha;
    }

    //called as an event in the zoom in and out manager script to hide stars when zoomed out
    public void StarShow()
    {
        //when you go to zoom in mode, reset all colours and starts
        //reset overlay
        overlay.color = new Color(0, 0, 0, 0.75f);
        for (int i = 0; i < starList.Count; i++)
        {
            //reset scale of all stars
            starList[i].transform.localScale = Vector2.one * 3;
            SpriteRenderer sr = starList[i].GetComponent<SpriteRenderer>();
            //reset colour of all stars
            sr.color = new Color(0.60f, 0.60f, 0);
            //show all stars
            starList[i].SetActive(true);
        }
        
    }

}
