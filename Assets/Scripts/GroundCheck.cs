using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        { player.Add(); }
        if(this.gameObject.name == "TopCheck")
        { player.rb.useGravity = true; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        { player.Minus(); }

        if (this.gameObject.name == "TopCheck")
        { player.rb.useGravity = false; }
    }
}
