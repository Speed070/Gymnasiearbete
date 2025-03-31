using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Player Setup")]
    public Player player; // Reference to the Player script
    public UIManager uiManager;

    [Header("Enemies")]
    public List<Enemy> enemies = new List<Enemy>();

    [Header("Battle Options")]
    public int intimidateChance = 50; // Base chance to intimidate
    public int walkAwayChance = 40;

    private Enemy currentEnemy;

    public void InitializeBattle(Enemy enemy)
    {
        Debug.Log($"Battle initiated with {enemy.enemyName}");
        currentEnemy = enemy;

        // Update UI with player and enemy status
        uiManager.UpdatePlayerUI(player);
        uiManager.UpdateEnemyUI(currentEnemy);
    }

    public void Strike()
    {
        if (currentEnemy == null) return;

        int damage = player.CalculateStrikeDamage();
        currentEnemy.TakeDamage(damage);
        uiManager.DisplayMessage($"You struck {currentEnemy.enemyName} for {damage} damage!");

        if (currentEnemy.IsDefeated())
        {
            uiManager.DisplayMessage($"{currentEnemy.enemyName} has been defeated!");
            EndBattle();
            return;
        }

        EnemyTurn();
    }

    public void Intimidate()
    {
        if (currentEnemy == null) return;

        bool success = player.TryIntimidate(currentEnemy);
        if (success)
        {
            uiManager.DisplayMessage($"You intimidated {currentEnemy.enemyName}. They hesitated!");
        }
        else
        {
            uiManager.DisplayMessage($"{currentEnemy.enemyName} is unaffected by your intimidation.");
            EnemyTurn();
        }
    }

    public void WalkAway()
    {
        if (currentEnemy == null) return;

        bool success = player.TryWalkAway();
        if (success)
        {
            uiManager.DisplayMessage($"You successfully walked away from {currentEnemy.enemyName}.");
            EndBattle();
        }
        else
        {
            uiManager.DisplayMessage($"You failed to escape from {currentEnemy.enemyName}!");
            EnemyTurn();
        }
    }

    private void EnemyTurn()
    {
        if (currentEnemy == null) return;

        uiManager.DisplayMessage($"{currentEnemy.enemyName} attacks you!");
        currentEnemy.PerformAction(player);
        uiManager.UpdatePlayerUI(player);

        if (player.IsDefeated())
        {
            uiManager.DisplayMessage("You have been defeated!");
            EndBattle();
        }
    }

    private void EndBattle()
    {
        Debug.Log("Battle ended.");
        currentEnemy = null;
        uiManager.DisplayMessage("Battle is over.");
    }
}
