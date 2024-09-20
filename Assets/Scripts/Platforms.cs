using UnityEngine;

public class Platforms : MonoBehaviour
{
    GameManager gameManager;
    GameObject Coin;
    GameObject FootObs;
    public bool isFirst;
    public bool UpRamp;
    public bool DownRamp;
    public GameObject[] SpawnPoints;
    public float RampYSpawn = 0f;
    public float speed = 2f;
    private bool hasSpawned = false;
    [SerializeField] int CoinSpawnChance;

    private void Start()
    {
        gameManager = GameManager.Instance;
        Coin = gameManager.CoinObj();
        FootObs = gameManager.FootObj();
        CoinSpawnChance = gameManager.CoinSpawnChance;

        /*if (UpRamp)
        { gameManager.SetYSpawn(RampYSpawn); }*/

        if (!isFirst)
        { CoinAndFootObs(); }
    }
    private void Update()
    {
        float z = transform.position.z;
        z = z - (speed * Time.deltaTime * gameManager.GlobalSpeedMulti);
        transform.position = new Vector3(transform.position.x, transform.position.y, z);

        if(transform.position.z >= -0.1f && transform.position.z <= 0.1f && !hasSpawned && gameObject.CompareTag("Ground") && !(UpRamp || DownRamp))
        {
            gameManager.Spawn(); 
            hasSpawned = true;
        }
        else if(transform.position.z >= -4.1f && transform.position.z <= -3.9f && !hasSpawned && gameObject.CompareTag("Ground") && (UpRamp || DownRamp))
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
            int rand = Random.Range(0, 101);
            if(rand <= CoinSpawnChance)
            {
                Instantiate(Coin, obj.transform.position, Quaternion.Euler(0f, 90f, 0f), this.transform);
            }
            else if(rand <= CoinSpawnChance + 5f)
            {
                Instantiate(FootObs, obj.transform.position, Quaternion.Euler(0f, 0f, 0f), this.transform);
            }
        }
    }
}
