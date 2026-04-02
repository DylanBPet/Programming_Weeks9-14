using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    //Used for speed of camera movment
    public float speed;

    //used to store the store the input value
    private Vector2 movement;

    //able to turn off movment
    public bool canMove;

    //save starting position to reset when planet is clicked
    private Vector2 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //set the starting position and allow movement
        startPos = transform.position;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        //camera position is determined and moved by the movement and speed variable in the OnMove function below
        transform.position += (Vector3)movement * speed * Time.deltaTime;
    }

    //calling the input action for move (WASD)
    public void OnMove(InputAction.CallbackContext context)
    {
        //can only move if the bool is true, it will be turned off when in zoominmode
        if (canMove == true)
        {
            //youre movement is indecated by reading the value of the player input 
            movement = context.ReadValue<Vector2>();
        }
    }

    //this will go on the zoom in/out unity event when zooming in
    public void CannotMoveWhenZoomedIn()
    {
        //resets the camera to center
        transform.position = startPos;

        //stops you from moving
        canMove = false;
    }

    //this will go on the zoom in/out unity event when zooming out
    public void AbleToMoveWhenZoomedOut()
    {
        //resets the camera to center
        transform.position = startPos;

        //allows you to move again
        canMove = true;
    }
}
