using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ZoomingInAndOut : MonoBehaviour
{
    //ZoomInMode variables I will need
    public GameObject ZoomInMode;
    public SpriteRenderer zoomInModeSprite;
    public Animator zoomInModeAnimator;

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
        GoToZoomOutMode();
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
                //check if any planet has been clicked by playing using the attack started
                if (planetSprites[i].bounds.Contains(mousePos) == true)
                {
                   
                    //only do it when context is started
                    if (context.started == true)
                    {
                       
                        //Change InZoomOutMode to false because we are no longer in zoom out mode
                        inZoomOutMode = false;
                        Debug.Log("Go to zoom in mode");

                        //Get the sprite from the clicked planet and change the zoom in mode sprite to be the same as it
                        newPlanetSprite = planetSprites[i].sprite;
                        zoomInModeSprite.sprite = newPlanetSprite;

                        //change to zoom in mode

                        ZoomOutMode.SetActive(false);
                        ZoomInMode.SetActive(true);

                        //change the animator int to i - This will be set up in the animator under the zoom in planet sprite and play the correct sprite 
                        

                        //Have the black screen fade in and out with a coroutine

                        zoomInModeAnimator.SetInteger("PlanetSpriteNumber", i);
                    }
                }
            }
        }
    }

    public void GoToZoomOutMode()
    {
        //this will go on a button
        inZoomOutMode = true;
        ZoomOutMode.SetActive(true);
        ZoomInMode.SetActive(false);
    }

    IEnumerator fadeIn()
    {
        blackOverlay.color = new Color(1, 1, 1, alpha);
        yield return null;
    }
    IEnumerator fadeout()
    {
        blackOverlay.color = new Color(1, 1, 1, alpha);
        yield return null;
    }

}
