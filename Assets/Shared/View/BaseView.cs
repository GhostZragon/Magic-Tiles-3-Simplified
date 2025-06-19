
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class BaseView : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    [SerializeField] protected GameObject view;
    public bool IsShowing;
    private void Reset()
    {
        if (!view)
        {
            view = transform.Find("Container").gameObject;
        }

        if (!canvasGroup)
            canvasGroup = view.GetComponent<CanvasGroup>();
    }

  
    public virtual async Task Show(bool isAnimated = false)
    {
        canvasGroup.gameObject.SetActive(true);
        SetBlockRaycast(true);
        if (isAnimated)
        {
            var UITask = canvasGroup.DOFade(1,1).AsyncWaitForCompletion();

            await Task.WhenAll(UITask);
            
            return;
        }

        canvasGroup.DOFade(1, 0);
    }

    public virtual async Task Hide(bool isAnimated = false)
    {
        SetBlockRaycast(false);
        if (isAnimated)
        {
            var UITask = canvasGroup.DOFade(0,1).AsyncWaitForCompletion();

            await Task.WhenAll(UITask);
            canvasGroup.gameObject.SetActive(false);
            return;
        }

        canvasGroup.DOFade(0, 0);
        canvasGroup.gameObject.SetActive(false);
        
    }

    public void SetBlockRaycast(bool isBlock)
    {
        canvasGroup.blocksRaycasts = isBlock;
    }

}