using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SwipeDetect : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Transform Player;
    private Vector2 startPos;
    private float startTime;
    private Vector2 EndPos;
    private float EndTime;

    [SerializeField] float minDistance = .2f;
    [SerializeField] float maxTime = 1f;
    [SerializeField, Range(0f, 1f)] float directionThreshold = 0.9f;

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
            /*Debug.DrawLine(startPos, EndPos, Color.red, 5f);
            Debug.Log("You've Swiped on the screen!!");*/
            Vector3 dir = EndPos - startPos;
            Vector2 dir2D = new Vector2(dir.x, dir.y).normalized;
            SwipeDirection(dir2D);

        }
    }

    private void SwipeDirection(Vector2 dir)
    {
        if(Vector2.Dot(Vector2.up, dir) > directionThreshold)
        {
            Debug.Log("You swiped up");
        }
        if (Vector2.Dot(Vector2.down, dir) > directionThreshold)
        {
            Debug.Log("You swiped down");
        }
        if (Vector2.Dot(Vector2.left, dir) > directionThreshold && Player.position.x != -1f)
        {
            float x = Player.position.x;
            Player.position = new Vector3(x - 1f, 1.5f, 0);
            Debug.Log("You swiped left");
        }
        if (Vector2.Dot(Vector2.right, dir) > directionThreshold && Player.position.x != 1f)
        {
            float x = Player.position.x;
            Player.position = new Vector3(x + 1f, 1.5f, 0);
            Debug.Log("You swiped right");
        }

    }
}
