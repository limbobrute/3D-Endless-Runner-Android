using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Buttons;
    public GameObject CoinButton;
    public TextMeshProUGUI CostShower;
    public TextMeshProUGUI HeldCoinUI;
    public GameObject[] Platforms = new GameObject[0];
    public float GlobalSpeedMulti = 1f;
    public int CoinSpawnChance = 15;
    public SwipeDetect swipe;
    public DataHolder dataHolder;

    [SerializeField] GameObject JumpingObstcale;
    [SerializeField] GameObject Coin;
    [SerializeField] int CoinsHeld = 0;
    [SerializeField] float YSpawn = 0.0f;
    [SerializeField] private bool GameOver = false;
    private bool RampedUp = false;
    private bool RampedDown = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
    }

    private void Start()
    {
        CoinsHeld = dataHolder.SavedCoins;
        CoinSpawnChance += dataHolder.CoinSpawnIncrease;
        HeldCoinUI.text = "$ " + CoinsHeld.ToString();
    }

    public void Spawn()
    {
        int rand = Random.Range(0, Platforms.Length);
        GameObject platform = Instantiate(Platforms[rand], new Vector3(0f, YSpawn, 9f), Quaternion.Euler(0f, 90f, 0f));

        Platforms plat = platform.GetComponent<Platforms>();
        if(plat.isRamp && !RampedUp)
        { platform.transform.rotation = Quaternion.Euler(0f, 270f, 0f); RampedUp = true; }
        else if(plat.isRamp && RampedUp)
        { Destroy(platform); Spawn(); }
    }

    public void SetYSpawn(float ySpawn)
    { YSpawn = ySpawn; }

    public void GameEnd()
    {
        GameOver = true;
        swipe.enabled = false;
        Buttons.SetActive(true);
        CheckValidUpgrade();
        GlobalSpeedMulti = 0f;
    }

    public void StartAgain()
    {
        dataHolder.SavedCoins = CoinsHeld;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void IncreaseCoinSpawn(int increase)
    {
        dataHolder.CoinSpawnIncrease += increase;
        dataHolder.CoinSpawnCost += 5;
        CoinsHeld -= dataHolder.CoinSpawnCost;
        HeldCoinUI.text = "$ " + CoinsHeld.ToString();
        CoinSpawnChance += increase;
        Debug.Log("Increased Coin Spawn rate, which is now 0." + CoinSpawnChance.ToString() + "%");
    }

    public void CheckValidUpgrade()
    {
        CostShower.text = "Cost $ " + dataHolder.CoinSpawnCost.ToString();
        Button button = CoinButton.GetComponent<Button>();
        if(CoinsHeld < dataHolder.CoinSpawnCost)
        { button.interactable = false; }
        else 
        { button.interactable = true;}
    }

    public void AddCoin()
    {
        CoinsHeld++;
        HeldCoinUI.text = "$ " + CoinsHeld.ToString();
    }

    public GameObject CoinObj()
    {
        return Coin;
    }

    public GameObject FootObj()
    {
        return JumpingObstcale;
    }
}
