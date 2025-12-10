using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [Header("Positions")]
    public Vector3 startPos;
    public Vector3 centerPos;

    [Header("Hopping Animation")]
    public float hopHeight = 0.25f;
    public float hopDuration = 0.25f;

    private bool isChasing = false;
    private bool isDefeated = false;

    void Start()
    {
        transform.position = startPos;
        StartIdleBreathing();
    }

    public void StartChase()
    {
        isChasing = true;
        LeanTween.cancel(gameObject);
        StartHopLoop();
    }

    void StartIdleBreathing()
    {
        LeanTween.scale(gameObject, new Vector3(1.03f, 0.97f, 1), 1f)
            .setEaseInOutQuad()
            .setLoopPingPong();
    }

    void StartHopLoop()
    {
        if (!isChasing || isDefeated) return;

        float baseY = startPos.y;

        LeanTween.scale(gameObject, new Vector3(1f, 1.08f, 1f), hopDuration);

        LeanTween.moveY(gameObject, baseY + hopHeight, hopDuration)
            .setEaseOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(gameObject, new Vector3(1.05f, 0.92f, 1f), hopDuration);

                LeanTween.moveY(gameObject, baseY, hopDuration)
                    .setEaseInQuad()
                    .setOnComplete(StartHopLoop);
            });
    }


    public void MoveOneStep(float progress)
    {
        if (!isChasing) return;

        Vector3 target = Vector3.Lerp(startPos, centerPos, progress);
        transform.position = new Vector3(target.x, transform.position.y, transform.position.z);
    }


    public void JumpOnPig(Transform pig)
    {
        GameManager.Instance.bgm.PlayGameSound(GameManager.Instance.bgm.pigSadSound, false);

        isChasing = false;
        LeanTween.cancel(gameObject);

        Vector3 target = pig.position + new Vector3(0f, 0.6f, 0f);

        LeanTween.move(gameObject, target, 0.35f)
            .setEaseOutQuad()
            .setOnComplete(StartBirdIdleOnPig);

        Invoke(nameof(PlayPBirdHappySound), 0.3f);

    }
    void PlayPBirdHappySound()
    {
        GameManager.Instance.bgm.PlayGameSound(GameManager.Instance.bgm.birdHappySound, false);
    }



    void StartBirdIdleOnPig()
    {
        LeanTween.scale(gameObject, new Vector3(1.1f, 0.92f, 1f), 0.4f)
            .setEaseInOutQuad()
            .setLoopPingPong();
    }


    public void Defeat()
    {
        isChasing = false;
        isDefeated = true;

        LeanTween.cancel(gameObject);

        LeanTween.scaleY(gameObject, 0.55f, 0.25f);
        LeanTween.scaleX(gameObject, 1.35f, 0.25f);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
        {
            LeanTween.color(gameObject, new Color(1f, 0.7f, 0.7f), 0.3f);
        }
    }
}
