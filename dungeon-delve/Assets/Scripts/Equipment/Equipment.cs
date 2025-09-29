using UnityEngine;

public abstract class Equipment : ScriptableObject
{
    [SerializeField] public Job job { get; private set; }
    [SerializeField] public int health { get; private set; }
    [SerializeField] public int damage { get; private set; }
    [SerializeField] public int speed { get; private set; }
}
