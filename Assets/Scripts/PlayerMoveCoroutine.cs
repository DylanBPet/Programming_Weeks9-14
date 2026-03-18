using System.Collections;
using UnityEngine;

public class PlayerMoveCoroutine : MonoBehaviour
{
    public Transform player;
    private Vector2 currentPos;
    private Vector2 endPos;

    public Animator playerAnimController;

    Coroutine playerWalkCoroutine;
    Coroutine playerJumpCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartPlayerWalk()
    {
        if(playerWalkCoroutine != null)
        {
            StopCoroutine(playerWalkCoroutine);
        }
        playerWalkCoroutine = StartCoroutine(PlayerWalk());
    }

    public void StartPlayerJump()
    {
        if (playerJumpCoroutine != null)
        {
            StopCoroutine(playerJumpCoroutine);
        }
        playerJumpCoroutine = StartCoroutine(PlayerJump());
    }

    //have the player larp forward and play walking animation
    IEnumerator PlayerWalk()
    {
        currentPos = player.position;
        endPos = currentPos;
        endPos.x += 2;
        float t = 0;
        playerAnimController.SetBool("PlayerRun", true);
        while (t < 1)
        {
            t += Time.deltaTime;
            player.position = Vector2.Lerp(currentPos, endPos, t);
            yield return null;
        }
        playerAnimController.SetBool("PlayerRun", false);
    }

}
