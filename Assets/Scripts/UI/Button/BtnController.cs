using UnityEngine;
using UnityEngine.UI;

public class BtnController : MonoBehaviour
{
    protected Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickBtn);
    }

    private void OnDestroy()
    {
        btn.onClick.RemoveListener(OnClickBtn);
    }
    protected virtual void OnClickBtn()
    {
    }
}