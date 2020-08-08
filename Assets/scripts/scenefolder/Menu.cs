using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] List<GameObject> levelButtons = new List<GameObject>();
    [SerializeField] List<GameObject> unlockedLevelButton = new List<GameObject>();
    public GameObject tonkA;
    public GameObject tonkB;
    public GameObject menu;
    public GameObject levels;
    public static int UnlockedLevelValue;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        BackToStart();
        Debug.Log(UnlockedLevelValue);
        if (UnlockedLevelValue >= unlockedLevelButton.Count) { UnlockNextLevel(UnlockedLevelValue); }
    }
    public void LoadLevels()
    {
        tonkA.SetActive(false);
        tonkB.SetActive(false);

        menu.SetActive(false);
        levels.SetActive(true);
    }
    public void BackToStart()
    {
        tonkA.SetActive(true);
        tonkB.SetActive(true);
        menu.SetActive(true);
        levels.SetActive(false);
    }
    public void Loadlevel(int level)
    { if (level <= levelButtons.Count) { SceneManager.LoadScene(level); } else { Debug.Log("niks"); } }
    public static void SetUnlockedLevelValue(int level)
    {
        UnlockedLevelValue = level;
    }
    public void UnlockNextLevel(int level)
    {

        if (unlockedLevelButton.Count == level)
        {

            if (levelButtons.Count >= level)
            {
                unlockedLevelButton.Add(levelButtons[level]);
                unlockedLevelButton[unlockedLevelButton.Count - 1].SetActive(true);
            }
        }
    }

}
