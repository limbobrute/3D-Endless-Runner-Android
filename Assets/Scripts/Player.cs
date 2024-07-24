using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager gameManager;
    //private bool isGrounded = true;
    public int touching = 0;
    public Rigidbody rb;
    void Update()
    {

        if(touching == 0)
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


}
