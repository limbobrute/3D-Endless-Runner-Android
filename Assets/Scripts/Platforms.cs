using UnityEngine;

public class Platforms : MonoBehaviour
{
    GameManager gameManager;
    GameObject Coin;
    GameObject FootObs;
    public bool isFirst;
    public GameObject[] SpawnPoints;
    public float speed = 2f;
    private bool hasSpawned = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Coin = gameManager.CoinObj();
        FootObs = gameManager.FootObj();

        if (!isFirst)
        { CoinAndFootObs(); }
    }
    private void Update()
    {
        float z = transform.position.z;
        z = z - (speed * Time.deltaTime * gameManager.GlobalSpeedMulti);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);

        if(transform.position.z >= -0.1f && transform.position.z <= 0.1f && !hasSpawned && gameObject.CompareTag("Ground"))
        {
            gameManager.Spawn(); 
            hasSpawned = true; 
        }

        if(transform.position.z <= -18.8f)
        { Destroy(this.gameObject); }
    }

    void CoinAndFootObs()
    {
        foreach(GameObject obj in SpawnPoints) 
        {
            float rand = Random.Range(0f, 10f);
            if(rand <= 1.5f)
            {
                Instantiate(Coin, obj.transform.position, Quaternion.Euler(0f, 90f, 0f), this.transform);
            }
            else if(rand <= 2f)
            {
                Instantiate(FootObs, obj.transform.position, Quaternion.Euler(0f, 0f, 0f), this.transform);
            }
        }
    }
}
