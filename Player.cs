using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int strength = 10;

    public bool IsDefeated()
    {
        return health <= 0;
    }

    public int CalculateStrikeDamage()
    {
        return strength;
    }

    public bool TryIntimidate(Enemy enemy)
    {
        return Random.Range(0, 100) < 50; // Example: 50% success
    }

    public bool TryWalkAway()
    {
        return Random.Range(0, 100) < 40; // Example: 40% success
    }
}
