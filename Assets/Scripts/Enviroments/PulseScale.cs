using UnityEngine;

using UnityEngine;
using DG.Tweening;

public class PulseScale : MonoBehaviour, IPulse
{
    [SerializeField] private float pulseScale = 1.15f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutBack;

    private Vector3 _startScale;
    private Tween _currentTween;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void Pulse()
    {
        _currentTween?.Kill();

        transform.localScale = _startScale;
        _currentTween = transform
            .DOScale(_startScale * pulseScale, duration * 0.5f)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                transform.DOScale(_startScale, duration * 0.5f)
                    .SetEase(Ease.InOutSine);
            });
    }
}
