using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public int health = 50;
    public int maxHealth = 50;
    public int damage = 10;

    public bool IsDefeated()
    {
        return health <= 0;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void PerformAction(Player player)
    {
        player.health -= damage;
        player.health = Mathf.Clamp(player.health, 0, player.maxHealth);
    }
}
