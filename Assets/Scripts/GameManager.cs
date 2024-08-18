using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Buttons;
    public GameObject CoinButton;
    public TextMeshProUGUI HeldCoinUI;
    public GameObject[] Platforms = new GameObject[0];
    public float GlobalSpeedMulti = 1f;
    public int CoinSpawnChance = 15;
    public SwipeDetect swipe;
    public DataHolder dataHolder;

    [SerializeField] GameObject JumpingObstcale;
    [SerializeField] GameObject Coin;
    [SerializeField] int CoinsHeld = 0;
    [SerializeField] private bool GameOver = false; 

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
        Instantiate(Platforms[rand], new Vector3(0f, 0f, 9f), Quaternion.Euler(0f, 90f, 0f));
    }

    public void GameEnd()
    {
        GameOver = true;
        swipe.enabled = false;
        Buttons.SetActive(true);
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
        CoinsHeld -= 10;
        HeldCoinUI.text = "$ " + CoinsHeld.ToString();
        CoinSpawnChance += increase;
        Debug.Log("Increased Coin Spawn rate, which is now 0." + (CoinSpawnChance).ToString() + "%");
    }

    public void CheckValidUpgrade()
    {
        Button button = CoinButton.GetComponent<Button>();
        if(CoinsHeld < 10)
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
