using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject RestartButton;
    public GameObject[] Platforms = new GameObject[0];
    public float GlobalSpeedMulti = 1f;
    public SwipeDetect swipe;

    [SerializeField] private bool GameOver = false; 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        { Destroy(this); }
        else
        { Instance = this; }
    }

    /*private void Start()
    {
        Instantiate(Platforms[0], new Vector3(0f, 0f, 3f), Quaternion.Euler(0f, 90f,0f));
    }*/

    public void Spawn()
    {
        int rand = Random.Range(0, Platforms.Length);
        Instantiate(Platforms[rand], new Vector3(0f, 0f, 9f), Quaternion.Euler(0f, 90f, 0f));
    }

    public void GameEnd()
    {
        GameOver = true;
        swipe.enabled = false;
        RestartButton.SetActive(true);
        GlobalSpeedMulti = 0f;
    }

    public void StartAgain()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
