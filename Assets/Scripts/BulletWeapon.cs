using UnityEngine;
using System.Collections;

public class BulletWeapon : MonoBehaviour
{
    public bool isAblityActive;
    Player player;
    [Header("# Bullet Damage")]
    public float Adamage;
    public float Bdamage;
    public float Cdamage;
    public float Ddamage;
    public float Edamage;
    [Header("# Prefab ID")]
    public int AprefabID;
    public int BprefabID;
    public int CprefabID;
    public int DprefabID;
    public int EprefabID;
    [Header("# Fire Delay")]
    public float maxfireDelay;
    public float fireDelay;
    [Header("# LV")]
    public int NoLv_power;
    public int lv1_Power;
    public int lv2_Power;
    [Header("# ETC")]
    public GameObject bubble;
    Animator bubbleAnim;
    bool isLv1Active = false;
    bool isLv2Active = false;


    void Awake()
    {
        bubbleAnim = bubble.GetComponent<Animator>();
        player = GetComponentInParent<Player>();
    }
    void Update()
    {
        fireDelay += Time.deltaTime;


        Fire();



        AblityFire();

        AblityCoroutine();
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && fireDelay > maxfireDelay && !player.isHit && isAblityActive == false)
        {
            if (lv1_Power >= 1 || lv2_Power >= 1)
            {
                AudioManager.instance.playSfx(AudioManager.Sfx.L12_Attack);
            }
            else
               AudioManager.instance.playSfx(AudioManager.Sfx.N_Attack);

            if (lv1_Power > 0 && !isLv1Active)
            {
              
                if (lv2_Power > 0)
                {
                    lv2_Power = 0;
                    isLv2Active = false;
                }
                isLv1Active = true;
            }
            else if (lv2_Power > 0 && !isLv2Active)
            {
             
                if (lv1_Power > 0)
                {
                    lv1_Power = 0;
                    isLv1Active = false;
                }
                isLv2Active = true;
            }

            switch (NoLv_power)
            {
                case 1:
                    maxfireDelay = 0.2f;
                    FireBullet(AprefabID, transform.position, Adamage, Vector2.up);
                    break;
                case 2:
                    maxfireDelay = 0.15f;
                    FireBullet(AprefabID, transform.position + Vector3.right * 0.1f, Adamage, Vector2.up);
                    FireBullet(AprefabID, transform.position + Vector3.left * 0.1f, Adamage, Vector2.up);
                    break;
                case 3:
                    maxfireDelay = 0.12f;
                    FireBullet(AprefabID, transform.position + Vector3.right * 0.2f, Adamage, Vector2.up);
                    FireBullet(AprefabID, transform.position + Vector3.left * 0.2f, Adamage, Vector2.up);
                    FireBullet(BprefabID, transform.position, Bdamage, Vector2.up);
                    break;
                case 4:
                    maxfireDelay = 0.11f;
                    FireBullet(BprefabID, transform.position + Vector3.right * 0.3f, Adamage, Vector2.up);
                    FireBullet(BprefabID, transform.position + Vector3.left * 0.3f, Adamage, Vector2.up);
                    FireBullet(AprefabID, transform.position + Vector3.right * 0.5f, Adamage, Vector2.up);
                    FireBullet(AprefabID, transform.position + Vector3.left * 0.5f, Adamage, Vector2.up);
                    FireBullet(BprefabID, transform.position, Bdamage, Vector2.up);
                    break;
            }

            switch (lv1_Power)
            {
                case 1:
                    lv2_Power = 0;
                    maxfireDelay = 0.2f;
                    int numberOfBullets = 3;
                    float spreadAngle = 10f;
                    float startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < numberOfBullets; i++)
                    {

                        float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);

                        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                        FireBullet(CprefabID, transform.position, Ddamage, direction);
                    }
                    break;
                case 2:
                 
