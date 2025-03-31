using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void UpdatePlayerUI(Player player)
    {
        Debug.Log($"Player Health: {player.health}/{player.maxHealth}");
    }

    public void UpdateEnemyUI(Enemy enemy)
    {
        Debug.Log($"Enemy Health: {enemy.health}/{enemy.maxHealth}");
    }

    public void DisplayMessage(string message)
    {
        Debug.Log(message);
    }
}
