using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SceneController : MonoBehaviour
{
    private const int TIME_SEC = 20;
    private const float TIME_SPAWN_SEC = 0.3f;
    private const int BASE_SCORE = 10;
    private const float SCORE_MUL = 5.0f;
    private const float SPEED_MUL = 2.0f;
    [SerializeField]
    private GameObject ballPrefab;
    private Text scoreText;
    private Text timerText;
    private int score = 0;
    private float time = 0.0f;
    private float spawnTime = TIME_SPAWN_SEC;

    void Start()
    {
        GameObject textObj = GameObject.Find("UI/ScoreText");
        if (!textObj)
        {
            Debug.Log("Need add Score Text UI to scene");
            Destroy(gameObject);
            return;
        }
        scoreText = textObj.GetComponent<Text>();
        
        GameObject timerObj = GameObject.Find("UI/TimerText");
        if (!timerObj)
        {
            Debug.Log("Need add Timer Text UI to scene");
            Destroy(gameObject);
            return;
        }
        timerText = timerObj.GetComponent<Text>();
    }

    //size in [0.0, 1.0]
    public void AddScore(float size)
    {
        float factor = 1.0f + (1.0f - size) * SCORE_MUL;
        int gainScore = (int)(BASE_SCORE * factor);
        score += gainScore;
        scoreText.text = "Cчет:" + score;
    }
    public float GetSpeedFactor()
    { return 1.0f + time / TIME_SEC * SPEED_MUL; }

    void Update()
    {
        #region RESTART
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.
                GetActiveScene().buildIndex);
        }
        #endregion

        #region SPAWN_BALLS
        if (time >= TIME_SEC)
        {
            time = TIME_SEC;
            timerText.text = "R рестарт";
            return;
        }
        int timeLeft = (int)(TIME_SEC - time);
        timerText.text = "Время:" +
            timeLeft.ToString().PadLeft(2);
        if (spawnTime >= TIME_SPAWN_SEC)
        {
            spawnTime -= TIME_SPAWN_SEC;
            Instantiate(ballPrefab);
        }
        time += Time.deltaTime;
        spawnTime += Time.deltaTime;
        #endregion
    }
}
