using System.Collections;
using UnityEngine;

public class CombatTriggerScript : MonoBehaviour
{
    public BattleManager battleManager;

    [Header("Enemy Setup")]
    public Enemy enemyPrefab;
    public Transform spawnPoint;

    private Enemy spawnedEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCombat();
        }
    }

    private void StartCombat()
    {
        if (spawnedEnemy == null)
        {
            // Spawn the enemy if not already spawned
            spawnedEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }

        // Trigger the battle manager
        battleManager.InitializeBattle(spawnedEnemy);
    }
}
