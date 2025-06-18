
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
        if (isAnimated)
        {
            var UITask = canvasGroup.DOFade(1,1).AsyncWaitForCompletion();

            await Task.WhenAll(UITask);
            
            return;
        }

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        canvasGroup.DOFade(1, 0);
    }

    public virtual async Task Hide(bool isAnimated = false)
    {
        if (isAnimated)
        {
            var UITask = canvasGroup.DOFade(0,1).AsyncWaitForCompletion();

            await Task.WhenAll(UITask);
            return;
        }
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        canvasGroup.DOFade(0, 0);
        
    }

    public void SetBlockRaycast(bool isBlock)
    {
        canvasGroup.blocksRaycasts = isBlock;
    }

}