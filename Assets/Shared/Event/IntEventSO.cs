using UnityEngine;

[CreateAssetMenu(fileName = "IntEventSO", menuName = "Events/Int Event")]
public class IntEventSO : PrimitiveEventSO<int>
{
    public void Increment()
    {
        Value++;
    }

    public void Decrement()
    {
        Value--;
    }

    public void Add(int amount)
    {
        Value += amount;
    }
}
