using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    public float speed = 5;
    //public Vector2 input;
    public Vector2 movement;

    public AudioSource sfx;

    public Animator animator;

    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //use with a stick
            transform.position += (Vector3)movement * speed * Time.deltaTime;

        //use with the mouse position
        //transform.position = movement;

        animator.SetFloat("AnimState", movement.magnitude);
        if(movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack " + context.phase);
        if(context.performed == true)
        {
            sfx.Play();
            Debug.Log("You Attacked");
            animator.SetTrigger("Attack");
        }
        
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        //The same as Mouse.current.position.ReadValue()
        movement = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }


}
