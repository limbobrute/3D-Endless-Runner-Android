using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public Player player;
    //[SerializeField] private int touching = 0;
    void OnTriggerEnter(Collider other)
    {
        player.Add();
    }

    private void OnTriggerExit(Collider other)
    {
        player.Minus();
    }
}
