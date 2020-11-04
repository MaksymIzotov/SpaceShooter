using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] SaveLoad SL;

    [SerializeField] GameObject[] enemyPrefab;
    [SerializeField] float minEnemySpeed, maxEnemySpeed;
    [SerializeField] float minTimeToNext, maxTimeToNext;
    [SerializeField] int minEnemiesDefault, maxEnemiesDefault;
    public int enemiesAmount;
    public int currentLevel = 1;
    Transform leftSpawnBoundary, rightSpawnBoundary;

    bool isFinished;


    void Awake()
    {
        leftSpawnBoundary = GameObject.Find("LB").transform;
        rightSpawnBoundary = GameObject.Find("RB").transform;
    }

    private void SetDefault(bool isRewriting)
    {
        enemiesAmount = Random.Range(minEnemiesDefault * currentLevel, maxEnemiesDefault * currentLevel);

        if (isRewriting)
            SaveLoad.Instance.obj[SaveLoad.Instance.obj.Count - 1] = new SaveVariables(enemiesAmount, currentLevel);
        else
            SaveLoad.Instance.obj.Add(new SaveVariables(enemiesAmount, currentLevel));

        SaveLoad.Instance.Save(SaveLoad.Instance.obj);
    }

    private void LoadValues()
    {
        foreach(SaveVariables n in SaveLoad.Instance.obj)
        {
            if (n.levelsUnlocked == currentLevel)
            {
                if (n.enemiesAmount == 0)
                {
                    SetDefault(true);
                    return;
                }

                enemiesAmount = n.enemiesAmount;
                return;
            }
        }
        SetDefault(false);
    }

    private void Update()
    {
        if (enabled)
        {
            if (isFinished)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                Debug.Log(enemies);
                if (enemies.Length < 1)
                    ButtonsManager.Instance.LevelWon(currentLevel);
            }
        }
    }

    public IEnumerator SpawnEnemies()
    {
        for(int i = 0; i < enemiesAmount; i++)
        {
            if (i == enemiesAmount - 1)
                isFinished = true;
            //Randomizing spawn position
            Vector3 pos = new Vector3(Random.Range(leftSpawnBoundary.position.x, rightSpawnBoundary.position.x), 0f, leftSpawnBoundary.position.z);
            int enemyIndex = Random.Range(0, enemyPrefab.Length);
            
            //Randomizing speed of asteroid
            float speed = Random.Range(minEnemySpeed, maxEnemySpeed);

            GameObject enemy = Instantiate(enemyPrefab[enemyIndex], pos, Quaternion.identity);

            //Assign speed
            enemy.GetComponent<AsteroidMovement>().speed = speed;

            float timeToNext = Random.Range(minTimeToNext / DelayFormula, maxTimeToNext / DelayFormula);
            yield return new WaitForSeconds(timeToNext);
        }
    }

    private float DelayFormula => currentLevel * 0.3f;

    private void OnEnable()
    {
        isFinished = false;
        if (SaveLoad.Instance.isExist)
            LoadValues();
        else
            SetDefault(false);
        StartCoroutine("SpawnEnemies");
    }

    private void OnDisable()
    {
        StopCoroutine("SpawnEnemies");
    }
}
