using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private Image fadeInOut;
    [SerializeField]
    private Animator fadeAnim;

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
        if (players[0].GetComponent<Destructible>().HP <= 0)
        {
            GameOver();
        }
    }

    private IEnumerator Fade()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.5f);
    }

    public void GameOver()
    {
        fadeInOut.gameObject.SetActive(true);
        StartCoroutine(Fade());
        SceneManager.LoadScene("Mainmenu");
    }

    private void RePlay()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Quit()
    {
        SceneManager.LoadScene("Mainmenu");
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
