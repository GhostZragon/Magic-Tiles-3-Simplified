using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ParticleEnvironmentManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;

    private void Awake()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
        SetActiveState(false);
    }

    [Button]
    public void SetActiveState(bool isActive)
    {
        foreach (var item in particleSystems)
        {
            if (isActive)
            {
                item.Play();
            }
            else
            {
                item.Stop();
            }
        }
    }
}