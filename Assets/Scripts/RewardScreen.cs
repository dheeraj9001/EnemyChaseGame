using UnityEngine;
using UnityEngine.UI;

public class RewardScreen : MonoBehaviour
{
    public Button playAgainBtn;
    public Text rewardText;

    void OnEnable()
    {
        rewardText.text = "YOU WON!";
    }

    void Start()
    {
        playAgainBtn.onClick.AddListener(OnPlayAgainBtnClick);
    }

    public void OnPlayAgainBtnClick()
    {
        GameManager.Instance.RestartGame();
    }
}
