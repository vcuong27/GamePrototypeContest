using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //instance
    public static GameManager Instance;

    public Canvas UI;
    public Canvas Background;
    public MagazineUI magazineUI;
    public HeathBarUI heathBarUI;
    public HeathBarUI bossHeathBarUI;
    public List<Player> players;
    public List<Enemy> enemies = new List<Enemy>();

    /// <summary>
    /// public variable
    /// </summary>
    public float ScreenWidth;
    public float SreenHeight;
    public float SPAWN_COOLDOWN;
    public List<Enemy> enemyTemplates = new List<Enemy>();
    public Player playerTemplate;
    public Text debug;
    public Text debug2;

    /// <summary>
    /// private variable
    /// </summary>
    private float spawnPoint;


    private void Start()
    {
        //debug.text = (1 / Time.deltaTime).ToString();
        //Time.timeScale = 0.25f;
        Debug.Log("GameManager start");
        Instance = this;
        Screen.orientation = ScreenOrientation.Portrait;
        ScreenWidth = 20;
        SreenHeight = 10;



        float dt = Time.deltaTime;
        players.Add(Instantiate(playerTemplate, new Vector3(0, -5, 0), Quaternion.identity));
        enemies.AddRange(FindObjectsOfType<Enemy>());
        //SpawnEnemy(enemyTemplates[1], Vector3.left);
        //SpawnEnemy(enemyTemplates[0], Vector3.right + Vector3.up);
    }

    private void Update()
    {
    }

    void SpawnEnemy(Enemy origin, Vector3 position)
    {
        Enemy enemy = Instantiate(origin, position, Quaternion.identity);
        enemies.Add(enemy);

    }


    public Enemy GetClosestEnemy(Vector3 pos)
    {
        Enemy target = null;
        float magnitude = float.PositiveInfinity;
        foreach (Enemy e in enemies)
        {
            if (e.isActiveAndEnabled && magnitude > (e.transform.position - pos).magnitude)
            {
                magnitude = (e.transform.position - pos).magnitude;
                target = e;
            }
        }
        Debug.Log($"{enemies.Count}");
        return target;
    }
}
