using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI ELEMENTS")]
    public Button playGameBtn;
    public Button backBtn;
    public GameObject rewardScreen;
    public GameObject startScreen;

    [Header("CLASS REF.")]
    public CharacterMover bird;
    public CharacterMover pig;
    public ProgressBar progressBar;
    public BGMController bgm;

    public int totalTapsRequired = 10;
    private int currentTapCount = 0;
    private bool gameFinished = false;
    private bool isGameStart = false;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }

    void Start()
    {
        bgm.FadeOut();
        playGameBtn.onClick.AddListener(OnGamePlayBtnClick);
        backBtn.onClick.AddListener(OnBackButnClick);
    }

    void Update()
    {
        if (isGameStart)
        {
            if (gameFinished) return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0))
            {
                currentTapCount++;
                float progress = (float)currentTapCount / totalTapsRequired;

                bird.MoveOneStep(progress);
                pig.MoveOneStep(progress);
                bgm.PlayGameSound(bgm.tapsfx, false);
                progressBar.SetProgress(progress);

                if (currentTapCount >= totalTapsRequired)
                    FinishGame();
            }
        }
    }

    private void OnGamePlayBtnClick()
    {
        bird.StartChase();
        pig.StartChase();
        startScreen.gameObject.SetActive(false);
        bgm.FadeIn();
        progressBar.gameObject.SetActive(true);
        bird.gameObject.SetActive(true);
        pig.gameObject.SetActive(true);
        isGameStart = true;
        backBtn.gameObject.SetActive(true);
    }

    private void OnBackButnClick()
    {
        RestartGame();
    }

    void FinishGame()
    {
        gameFinished = true;
        bgm.FadeOut();
        bird.JumpOnPig(pig.transform);
        pig.Defeat();
        StartCoroutine(ActivateRewardScreenWithDelay(1.6f));
    }

    IEnumerator ActivateRewardScreenWithDelay(float DelayTime)
    {
        yield return new WaitForSeconds(DelayTime);
        rewardScreen.SetActive(true);
        backBtn.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
