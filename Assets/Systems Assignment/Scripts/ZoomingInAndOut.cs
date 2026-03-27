using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomingInAndOut : MonoBehaviour
{
    //ZoomInMode variables I will need
    public GameObject ZoomInMode; //The parent game object
    public SpriteRenderer zoomInModeSprite; //the current sprite renderer
    public Animator zoomInModeAnimator; //the animator for the planet
    public GameObject zoomInCanvas; //the canvas

    //ZoomOutMode variables I will need
    public GameObject ZoomOutMode;

    
    //A list of all the planets that are around the map
    public List<SpriteRenderer> planetSprites;

    //keeps track of the mouse position in update
    private Vector2 mousePos;

    //will track what mode we are in, not needed in a project this small but only certain codes will even be looked for depening on the mode to lighten burden on hardware
    private bool inZoomOutMode;

    //This will store a sprite that will be applied to the zoom in mode sprite
    private Sprite newPlanetSprite;

    public SpriteRenderer blackOverlay;
    private float alpha;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //turn Zoom in mode on just in case the canvas is not turned off itself (this will effect the fade to black effect)
        ZoomInMode.SetActive(true);

        //turn them back off with the canvas first
        zoomInCanvas.SetActive(false);
        ZoomInMode.SetActive(false);

        //turn zoom out mode on
        inZoomOutMode = true;
        ZoomOutMode.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    public void GoToZoomInMode(InputAction.CallbackContext context)
    {
        //this will go on the manager script and will take a hitbox for each planet

        //Only do this if we are in zoom out mode
        if(inZoomOutMode == true)
        {
            //Make an arraylist of planet sprites, when clicked, go to zoom in mode and change the zoom in mode sprite to the same sprite as the planet clicked
            for (int i = 0; i < planetSprites.Count; i++)
            {
                //Only do this when attack context has been started
                if (context.started == true)
                {
                    //run this function that checks through the array to see if you clicked inside a planet. If yes, change to zoom in mode
                    StartCoroutine (CheckTheList(i));
                }
            }
        }
    }

    IEnumerator CheckTheList(int i)
    {
        //check if any planet has been clicked by playing using the attack started
        if (planetSprites[i].bounds.Contains(mousePos) == true)
        {
                //Change InZoomOutMode to false because we are no longer in zoom out mode
                inZoomOutMode = false;

                //Get the sprite from the clicked planet and change the zoom in mode sprite to be the same as it
                newPlanetSprite = planetSprites[i].sprite;
                zoomInModeSprite.sprite = newPlanetSprite;


            //make the screen fade effect & wait for it to finish+
            alpha = 0;
            while (alpha <= 1)
            {
                blackOverlay.color = new Color(0, 0, 0, alpha);
                alpha += 0.005f;
                yield return null;
            }

            //change to zoom in mode
            ZoomOutMode.SetActive(false);
            ZoomInMode.SetActive(true);
            //change the animator int to i - This will be set up in the animator under the zoom in planet sprite and play the correct sprite 
            zoomInModeAnimator.SetInteger("PlanetSpriteNumber", i);

            //Have the black screen fade in and out with a coroutine (have it hold on a black screen for a few frames)
            alpha = 1.1f;
            while (alpha >= 0)
            {
                blackOverlay.color = new Color(0, 0, 0, alpha);
                alpha -= 0.003f;
                yield return null;
            }
            zoomInCanvas.SetActive(true);
        }
    }

    public void ZoomOutCoroutine()
    {
        //this will go on a button
        StartCoroutine(GoToZoomOutMode());
    }

    IEnumerator GoToZoomOutMode()
    {
        //turn off the canvas
        inZoomOutMode = true;
        zoomInCanvas.SetActive(false);

        //fade to black
        alpha = 0;
        while (alpha <= 1)
        {
            blackOverlay.color = new Color(0, 0, 0, alpha);
            alpha += 0.005f;
            yield return null;
        }

        //turn off zoom in mode
        ZoomInMode.SetActive(false);
        //Change game object back to zoom out mode
        ZoomOutMode.SetActive(true);

        //fade back to nothing (hold black for a few frames)
        alpha = 1.1f;
        while (alpha >= 0)
        {
            blackOverlay.color = new Color(0, 0, 0, alpha);
            alpha -= 0.003f;
            yield return null;
        }
    }

}
