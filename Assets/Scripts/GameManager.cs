using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText, arrowCountText, HighScoreText, currentScoreText;

    // timerText;
    public int totalScore = 0, HighestScore = 0, noNewArrowfromSideScore, arrowCount = 10, maxArrowCount = 10;
    public GameObject gameOverScreen, HitBoard, arrow, topBarUI, progessBarTimer;

    private string highScoreKey = "HighScore";
    [SerializeField]
    private float arrowTimer = 3f;

    public static GameManager Instance { get; private set; }

    public loadInterstitial interstitialLoader;
    public loadRewarded rewardedLoader;
    public initializeAds adsInitializer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Instance = this;
        progessBarTimer.SetActive(false);
        gameOverScreen.SetActive(false);
        noNewArrowfromSideScore = Random.Range(40, 60);
        HitBoard = GameObject.FindGameObjectWithTag("Target");
        UpdateUI();
        LoadHighestScore();

        initializeAds.OnAdsInitialized += OnAdsInitialized;
    }

    void OnAdsInitialized()
    {
        // Ads are initialized, now load the initial ads
        interstitialLoader.androidAdUnitId = "YOUR_ANDROID_INTERSTITIAL_AD_UNIT_ID";
        interstitialLoader.iosAdUnitId = "YOUR_IOS_INTERSTITIAL_AD_UNIT_ID";


        rewardedLoader.androidAdUnitId = "YOUR_ANDROID_REWARDED_AD_UNIT_ID";
        rewardedLoader.iosAdUnitId = "YOUR_IOS_REWARDED_AD_UNIT_ID";

        // Load the rewarded ad before attempting to show it
        rewardedLoader.LoadAd();

    }

    void Update()
    {
        if (!gameOverScreen.activeSelf && totalScore >= 1000)
        {
            progessBarTimer.SetActive(true);
            arrowTimer -= Time.deltaTime;
            if (arrowTimer <= 1)
            {
                ArrowSpawner.Instance.AutoReleaseArrow();
                arrowTimer = 3f;
            }
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        scoreText.text = $"Score: {totalScore}";
        currentScoreText.text = $"Current {scoreText.text}";
        arrowCountText.text = $"Arrows: {arrowCount}";
        HighScoreText.text = $"High Score: {GetHighestScore()}";
        // timerText.text = Mathf.Round(arrowTimer).ToString();
    }

    public void UpdateScore(int score)
    {
        totalScore += score;

        if (totalScore > GetHighestScore())
            SaveHighScore();

        if (totalScore <= noNewArrowfromSideScore)
        {
            AddNewArrow(1);
        }
        else if (totalScore > noNewArrowfromSideScore)
        {
            AddNewArrow(2);
        }

        CheckArrowCount();
        UpdateUI();
    }

    int GetHighestScore() => PlayerPrefs.GetInt(highScoreKey, 0);

    void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, totalScore);
        PlayerPrefs.Save();
    }

    void LoadHighestScore() => HighestScore = GetHighestScore();

    public void UseArrow(int arrow = 1)
    {
        arrowCount -= arrow;
        CheckArrowCount();
        UpdateUI();
    }

    void CheckArrowCount()
    {
        arrowCount = Mathf.Clamp(arrowCount, 0, maxArrowCount);
        UpdateUI();
        if (arrowCount <= 0)
            GameOver();
        else if (arrowCount > maxArrowCount)
            Debug.Log("Arrow count exceeds the maximum. Arrows won't be added.");
    }

    public void AddNewArrow(int arrow)
    {
        if (arrowCount + arrow <= maxArrowCount)
        {
            arrowCount += arrow;
            Debug.Log("Arrow added.:  " + arrow);
            UpdateUI();
        }
        else
            Debug.Log("Cannot add more arrows. Arrow count is at the maximum.");
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        HitBoard.SetActive(false);
        arrow.SetActive(false);
        topBarUI.SetActive(false);
        HighScoreText.enabled = true;
        interstitialLoader.LoadAd();
    }

    private void OnDestroy()
    {
        Instance = null;
        initializeAds.OnAdsInitialized -= OnAdsInitialized;
    }

    public void DoubleScore()
    {
        rewardedLoader.ShowAd(OnRewardedAdSuccess);
    }


    public void OnRewardedAdSuccess()
    {
        // Scene currentScene = SceneManager.GetActiveScene();
        // SceneManager.LoadScene(currentScene.name);
        totalScore *= 2;
        UpdateUI();
        Debug.Log("Rewarded ad success. Total Score :  " + totalScore);
    }
}
