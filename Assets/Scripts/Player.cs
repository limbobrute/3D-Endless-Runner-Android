using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject GroundCheck;
    public bool isJumping = false;
    public float speed = 1.0f;
    public float maxHeight = 2.0f;
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
        isJumping = true;
        //touching--;
        StartPos = transform.position;
        //GroundCheck.SetActive(false);
        StartCoroutine(Jump());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Coin(Clone)")
        { gameManager.AddCoin(); }
    }
    IEnumerator Jump()
    {
        float x = 0f;
        if (StartPos.x == 1)
        { x = 1f; }
        else if (StartPos.x == -1f)
        { x = -1f; }
        transform.position = new Vector3(x, 1.51f, transform.position.z);
        while (transform.position.y > 1.5f)
        {
            if(x != transform.position.x)
            { x = transform.position.x; StartPos.x = x; }

            Vector3 newPoint;
            var temp = StartPos + new Vector3(x, maxHeight * (Mathf.Sin(speed * Time.time) + 1), 0f);
            transform.position = new Vector3(transform.position.x, temp.y, 0f);

            newPoint = transform.position;
            newPoint.y += Mathf.Sin(Time.time) * Time.deltaTime;
            transform.position = newPoint;
            yield return null;
        }
        transform.position = StartPos;
        //GroundCheck.SetActive(true);
        isJumping = false;
    }


}
