using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private float speed;
    [SerializeField] private Image[] healthImg;

    private int healthCount=5;

    private Vector3 firstPos;

    private PlayerController playerCntrller;

    public bool isGameStart;

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] PowerUps;
    [SerializeField] private GameObject point;
    [SerializeField] private Vector3 spawnPos;

    [SerializeField] GameObject menuUI;
    [SerializeField] GameObject restartUI;

    [SerializeField] Slider musicVolume;
    [SerializeField] AudioSource music;

    private bool isPause;
    [SerializeField] Image pauseImage;
    void Start()
    {
        firstPos = plane.transform.position;
        playerCntrller = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void StartGame(int spawnRate)
    {
        isGameStart = true;
        menuUI.SetActive(false);

        InvokeRepeating("EnemySpawn", 2, spawnRate);
        InvokeRepeating("PowerUpSpawn", 2, 15);
        InvokeRepeating("PointSpawn", 2, 3);
    }
    void EnemySpawn()
    {
        int randomEnemy = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomEnemy], new Vector3(Random.Range(-7, 8), spawnPos.y, spawnPos.z), enemies[randomEnemy].transform.rotation);
    }
    void PowerUpSpawn()
    {
        int randomEnemy = Random.Range(0, PowerUps.Length);
        Instantiate(PowerUps[randomEnemy], new Vector3(Random.Range(-7, 8), spawnPos.y, spawnPos.z), PowerUps[randomEnemy].transform.rotation);
    }
    void PointSpawn()
    {
        Instantiate(point, new Vector3(Random.Range(-7, 8), spawnPos.y, spawnPos.z), point.transform.rotation);
    }
    void Update()
    {
        if (isGameStart)
        {
            plane.transform.Translate(Vector3.forward * -speed);
            if (plane.transform.position.z < -50)
            {
                plane.transform.position = firstPos;
            }
            PauseGame();
        }

        MusicVolume();
 
    }
    public void Health()
    {
        if (healthCount>0)
        {
            healthCount--;
            healthImg[healthCount].gameObject.SetActive(false);
        }
        if (healthCount==0)
        {
            playerCntrller.GameOver();

            CancelInvoke("EnemySpawn");
            CancelInvoke("PowerUpSpawn");
            CancelInvoke("PointSpawn");
            restartUI.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void MusicVolume()
    {
        music.volume = musicVolume.value;
    }
    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isPause)
        {
            pauseImage.gameObject.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
            speed = 0;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPause)
        {
            pauseImage.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
            speed = 0.08f;
        }
    }
}
