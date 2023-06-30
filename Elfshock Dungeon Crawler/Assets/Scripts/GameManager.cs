using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyObj;
    [SerializeField] private GameObject playerObj;

    [SerializeField] private GameObject gateObj;

    [SerializeField] private int minEnemies;
    [SerializeField] private int maxEnemies;

    [SerializeField] private GameObject Level;

    private float minXEnemy = -5f;
    private float maxXEnemy = 1f;
    private float minZEnemy = -5f;
    private float maxZEnemy = 5f;

    private float playerX = -2f;
    private float playerZ = 13f;

    private Transform playerTransform;

    public List<GameObject> enemies = new();

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        // Delete existing enemies
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        enemies.Clear();

        // Delete player
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
        if (existingPlayer != null)
        {
            Destroy(existingPlayer);
        }

        // Reset Gate
        gateObj.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        // Generate Player
        Vector3 playerPosition = new Vector3(playerX, 0f, playerZ);
        GameObject player = Instantiate(playerObj, playerPosition, Quaternion.identity);
        player.transform.eulerAngles = new Vector3(0f, 180f, 0f); // Set y rotation to 180 degrees
        player.GetComponent<CombatController>().OnPlayerDeath += ResetLevel;
        player.transform.SetParent(Level.transform);
        playerTransform = player.transform;

        // Generate Enemies and add them to the list
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minXEnemy, maxXEnemy), 0f, Random.Range(minZEnemy, maxZEnemy));
            GameObject enemy = Instantiate(enemyObj, randomPosition, Quaternion.identity);
            enemy.transform.SetParent(Level.transform);
            enemies.Add(enemy);

            // Subscribe to the OnDestroy event of the enemy object
            var controller = enemy.GetComponent<EnemyController>();

            controller.OnEnemyDestroyed += RemoveEnemyFromList;
            controller.playerTransform = playerTransform;
        }      
    }
    private void RemoveEnemyFromList(GameObject enemy)
    {
        enemies.Remove(enemy);

        if(enemies.Count == 0)
        {
            gateObj.transform.eulerAngles = new Vector3(0f, 90f, 0f);
        }
            
    }

    private void ResetLevel(GameObject player)
    {
        GenerateLevel();
    }
}
