using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDetector : MonoBehaviour
{
    private List<GameObject> enemiesInRange = new();
    private CombatController combatcontroller;

    void Awake()
    {
        combatcontroller = GetComponentInParent<CombatController>();      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            combatcontroller.enemyInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);

            if (enemiesInRange.Count == 0)
                combatcontroller.enemyInRange = false;
        }
    }

    public List<GameObject> GetEnemiesInRange()
    {
        return enemiesInRange;
    }
}