                    maxfireDelay = 0.18f;
                    numberOfBullets = 5;
                    spreadAngle = 30f;
                    startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < numberOfBullets; i++)
                    {

                        float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);

                        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                        FireBullet(CprefabID, transform.position, Ddamage, direction);
                    }
                    break;
                case 3:
                

                    maxfireDelay = 0.15f;
                    numberOfBullets = 8;
                    spreadAngle = 35f;
                    startAngle = -spreadAngle / 2f;

                    for (int i = 0; i < numberOfBullets; i++)
                    {

                        float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);

                        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                        FireBullet(CprefabID, transform.position, Ddamage, direction);
                    }
                    break;
            }

            switch (lv2_Power)
            {
                case 1:
                    maxfireDelay = 0.15f;
                    int numberOfBullets = 3;
                    float bulletSpacing = 0.6f;
                    

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        Vector3 bulletPosition = transform.position + new Vector3((i - numberOfBullets / 2) * bulletSpacing, 0, 0);

                        Vector2 direction = Vector2.up;

                        FireBullet(DprefabID, bulletPosition, Edamage, direction);
                    }
                    break;
                case 2:
           
                    maxfireDelay = 0.13f;
                    numberOfBullets = 5;
                    bulletSpacing = 0.5f;
                   

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        Vector3 bulletPosition = transform.position + new Vector3((i - numberOfBullets / 2) * bulletSpacing, 0, 0);

                        Vector2 direction = Vector2.up;

                        FireBullet(DprefabID, bulletPosition, Edamage, direction);
                    }
                    break;
                case 3:
      
                    maxfireDelay = 0.11f;
                    numberOfBullets = 7;
                    bulletSpacing = 0.4f;
                  

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                        Vector3 bulletPosition = transform.position + new Vector3((i - numberOfBullets / 2) * bulletSpacing, 0, 0);

                        Vector2 direction = Vector2.up;

                        FireBullet(DprefabID, bulletPosition, Edamage, direction);
                    }
                    break;
            }

            bubbleAnim.SetTrigger("isFire");
            fireDelay = 0;

        }

    }

    void AblityFire()
    {
        if (Input.GetKey(KeyCode.Space) && fireDelay > maxfireDelay && isAblityActive == true)
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.A_Attack);
            maxfireDelay = 0.1f;
            int numberOfBullets = 15;
            float spreadAngle = 45f;
            float startAngle = -spreadAngle / 2f;

            for (int i = 0; i < numberOfBullets; i++)
            {

                float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);

                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

                FireBullet(EprefabID, transform.position, Cdamage, direction);
            }

            bubbleAnim.SetTrigger("isFire");
            fireDelay = 0;
        }
    }

    void AblityCoroutine()
    {
        if (Input.GetKeyDown(KeyCode.Q) && GameManager.instance.ablity == 100)
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.ablitySFx);
            AudioManager.instance.playSfx(AudioManager.Sfx.L12_Attack);
            AudioManager.instance.playSfx(AudioManager.Sfx.ablityOn);
            StartCoroutine(Ablity());
        }
    }

    IEnumerator Ablity()
    {
        GameManager.instance.Boom.gameObject.SetActive(true);
        GameManager.instance.player.barriar.SetActive(true);
        isAblityActive = true;
        player.isInviblity = true;
        GameManager.instance.BoomObject();

        float duration = 10f;
        float time = 0f;
        float initialAbility = GameManager.instance.ablity;

        yield return new WaitForSeconds(0.3f);

        GameManager.instance.Boom.gameObject.SetActive(false);

        while (time < duration)
        {
            GameManager.instance.ablity = (int)Mathf.Lerp(initialAbility, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GameManager.instance.ablity = 0;

        isAblityActive = false;
        player.isInviblity = false;
        player.isInviblityItem = false;
        GameManager.instance.player.barriar.SetActive(false);
        
    }

    void FireBullet(int prefabID, Vector3 position, float damage, Vector2 direction)
    {
        Transform bulletTransform = GameManager.instance.poolManager.Get(prefabID).transform;
        bulletTransform.position = position;
        bulletTransform.rotation = Quaternion.FromToRotation(Vector2.up, direction);
        Rigidbody2D rb = bulletTransform.GetComponent<Rigidbody2D>();
        rb.velocity = direction * 10;
        bulletTransform.GetComponent<Bullet>().damage = damage;
    }


}
