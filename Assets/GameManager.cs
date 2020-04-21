using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer background;
    public Text lengthText;
    public Text timerText;
    public Text resultText;
    public Text messageText;
    public Text foodCountText;
    public float countDown;
    public GameObject gameBoard;


    public static GameManager Instance;
    private bool pauseGame = false;
    private bool dead;
    private float currentTimeScale;
    private float timer;
    private float foodCount;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0.5f;
        currentTimeScale = Time.timeScale;
        ResetTimer();
    }

    private void Update()
    {
        if (pauseGame)
            Time.timeScale = 0;
        else
            Time.timeScale = currentTimeScale;

        if (dead || pauseGame)
            return;

        Timer();

        currentTimeScale = Mathf.Clamp(currentTimeScale, 0, 20f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameBoard.SetActive(true);
            resultText.text = "Pause";
            PauseGame();
        }

        lengthText.text = SnakeController.BodyLength().ToString();
    }

    private void Timer()
    {
        timer -= Time.deltaTime / Time.timeScale;
        timerText.text = ((int) timer).ToString();
        if (timer < 0)
        {
            SnakeController.EatSelf();
            ResetTimer();
        }
    }

    public static void GameOver()
    {
        SoundManager.PlayFail();
        Instance.gameBoard.SetActive(true);
        Instance.resultText.text = "GAME OVER";
        Instance.dead = true;
        PauseGame();
    }

    public static void PauseGame()
    {
        Instance.pauseGame = true;
    }

    public static void SpeedUp()
    {
        Instance.currentTimeScale += 0.02f;
    }

    public static void RamdomBackground()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        Instance.background.color = new Color(r, g, b);
    }

    public static void ResetTimer()
    {
        Instance.timer = Instance.countDown;
    }

    public void Restart()
    {
        Instance.gameBoard.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resume()
    {
        Instance.gameBoard.SetActive(false);
        Instance.pauseGame = false;
        if (dead)
            Restart();
    }

    public static void Success()
    {
        SoundManager.PlaySuccess();
        Instance.gameBoard.SetActive(true);
        Instance.resultText.text = "MISSION COMPLETE!";
        PauseGame();
    }

    public static void SetMessage(string message)
    {
        Instance.messageText.gameObject.SetActive(true);
        Instance.messageText.text = message;
        Instance.Invoke("DisableMessageText", 2f);
    }

    private void DisableMessageText()
    {
        Instance.messageText.gameObject.SetActive(false);
    }

    public static void SpeedDown()
    {
        Instance.currentTimeScale -= 0.01f;
    }

    public static void EatFood()
    {
        Instance.foodCount++;
        Instance.foodCountText.text = Instance.foodCount.ToString();
    }
}