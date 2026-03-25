using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerClickToMove : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private Coroutine clickToMoveCoroutine;

    public LineRenderer lr;
    public List<Vector2> points;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        points.Add(transform.position);
        UpdateLineRenderer();
    }

    // Update is called once per frame
    void Update()
    {
       points[0] = transform.position;
        UpdateLineRenderer();
        if (points.Count > 1)
        {
            float distance = Vector2.Distance(points[0], points[1]);
            if (distance < 0.5)
            {
                points.RemoveAt(1);
            }
        }
    }

    IEnumerator ClickToMove()
    {
        //typically this would be done in update to avoid starting a new one breaking the line
        float t = 0;

        animator.SetBool("Run", true);
        while (t < 1)
        {
            t += 1 * Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, t);
            yield return null;  
        }
        animator.SetBool("Run", false);

        UpdateLineRenderer();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //when clicked, endPos is set to where it has been clicked
        //StartPos is where the player currently is
        //do the ClickToMove()Coroutine
        if(context.started == true)
        {
            CalculateNewLine();

            if (clickToMoveCoroutine != null )
            {
                animator.SetBool("Run", false);
                StopCoroutine(clickToMoveCoroutine);
            }

            clickToMoveCoroutine = StartCoroutine(ClickToMove());
        }
    }

    private void UpdateLineRenderer()
    {
        lr.positionCount = points.Count;
        for(int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points[i]);
        }
    }

    private void CalculateNewLine()
    {
        startPos = transform.position;
        endPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        //adding it to array
        points.Add(startPos);
        points.Add(endPos);
        UpdateLineRenderer();
    }

}
