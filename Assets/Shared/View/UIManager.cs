using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-99)]
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Camera UICamera;
    private Dictionary<Type, BaseView> UIViewDictionary = new();

    private List<BaseView> baseViewsPrefab;

    private Dictionary<Type, BaseView> prefabsDictionary = new();

    private BaseView mainView;
    public UnityEvent OnChangedView;
    protected virtual void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public T Get<T>() where T : BaseView
    {
        Type type = typeof(T);

        UIViewDictionary.TryGetValue(type, out BaseView view);
        if(view != null)
        {
            return (T)view;
        }
        return null;
    }

    public async Task<T> Show<T>(Action whenCurrentUIHide = null, Action whenNewUIShow = null) where T : BaseView
    {
        var view = Get<T>();
        if (view != null)
        {
            if (mainView != null)
            {
                await mainView.Hide(true);
            }
            mainView = view;
            await mainView.Show(true);
        }
        return view;
    }

    private Dictionary<string, GameObject> popupPrefabs = new();
    
    public GameObject GetPopup(string name, bool isBlockScreen = false)
    {
        return null;
    }
}