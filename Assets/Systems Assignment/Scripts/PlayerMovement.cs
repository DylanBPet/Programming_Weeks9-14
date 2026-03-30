using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 movement;
    public bool canMove;

    private Vector2 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
