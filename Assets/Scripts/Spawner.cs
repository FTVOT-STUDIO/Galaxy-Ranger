using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public Transform[] spawnPoint;
    public spawnDate[] spawnDate;
    public int level;

    private float[] timers;

    void Awake()
    {
        instance = this;
        spawnPoint = GetComponentsInChildren<Transform>();

        // 각 레벨별로 타이머를 초기화
        timers = new float[spawnDate.Length];
    }

    void Update()
    {
        level = Mathf.Min(GameManager.instance.score / 1000, spawnDate.Length - 1);

        for (int i = 0; i <= level; i++)
        {
            timers[i] += Time.deltaTime;

          
            if (timers[i] > spawnDate[i].spawnTime)
            {
                timers[i] = 0;
                Spawn(i);
            }
        }
    }

    void Spawn(int enemyLevel)
    {
        if (GameManager.instance.boss.bossIsActive == true)
            return;
        GameObject enemy = GameManager.instance.poolManager.Get(0);

        

        if (enemyLevel == 1)
        {
   
            int spawnIndex = Random.Range(7, 9);
            enemy.transform.position = spawnPoint[spawnIndex].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[1], spawnIndex); 
        }
        else
        {
     
            int spawnIndex = Random.Range(1, 7);
            enemy.transform.position = spawnPoint[spawnIndex].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[enemyLevel], spawnIndex); 
        }

        
    }

    public void SetLevel(spawnDate date)
    {
        level = date.enemyType;
    }
}

[System.Serializable]
public class spawnDate
{
    [Header("# 적 레벨")] public int enemyType;
    [Header("# 스폰 주기")] public float spawnTime;
    [Header("<==========================>")]
    [Header("# 체력")] public int health;
    [Header("# 데미지")] public int damage;
    [Header("<==========================>")]
    [Header("# 점수")] public int score;
    [Header("# 경험치")] public int exp;
    [Header("<==========================>")]
    [Header("# 탄환 속도")] public float bulletSpeed;
    [Header("# 발사 속도")] public float maxFireDelay;
    [Header("# 이동 속도")] public float speed;
}
