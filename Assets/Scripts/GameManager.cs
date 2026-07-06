using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("# Header")]
    public static GameManager instance;
    public Player player;

    [Header("# Score")]
    public int score;
    public int maxScore;

    [Header("# Info")]
    public float health;
    public float maxhealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int ablity;
    public bool isLive;
    public int[] nextExp;

    public Boss boss;

    [Header("# Pooling")]
    public PoolManager poolManager;

    [Header("# Item")]
    public GameObject Boom;

    [Header("# UI")]
    public GameObject GamePause;
    public bool isPaused = false;
    public GameObject GameOver;
    public Text ExpInfoText;

    public void GameStart()
    {
        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.playSfx(AudioManager.Sfx.GameStart);
        AudioManager.instance.playSfx(AudioManager.Sfx.Load);
        health = maxhealth;
        player.gameObject.SetActive(true);
        ablity = 0;
        Time.timeScale = 1;
    }

    void Awake()
    {
        Time.timeScale = 0;
        instance = this;
    }

    void Update()
    {

        ExpInfo();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.barriar);
            player.isInviblity = !player.isInviblity;
            player.barriar.SetActive(!player.barriar.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.F2) && level <= nextExp.Length - 2)
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.levelUp);
            level++;
            exp = 0;

                maxhealth = Mathf.Round(maxhealth * 1.1f);
                player.weapon.Adamage = Mathf.Round(player.weapon.Adamage * 1.1f);
                player.weapon.Bdamage = Mathf.Round(player.weapon.Bdamage * 1.1f);
                player.weapon.Cdamage = Mathf.Round(player.weapon.Cdamage * 1.1f);
                player.weapon.Ddamage = Mathf.Round(player.weapon.Ddamage * 1.1f);
                player.weapon.Edamage = Mathf.Round(player.weapon.Edamage * 1.1f);
                health = maxhealth;



                switch (level)
            {
                case 5:
                    player.weapon.NoLv_power++;
                    break;
                case 10:
                    player.weapon.NoLv_power++;
                    break;
                case 15:
                    player.weapon.NoLv_power++;
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.F3) && level > 1)
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.levelDown);
            level--;
                exp = 0;

            maxhealth = Mathf.Round(maxhealth * 0.9f);
            player.weapon.Adamage = Mathf.Round(player.weapon.Adamage * 0.9f);
            player.weapon.Bdamage = Mathf.Round(player.weapon.Bdamage * 0.9f);
            player.weapon.Cdamage = Mathf.Round(player.weapon.Cdamage * 0.9f);
            player.weapon.Ddamage = Mathf.Round(player.weapon.Ddamage * 0.9f);
            player.weapon.Edamage = Mathf.Round(player.weapon.Edamage * 0.9f);
            health = maxhealth;

            switch (level)
            {
                case 5:
                    player.weapon.NoLv_power--;
                    break;
                case 10:
                    player.weapon.NoLv_power--;
                    break;
                case 15:
                    player.weapon.NoLv_power--;
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            score += 1000;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;

            GamePause.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;

        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            health = maxhealth;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Drop_WItem();
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Drop_NItem();
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            bool isActive = ExpInfoText.enabled;
            ExpInfoText.enabled = !isActive;
            if (!isActive)
            {
                ExpInfo();
            }
        }

        if (Input.GetKey(KeyCode.E) && ablity < 100)
        {
            ablity++;
        }

        if (score >= 10000)
        {
            boss.bossIsActive = true;
            boss.gameObject.SetActive(true);

        }

    }

    void ExpInfo()
    {
        if (level >= nextExp.Length)
            return; 
        ExpInfoText.text = $"exp: {exp}\nlevel: {level}\nnextExp: {nextExp[level]}";
    }

    void LateUpdate()
    {
        LevelUpSkin();
    }

    void LevelUpSkin()
    {
        switch (level)
        {
            case 1:
                player.levelSKin[0].SetActive(true);
                break;
            case 5:
                player.levelSKin[1].SetActive(true);
                break;
            case 10:
                player.levelSKin[2].SetActive(true);
                break;
            case 15:
                player.levelSKin[3].SetActive(true);
                break;
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        if (score > maxScore)
        {
            maxScore = score;
        }
    }

    public void GetExp(int expAdd)
    {
        exp += expAdd;

        if (level >= nextExp.Length)
            return; 
        

        if (exp >= nextExp[level])
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.levelUp);
            level++;
            exp = 0;

            maxhealth = Mathf.Round(maxhealth * 1.1f);
            player.weapon.Adamage = Mathf.Round(player.weapon.Adamage * 1.1f);
            player.weapon.Bdamage = Mathf.Round(player.weapon.Bdamage * 1.1f);
            player.weapon.Cdamage = Mathf.Round(player.weapon.Cdamage * 1.1f);
            player.weapon.Ddamage = Mathf.Round(player.weapon.Ddamage * 1.1f);
            player.weapon.Edamage = Mathf.Round(player.weapon.Edamage * 1.1f);
            health = maxhealth;
            switch (level)
            {
                case 5:
                    player.weapon.NoLv_power++;
                    break;
                case 10:
                    player.weapon.NoLv_power++;
                    break;
                case 15:
                    player.weapon.NoLv_power++;
                    break;
            }
        }
    }

    public void BoomObject()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        // "Enemy Bullet" 태그를 가진 모든 오브젝트 비활성화
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("Enemy Bullet");
        foreach (GameObject enemyBullet in enemyBullets)
        {
            enemyBullet.SetActive(false);
        }
    }

    void Drop_NItem()
    {
            GameObject eventBall = GameManager.instance.poolManager.Get(Random.Range(8, 11));
            eventBall.transform.position = ScreenPosition();
    }

    void Drop_WItem()
    {
        GameObject eventBall = GameManager.instance.poolManager.Get(Random.Range(6, 8));
        eventBall.transform.position = ScreenPosition();
    }


    Vector3 ScreenPosition()
{

    Camera cam = Camera.main;
    Vector3 screenBottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
    Vector3 screenTopRight = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));


    float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
    float randomY = Random.Range(screenBottomLeft.y, screenTopRight.y);

    return new Vector3(randomX, randomY, 0);
}

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("0");
    }

}
