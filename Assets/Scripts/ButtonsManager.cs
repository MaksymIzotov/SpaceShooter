using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonsManager : MonoBehaviour
{
    public static ButtonsManager Instance;

    int currentLevel = 1;
    int lastLevelUnlocked;
    [SerializeField] TMP_Text text;

    [SerializeField]
    GameObject playerPrefab, spawnPoint;

    private void Awake()
    {
        Instance = this;
        lastLevelUnlocked = 1;
    }

    public void UpdateLastLevel() => lastLevelUnlocked = SaveLoad.Instance.obj[SaveLoad.Instance.obj.Count - 1].levelsUnlocked;


    public void StartGame()
    {
        MenuManager.Instance.CloseMenu("levels");
        Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        GetComponent<EnemyController>().currentLevel = currentLevel;
        GetComponent<EnemyController>().enabled = true;
    }

    public void NextLevel()
    {
        if (lastLevelUnlocked == currentLevel)
            return;

        currentLevel++;
        UpdateLevel();
    }

    public void PrevLevel()
    {
        if (currentLevel == 1)
            return;

        currentLevel--;
        UpdateLevel();
    }

    void UpdateLevel()
    {
        text.text = "Level: " + currentLevel;
    }

    public void GameOver()
    {
        MenuManager.Instance.OpenMenu("main");
        GetComponent<EnemyController>().enabled = false;

        Destroy(GameObject.FindGameObjectWithTag("Player"));

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject n in enemies)
            Destroy(n);
    }

    public void LevelWon(int levelPassed)
    {
        if(levelPassed == lastLevelUnlocked)
        {
            lastLevelUnlocked++;
            SaveLoad.Instance.obj.Add(new SaveVariables(0, lastLevelUnlocked));
            SaveLoad.Instance.Save(SaveLoad.Instance.obj);
        }
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        MenuManager.Instance.OpenMenu("main");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
