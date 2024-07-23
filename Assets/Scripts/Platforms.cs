using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    GameManager gameManager;
    public float speed = 2f;
    private bool hasSpawned = false;
    private float step = 0f;

    private void Start()
    {
        gameManager = GameManager.Instance;    
    }
    private void Update()
    {
        /*step += Time.deltaTime * gameManager.GlobalSpeedMulti;
        float z = Mathf.Lerp(transform.position.z, -5f, step / speed);
        transform.position = new Vector3(0f, 0f, z);*/
        float z = transform.position.z;
        z = z - (speed * Time.deltaTime * gameManager.GlobalSpeedMulti);
        transform.position = new Vector3(0f, 0f, z);

        if(transform.position.z >= -0.1f && transform.position.z <= 0.1f && !hasSpawned)
        { gameManager.Spawn(); hasSpawned = true; }

        if(transform.position.z <= -4.8f)
        { Destroy(this.gameObject); }
    }
}
