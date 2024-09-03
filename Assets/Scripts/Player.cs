using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject GroundCheck;
    public bool isJumping = false;
    public float maxHeight = 2.0f;
    public float timePeriod = 2.0f;
    public int touching = 0;
    public Rigidbody rb;
    private Vector3 StartPos;

    void Update()
    {
        if(touching == 0 && !isJumping)
        {
            rb.useGravity = true;
            gameManager.GameEnd();
            Debug.Log("You're falling into the abyss. Game Over");
        }
    }

    public void Add()
    {
        touching++;
    }

    public void Minus()
    {
        touching--;
    }

    public void StartJump()
    {
        if (!isJumping)
        {
            isJumping = true;
            StartPos = transform.position;
            StartCoroutine(Jump());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Coin(Clone)")
        { gameManager.AddCoin(); other.gameObject.SetActive(false); }
    }
    IEnumerator Jump()
    {
        float x = 0f;
        if (StartPos.x == 1)
        { x = 1f; }
        else if (StartPos.x == -1f)
        { x = -1f; }
        transform.position = new Vector3(x, 1.55f, transform.position.z);
        float step = 0.1f;
        while (transform.position.y > 1.5f)
        {
            if(x != transform.position.x)
            { x = transform.position.x; StartPos.x = x; }

            Vector3 nextPoint = transform.position;
            nextPoint.y = StartPos.y + maxHeight * Mathf.Sin((Mathf.PI * 2 / (timePeriod*gameManager.GlobalSpeedMulti)) * step);
            step += Time.deltaTime;
            transform.position = nextPoint;
            yield return null;
        }
        transform.position = StartPos;
        isJumping = false;
    }


}
