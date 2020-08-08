using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelhandler : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject EndPanel;
    public GameObject WinPanel;

    public GameObject Canvas;
    public GameObject TonkPlayer;

    private bool Running;
    public List<GameObject> enemies;
    public int enemycount;
    public GameObject enemyprefab;
    public Menu menu;
    public int Xlenght;
    public int Zlenght;

    private GameObject PauseResumeButton;
    private GameObject PauseSettingsButton;
    private GameObject PauseToMenuButton;

    private GameObject DeathPanelRetry;
    private GameObject DeathPanelMenu;
    void Start()
    {
        AssignEmptys();
        AssignButtons();
        enemies = new List<GameObject>();
        TonkPlayer = GameObject.Find("Player").gameObject;
        Running = true;
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
        EndPanel.SetActive(false);
        WinPanel.SetActive(false);
        for (int i = 0; i < enemycount; i++)
        {
            //Vector3 pos = new Vector3(Random.Range(Xlenght, Zlenght) * (i+1), 5, Random.Range(Xlenght, Zlenght) * (i+1));
            GameObject enemy = Instantiate(enemyprefab, new Vector3(0 , 5 + i,10), Quaternion.identity);
            enemies.Add(enemy);
        }
    }
    private void AssignEmptys()
    {
        Canvas = GameObject.Find("Canvas");
        PauseMenu = Canvas.transform.Find("Pause").gameObject;
        EndPanel = Canvas.transform.Find("DeathScreen").gameObject;
        WinPanel = Canvas.transform.Find("win").gameObject;
    }
    private void AssignButtons()
    {
        //PausePanel buttons
        PauseResumeButton = PauseMenu.transform.GetChild(0).Find("Resume").gameObject;
        PauseResumeButton.GetComponent<Button>().onClick.AddListener(Resume);

        PauseSettingsButton = PauseMenu.transform.GetChild(0).Find("settings").gameObject;
        PauseSettingsButton.GetComponent<Button>().onClick.AddListener(Settings);

        PauseToMenuButton = PauseMenu.transform.GetChild(0).Find("ToMenu").gameObject;
        PauseToMenuButton.GetComponent<Button>().onClick.AddListener(ReturnToMenu);

        //DeathPanelButtons
        DeathPanelRetry = EndPanel.transform.Find("rettry").gameObject;
        DeathPanelRetry.GetComponent<Button>().onClick.AddListener(Retry);

        DeathPanelMenu = EndPanel.transform.Find("exit").gameObject;
        DeathPanelMenu.GetComponent<Button>().onClick.AddListener(ReturnToMenu);
    }
    void Update()
    {
        if (Running)
        {
            if(TonkPlayer.GetComponent<Healt>()!=null)
                if (TonkPlayer.GetComponent<Healt>().healt <= 0)
            {
                Death();
            }
            if (TonkPlayer.transform.position.y < 0)
            {
                Death();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.Remove(enemies[i]);
            }
        }
        if (enemies.Count == 0)
        {
            WinPanel.SetActive(true);
            Invoke("EndGame",5f);
        }
    }
    void EndGame()
    {
        Menu.SetUnlockedLevelValue(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
    void Death()
    {
        //Time.timeScale = 0;
        Running = false;
        TonkPlayer.GetComponent<Tonk>().SetRunState(false);
        EndPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void Settings()
    {

    }
    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
}
