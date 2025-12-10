using UnityEngine;

public class BGMController : MonoBehaviour
{
    [Header("AUDIO SOURCE")]
    public AudioSource bgm;
    public AudioSource audioSource;

    [Header("AUDIO CLIPS")]
    public AudioClip tapsfx;
    public AudioClip birdHappySound;
    public AudioClip pigSadSound;

    public void FadeOut()
    {
        LeanTween.value(gameObject, bgm.volume, 0, 1f)
            .setOnUpdate((float v) => bgm.volume = v);
    }


    public void FadeIn()
    {
        bgm.Play();
        LeanTween.value(bgm.gameObject, bgm.volume, 0.208f, 1f)
            .setOnUpdate((float v) => bgm.volume = v);
    }

    public void PlayGameSound(AudioClip clip, bool IsLoop)
    {
        audioSource.loop = IsLoop;
        audioSource.clip = clip;
        audioSource.Play();
    }

}
