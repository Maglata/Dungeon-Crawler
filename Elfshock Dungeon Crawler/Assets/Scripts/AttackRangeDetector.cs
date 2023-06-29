using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeDetector : MonoBehaviour
{
    private List<GameObject> enemiesInRange = new();
    private CombatController combatcontroller;

    void Start()
    {
        combatcontroller = GetComponent<CombatController>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Something Entered Attack Range");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
            combatcontroller.enemyInRange = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);

            if (enemiesInRange.Count == 0)
                combatcontroller.enemyInRange = false;
        }
    }

    public List<GameObject> GetEnemiesInRange()
    {
        return enemiesInRange;
    }
}
