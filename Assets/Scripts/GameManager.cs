using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float initialGameSpeed = 6f;
    public float gameSpeedIncrease = 0.26f;
    public float gameSpeed { get; private set; }

    public TextMeshPro gameOverText;
    public TextMeshPro scoreText;
    public TextMeshPro hiscoreText;
    public TextMeshPro startHint;
    //public Button retryButton;

    private Player player;
    private Spawner spawner;

    private float textDelay = 1f;
    private float restartDelay = 1.5f;
    private float timeSinceGameOver = 0f;


    private float score;

    bool JumpPressed()
    {
        // Check for keyboard input (for testing in the Unity editor)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return true;
        }

        // Check for touchscreen input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) // TouchPhase.Began means the screen has just been touched
            {
                return true;
            }
        }

        return false;
    }


    public enum GameState
    {
        Stopped,
        Playing,
        GameOver
    }

    public GameState CurrentState
    {
        get { return currentState; }
    }


    private GameState currentState = GameState.Stopped;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        startHint.gameObject.SetActive(true);

        StopGame();
        UpdateHiscore();
        player.gameObject.SetActive(true);
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        startHint.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Vector3 currentCenter = player.character.center;
        player.character.center = new Vector3(0, currentCenter.y, currentCenter.z);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        //retryButton.gameObject.SetActive(false);

        UpdateHiscore();
        currentState = GameState.Playing;
    }

    public void GameOver()
    {

        gameSpeed = 0f;
        //enabled = false; //keeping this makes running state being stopped and key input fail to be detcted

        //player.gameObject.SetActive(false);
        player.transform.rotation = Quaternion.Euler(0, 90, -108); // Set rotation to -90 on z-axis
        Vector3 currentCenter = player.character.center;
        player.character.center = new Vector3(-4.08f, currentCenter.y, currentCenter.z);
        spawner.gameObject.SetActive(false);
        //gameOverText.gameObject.SetActive(true);
        //retryButton.gameObject.SetActive(true);

        UpdateHiscore();
        currentState = GameState.GameOver;
        timeSinceGameOver = 0f;
    }

    private void Update()
    {
        Debug.Log("Current GameState: " + currentState.ToString());

        switch (currentState)
        {
            case GameState.Stopped:
                if (JumpPressed())
                {
                    NewGame();
                    currentState = GameState.Playing;
                }
                break;

            case GameState.Playing:
                gameSpeed += gameSpeedIncrease * Time.deltaTime;
                score += gameSpeed * Time.deltaTime * 3;
                scoreText.text = Mathf.FloorToInt(score).ToString("D6");
                break;

            case GameState.GameOver:

                // Increase the time since game over
                timeSinceGameOver += Time.deltaTime;

                if (timeSinceGameOver >= textDelay)
                {
                    gameOverText.gameObject.SetActive(true);
                }

                // Check if enough time has passed
                if (timeSinceGameOver >= restartDelay && JumpPressed())
                {
                    NewGame();
                    currentState = GameState.Playing;
                }
                break;
        }
    }

    private void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D6");
    }

    public void StopGame()
    {
        gameSpeed = 0f;
        //player.gameObject.SetActive(false);
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        Vector3 currentCenter = player.character.center;
        player.character.center = new Vector3(0, currentCenter.y, currentCenter.z);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        //retryButton.gameObject.SetActive(false);
    }

}
