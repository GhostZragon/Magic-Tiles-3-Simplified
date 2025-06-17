using UnityEngine;

[CreateAssetMenu(fileName = "BoolEventSO", menuName = "Events/Bool Event")]
public class BoolEventSO : PrimitiveEventSO<bool>
{
    public void ToggleValue()
    {
        Value = !value;
    }
}