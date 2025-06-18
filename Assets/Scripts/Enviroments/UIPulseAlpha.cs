using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class UIPulseAlpha : MonoBehaviour, IPulse
{
    [SerializeField] private float pulseAlpha = 1.2f;
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Ease easeType = Ease.OutQuad;

    [SerializeField] private Image _image;

    private float _startAlpha;
    private Tween _currentTween;

    private void Awake()
    {
        if (_image == null)
            _image = GetComponent<Image>();

        _startAlpha = _image.color.a;
    }

    public void Pulse()
    {
        _currentTween?.Kill();

        Color startColor = _image.color;
        float targetAlpha = _startAlpha * pulseAlpha;

        _currentTween = DOTween.Sequence()
            .Append(DOFadeAlpha(targetAlpha, duration * 0.5f))
            .Append(DOFadeAlpha(_startAlpha, duration * 0.5f));
    }

    private Tween DOFadeAlpha(float toAlpha, float time)
    {
        return DOTween.To(
            () => _image.color.a,
            a =>
            {
                Color c = _image.color;
                _image.color = new Color(c.r, c.g, c.b, a);
            },
            toAlpha,
            time
        ).SetEase(easeType);
    }
}
