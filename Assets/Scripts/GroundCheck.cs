using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        { player.Add(); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        { player.Minus(); }
    }
}
