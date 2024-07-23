using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //public GameObject FullPlat;
    public GameObject[] Platforms = new GameObject[0];
    public float GlobalSpeedMulti = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
    }

    private void Start()
    {
        /*GameObject FirstPlat =*/
        Instantiate(Platforms[0], new Vector3(0f,0f,3f), Quaternion.Euler(0f, 90f,0f));
        //StartCoroutine(Spawn());
    }

    public void Spawn()
    {
        int rand = Random.Range(0, Platforms.Length);
        Instantiate(Platforms[rand], new Vector3(0f, 0f, 3f), Quaternion.Euler(0f, 90f, 0f));
    }

    /*IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(FullPLat, new Vector3(0f, 0f, 3f), Quaternion.identity);
        StartCoroutine(Spawn());
    }*/
}
