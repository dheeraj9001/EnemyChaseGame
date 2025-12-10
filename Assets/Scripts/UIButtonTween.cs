using UnityEngine;

public class UIButtonTween : MonoBehaviour
{
    public RectTransform target;

    void Start()
    {
        if (target == null)
            target = GetComponent<RectTransform>();

        StartPulse();
    }

    void StartPulse()
    {
        LeanTween.scale(target, new Vector3(1.1f, 1.1f, 1f), 0.6f)
            .setEaseInOutQuad()
            .setLoopPingPong();
    }
}
