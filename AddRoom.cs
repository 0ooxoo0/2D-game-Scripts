using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddRoom : MonoBehaviour
{
    public bool KeyStone = false;
    public int NumberScene;

    [Header("Boss")]
    private float times = 6;
    public bool bosSpawn = false;
    public bool bosDead = false;

    [Header("Walls")]
    public GameObject[] walls;
    public GameObject wallEffect;
    public GameObject door;

    [Header("Enemies")]
    public GameObject[] enemyTypes;
    public Transform[] enemySpawners;

    [Header("Powerups")]
    public GameObject shield;
    public GameObject healthPosition;

    [HideInInspector] public List<GameObject> enemies;

    private RoomVariants variants;
    private bool spawned;
    private bool wallsDestroyed;

    public void Update()
    {
        if (bosDead == true)
        {
            DestroyWalls();
        }
        if (Player.BossSpawn == false)
        {
            times -= Time.deltaTime;
        }
        if (times <= 0)
        {
            SceneManager.LoadScene(NumberScene);
        }
        if (bosSpawn = true && KeyStone == true)
        {
            SceneManager.LoadScene(NumberScene);
        }
    }
    private void Awake()
    {
        variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    }
    private void Start()
    {
        variants.rooms.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned && bosSpawn == true)
        {
            if (walls.Length > 1)
            {
                walls[0].SetActive(true);
                walls[1].SetActive(true);
            }
            if (walls.Length == 1)
            {
                walls[0].SetActive(true);
            }
        }
        if (other.CompareTag("Player") && !spawned && bosSpawn == false)
        {
            spawned = true;
            if (walls.Length > 1)
            {
                Enemy.v = true;
                walls[0].SetActive(true);
                walls[1].SetActive(true);
            }
            if (walls.Length == 1)
            {
                walls[0].SetActive(true);
            }
            foreach (Transform spawner in enemySpawners)
            {
                int rand = Random.Range(0, 11);
                if (rand <= 8)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                else if (rand == 9)
                {
                    Instantiate(healthPosition, spawner.position, Quaternion.identity);
                }
                else if (rand > 9)
                {
                    Instantiate(shield, spawner.position, Quaternion.identity);
                }
            }
            StartCoroutine(CheckEnemies());
        }
        else if (other.CompareTag("Player") && spawned)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = false;
                enemy.GetComponent<EnemyNew>().playerNotInRoom = false;
                enemy.GetComponent<EnemyDalnic>().playerNotInRoom = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spawned)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = true;
            }
        }
        if (other.tag == "Boss")
        {
            bosDead = true;
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (GameObject wall in walls)
        {
            if (wall != null && wall.transform.childCount != 0 && bosSpawn == false)
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
            }
            if (bosSpawn == true && bosDead == true)
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (wallsDestroyed && other.CompareTag("Stena"))
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Boss")
        {
            Player.BossSpawn = true;
            bosSpawn = true;
        }
        if (other.tag == "KeyStone")
        {
            KeyStone = true;
        }
    }
}

        }
            if (other.CompareTag("Player") && !spawned && bosSpawn == false)
        {
            spawned = true;
            if (walls.Length > 1)
            {
                Enemy.v = true;
                walls[0].SetActive(true);
                walls[1].SetActive(true);
            }
            if (walls.Length == 1)
            {
                walls[0].SetActive(true);
            }
            foreach (Transform spawner in enemySpawners)
            {
                int rand = Random.Range(0, 11);
                if (rand <= 8)
                {
                    GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Length)];
                    GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                    enemy.transform.parent = transform;
                    enemies.Add(enemy);
                }
                else if (rand == 9)
                {
                    Instantiate(healthPosition, spawner.position, Quaternion.identity);
                }
                else if (rand > 9)
                {
                    Instantiate(shield, spawner.position, Quaternion.identity);
                }
            }
            StartCoroutine(CheckEnemies());
        }
        else if(other.CompareTag("Player") && spawned)
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = false;
                enemy.GetComponent<EnemyNew>().playerNotInRoom = false;
                enemy.GetComponent<EnemyDalnic>().playerNotInRoom = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && spawned)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().playerNotInRoom = true;
            }
        }
        if (other.tag == "Boss")
        {
            bosDead = true;
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach(GameObject wall in walls)
        {
            if(wall != null && wall.transform.childCount != 0 && bosSpawn == false)
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
            }
            if(bosSpawn == true && bosDead == true)
            {
                Instantiate(wallEffect, wall.transform.position, Quaternion.identity);
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(wallsDestroyed && other.CompareTag("Stena"))
        {
            Destroy(other.gameObject);
        }
        if (other.tag == "Boss")
        {
            Player.BossSpawn = true;
            bosSpawn = true;
        }
        if(other.tag == "KeyStone")
        {
            KeyStone = true;
        }
    }
}
