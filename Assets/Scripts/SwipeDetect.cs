using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetect : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    private Vector2 startPos;
    private float startTime;
    private Vector2 EndPos;
    private float EndTime;

    [SerializeField] float minDistance = .2f;
    [SerializeField] float maxTime = 1f;

    private void Start()
    {
        //inputManager = GetComponent<InputManager>();//InputManager.Instance; 
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 pos, float time)
    {
        startPos = pos;
        startTime = time;
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        EndPos = pos;
        EndTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if(Vector3.Distance(startPos, EndPos) >= minDistance && EndTime - startTime <= maxTime)
        {
            Debug.DrawLine(startPos, EndPos, Color.red, 5f);
            Debug.Log("You've Swiped on the screen!!");
        }
    }
}
