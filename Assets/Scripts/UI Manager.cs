using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIType { Exp, Level, Kill, Time, Health, Ablity, Score, HUD , BossHealth,}
    public UIType Type;

    Text scoreText;
    Text levelText;
    Text killText;
    Text healthText;
    Text ablityText;
    Slider ablitySlider;
    Slider expSlider;
    Slider healthSlider;
    Slider bossHealthSlider;
    Text HudText;
    public GameObject[] HudImage;
  
    void Awake()
    {
        healthText = GetComponentInChildren<Text>();
        scoreText = GetComponent<Text>();
        levelText = GetComponent<Text>();
        expSlider = GetComponent<Slider>();
        healthSlider = GetComponent<Slider>();
        killText = GetComponent<Text>();
        ablitySlider = GetComponent<Slider>();
        ablityText = GetComponentInChildren<Text>();
        bossHealthSlider = GetComponent<Slider>();
        // HudImage = GetComponents<Image>();
        HudText =  GetComponentInChildren<Text>();   
    }

    void LateUpdate()
    {
        switch (Type)
        {
            case UIType.Exp:
                
                float curExp = GameManager.instance.exp;
                if (GameManager.instance.level >= GameManager.instance.nextExp.Length)
                    return;
                

                float maxExp = GameManager.instance.nextExp[GameManager.instance.level];
                if (GameManager.instance.level >= GameManager.instance.nextExp.Length - 1)
                {
                    expSlider.value = 1;
                }
                else
                    expSlider.value = curExp / maxExp;
                
                break;
            case UIType.Level:
                if (GameManager.instance.level >= GameManager.instance.nextExp.Length - 1)
                {
                    levelText.text = string.Format("Lv.MAX");
                }
                else
                    levelText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;
            case UIType.Kill:
                killText.text = string.Format("Kill.{0:F0}", GameManager.instance.kill);
                break;
            case UIType.Time:

                break;
            case UIType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxhealth;

                healthSlider.value = curHealth / maxHealth;
                healthText.text = Mathf.Round(GameManager.instance.health).ToString();
                break;
            case UIType.Ablity:
                ablitySlider.value = GameManager.instance.ablity;
                ablityText.text = string.Format("{0:F0}", GameManager.instance.ablity + "%");
                
                break;
            case UIType.Score:
                scoreText.text = string.Format("Score.{0:F0}", GameManager.instance.score);
                break;
            case UIType.HUD:
                if (GameManager.instance.player.weapon.NoLv_power > 0)
                {
                    HudImage[0].SetActive(true);
                    HudText.text = "WeaponNormal";
                }
                else
                {
                    HudImage[0].SetActive(false);
                }

             
                if (GameManager.instance.player.weapon.lv1_Power > 0)
                {
                    HudImage[0].SetActive(false);
                    HudImage[1].SetActive(true);
                    HudText.text = "WeaponOne";
                }
                else
                {
                    HudImage[1].SetActive(false);
                }

                
                if (GameManager.instance.player.weapon.lv2_Power > 0)
                {
                    HudImage[0].SetActive(false);
                    HudImage[2].SetActive(true);
                    HudText.text = "WeaponTwo";
                }
                else
                {
                    HudImage[2].SetActive(false);
                }
                if (GameManager.instance.player.weapon.isAblityActive == true)
                {
                    HudImage[3].SetActive(true);
                    HudImage[0].SetActive(false);
                    HudImage[1].SetActive(false);
                    HudImage[2].SetActive(false);
                    HudText.text = "WeaponAblity";
                }
                else
                    HudImage[3].SetActive(false);


                break;
            case UIType.BossHealth:

                bossHealthSlider.value = GameManager.instance.boss.health;
                break;
        }
    }
}
