using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-99)]
public class UIManager : UnitySingleton<UIManager>
{
    public Camera UICamera;
    private Dictionary<Type, BaseView> UIViewDictionary = new();

    private List<BaseView> baseViewsPrefab;

    private Dictionary<Type, BaseView> prefabsDictionary = new();

    private BaseView mainView;

    protected override void Awake()
    {
        base.Awake();
        var views = GetComponentsInChildren<BaseView>();

        foreach (var view in views)
        {
            var type = view.GetType();
            if (UIViewDictionary.ContainsKey(type))
            {
                Debug.Log(
                    $"This type is already contain in dictionary: {type}, type is in dictionary is {UIViewDictionary[type].gameObject.name}");
                continue;
            }
            else
            {
                Debug.Log($"Add {type} to views", view.gameObject);
            }

            UIViewDictionary[type] = view;
        }
    }

    public T Get<T>() where T : BaseView
    {
        Type type = typeof(T);

        UIViewDictionary.TryGetValue(type, out BaseView view);
        if (view != null)
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