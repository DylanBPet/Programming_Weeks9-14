using UnityEngine;
using UnityEngine.InputSystem;

public class LocalMultiplayerController : MonoBehaviour
{
    public Vector2 movementInput;
    public float speed = 5;
    public PlayerInput playerInput;

    public LocalMultiplayerManager manager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3) movementInput * speed * Time.deltaTime;
    }

    public void PlayerMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Player " + playerInput.playerIndex + ": Did Attack");
            manager.playerAttacking(playerInput);
        }
        
    }
}
