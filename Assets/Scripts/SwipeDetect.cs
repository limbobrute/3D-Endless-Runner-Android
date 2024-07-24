using System.Collections;
using UnityEngine;

public class SwipeDetect : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Transform PlayerObj;
    public Player Player;
    private Vector2 startPos;
    private float startTime;
    private Vector2 EndPos;
    private float EndTime;

    [SerializeField] float minDistance = .2f;
    [SerializeField] float maxTime = 1f;
    [SerializeField, Range(0f, 1f)] float directionThreshold = 0.9f;

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
            Vector3 dir = EndPos - startPos;
            Vector2 dir2D = new Vector2(dir.x, dir.y).normalized;
            SwipeDirection(dir2D);

        }
    }

    private void SwipeDirection(Vector2 dir)
    {
        if(Vector2.Dot(Vector2.up, dir) > directionThreshold)
        {
            Player.StartJump();
            Debug.Log("You swiped up");
        }
        if (Vector2.Dot(Vector2.down, dir) > directionThreshold)
        {
            Debug.Log("You swiped down");
        }
        if (Vector2.Dot(Vector2.left, dir) > directionThreshold && PlayerObj.position.x != -1f)
        {
            float x = PlayerObj.position.x - 1f;
            StartCoroutine(MovePlayer(x));
            Debug.Log("You swiped left");
        }
        if (Vector2.Dot(Vector2.right, dir) > directionThreshold && PlayerObj.position.x != 1f)
        {
            float x = PlayerObj.position.x + 1f;
            StartCoroutine(MovePlayer(x));
            Debug.Log("You swiped right");
        }

    }

    IEnumerator MovePlayer(float target)
    {
        float step = 0f;
        while (Mathf.Abs(PlayerObj.position.x - target) > 0.01f)
        {
            step += Time.deltaTime;
            float x = Mathf.Lerp(PlayerObj.position.x, target, step/2f);
            PlayerObj.position = new Vector3(x, PlayerObj.position.y, 0f);
            yield return null;
        }

        PlayerObj.position = new Vector3(target, PlayerObj.position.y, 0f);
    }
}
