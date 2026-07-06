using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float spawnTime;
    public float speed;
    public float maxSpeed;
    public float health;
    public float maxHealth;
    public int damage;
    public int maxDamage;
    public int score;
    public int exp;
    public int level;
    public float bulletSpeed;
    public float maxBulletSpeed;
    public float maxFireDelay;
    public float fireDelay;
    public int bulletPrefabID;

    public RuntimeAnimatorController[] controller;
    bool isLive;

    SpriteRenderer sp;
    Rigidbody2D rb;
    Animator anim;
    CapsuleCollider2D cc;

    float sfxDelay = 0;
    int spawnPointIndex;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    void SetLevel(int currentLv)
    {
        level = currentLv;
    }

    void OnEnable()
    {
        isLive = true;
        // health = maxHealth;
        cc.enabled = true;
        anim.SetBool("isDie", false);
        SetLevel(level);
        Invoke("StopMovement", Random.Range(0.9f, 1.3f));
        Invoke("ReStartMovement", 3f);
        fireDelay = 0; // 초기화 추가
                       UpdateStat();
    }

    void Spawn(int enemyLevel)
    {
        switch (enemyLevel)
        {
            case 0:
                float currentTime = Time.time;

                if (currentTime - sfxDelay >= 3f)
                {
                    AudioManager.instance.playSfx(AudioManager.Sfx.enemyAttack); ;
                    sfxDelay = currentTime;
                }
                SpawnBullet(transform.position, Vector2.down * Spawner.instance.spawnDate[enemyLevel].bulletSpeed);
               // Debug.Log("0번 스킬발동");
                break;
            case 1:
                currentTime = Time.time;

                if (currentTime - sfxDelay >= 3f)
                {
                    AudioManager.instance.playSfx(AudioManager.Sfx.enemyAttack); ;
                    sfxDelay = currentTime;
                }

                int numberOfBullets = 3; 
                float spreadAngle = 60f; 
                float startAngle = transform.eulerAngles.z - (spreadAngle / 2f); 

                for (int i = 0; i < numberOfBullets; i++)
                {
                    
                    float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);
                    Quaternion rotation = Quaternion.Euler(0, 0, angle);
                    Vector2 bulletVelocity = rotation * Vector2.down * Spawner.instance.spawnDate[enemyLevel].bulletSpeed;

                    SpawnBullet(transform.position, bulletVelocity, angle);
                }
                break;
            case 4:
                if (speed == 0)
                {
                    
                    numberOfBullets = 6; 
                    spreadAngle = 55f; 
                    startAngle = transform.eulerAngles.z - (spreadAngle / 2f); 

                    for (int i = 0; i < numberOfBullets; i++)
                    {
                       
                        float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);
                        Quaternion rotation = Quaternion.Euler(0, 0, angle);
                        Vector2 bulletVelocity = rotation * Vector2.down * Spawner.instance.spawnDate[level].bulletSpeed;

                        SpawnBullet(transform.position, bulletVelocity, angle);
                    }
                    currentTime = Time.time;

                    if (currentTime - sfxDelay >= 3f)
                    {
                        AudioManager.instance.playSfx(AudioManager.Sfx.enemyAttack); ;
                        sfxDelay = currentTime;
                    }

                }

          
                break;
        }
    }

    void SpawnBullet(Vector3 position, Vector2 velocity, float angle = 0)
    {
        GameObject bulletObj = GameManager.instance.poolManager.Get(bulletPrefabID);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

   
        bullet.Init(damage);
        bulletObj.transform.position = position;
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        rb.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
        if (angle != 0)
        {
            rb.rotation = angle; 
        }
    }

    void UpdateStat()
    {
        int score = GameManager.instance.score;

     
        if (score >= 100 && score < 200)
        {
            maxDamage = (int)(maxDamage * 1.1f);
            maxHealth = (int)(maxHealth * 1.2f);
            
        }
        else if (score >= 200 && score < 300)
        {
            maxDamage = (int)(maxDamage * 1.2f);
            maxHealth = (int)(maxHealth * 1.5f);
            
        }
        else if (score >= 300 && score < 1000)
        {
            maxDamage = (int)(maxDamage * 1.3f);
            maxHealth = (int)(maxHealth * 2f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.3f);
        }
        else if (score >= 1000 && score < 2000)
        {
            maxDamage = (int)(maxDamage * 1.4f);
            maxHealth = (int)(maxHealth * 2.5f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.4f);
            speed = (int)(speed * 1.5);
        }
        else if (score >= 2000 && score < 3000)
        {
            maxDamage = (int)(maxDamage * 1.5f);
            maxHealth = (int)(maxHealth * 9f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.5f);
            speed = (int)(speed * 2);
        }
        else if (score >= 3000 && score < 4000)
        {
            maxDamage = (int)(maxDamage * 1.6f);
            maxHealth = (int)(maxHealth * 10f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.6f);
            speed = (int)(speed * 2.5);
        }
        else if (score >= 4000 && score < 5000)
        {
            maxDamage = (int)(maxDamage * 2f);
            maxHealth = (int)(maxHealth * 15f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.7f);
            speed = (int)(speed * 3);
        }
        else if (score >= 5000 && score < 6000)
        {
            maxDamage = (int)(maxDamage * 4f);
            maxHealth = (int)(maxHealth * 20f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.8f);
            speed = (int)(speed * 3.5);
        }
        else if (score >= 6000 && score < 7000)
        {
            maxDamage = (int)(maxDamage * 6f);
            maxHealth = (int)(maxHealth * 30f);
            maxBulletSpeed = (int)(maxBulletSpeed * 1.9f);
            speed = (int)(speed * 3.5);
        }
        else if (score >= 7000 && score < 8000)
        {
            maxDamage = (int)(maxDamage * 8f);
            maxHealth = (int)(maxHealth * 40f);
            maxBulletSpeed = (int)(maxBulletSpeed * 2f);
        }
        else if (score >= 8000 && score < 9000)
        {
            maxDamage = (int)(maxDamage * 10f);
            maxHealth = (int)(maxHealth * 50f);
            maxBulletSpeed = (int)(maxBulletSpeed * 2.1f);
        }


        damage = maxDamage;
        bulletSpeed = maxBulletSpeed;
        speed = maxSpeed;
        health = maxHealth; 
    }

    void Update()
    {
        if (isLive)
        {
            fireDelay += Time.deltaTime;

            if (fireDelay > maxFireDelay)
            {
                Spawn(level);
                fireDelay = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (isLive)
        {
            Vector2 movementDirection = Vector2.down * speed * Time.fixedDeltaTime;

           
            if (level == 1 && spawnPointIndex >= 0)
            {
                if (spawnPointIndex == 7)
                {
                    movementDirection = Vector2.right * speed * Time.fixedDeltaTime;
                }
                else if (spawnPointIndex == 8)
                {
                    movementDirection = Vector2.left * speed * Time.fixedDeltaTime;
                }
            }

            rb.MovePosition(rb.position + movementDirection);
        }
    }

    void StopMovement()
    {
        switch (level)
        {
            case 2:
                speed = 0;
                break;
            case 3:
                speed = 0;
                break;
            case 4:
                speed = 0;
                break;
        }
    }

    void ReStartMovement()
    {
        switch (level)
        {
            case 2:
                speed = 20;
                break;
            case 4:
                speed = 15;
                break;
        }
    }

    void Ondamaged(float damage)
    {
        if (!isLive)
            return;

        float currentTime = Time.time;

        if (currentTime - sfxDelay >= 3f) 
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.EnemyDamage);
            sfxDelay = currentTime;
        }
        health -= damage;
        sp.color = new Color(0, 0, 0, 0.5f);
        Invoke("ReColor", 0.1f);



        if (health <= 0)
        {
            OnDead();
        }
    }

    void OnDead()
    {
        AudioManager.instance.playSfx(AudioManager.Sfx.EnemyDead);
        isLive = false;
        anim.SetBool("isDie", true);
        cc.enabled = false;
        Invoke("IsDead", 0.7f);
        GameManager.instance.IncreaseScore(score);
        GameManager.instance.kill++;
        GameManager.instance.GetExp(exp);
        //Debug.Log("뒤짐");
        EnemyLv3();
        DropItem();
        if (GameManager.instance.ablity < 100 && !GameManager.instance.player.weapon.isAblityActive)
        {
            GameManager.instance.ablity++;
        }
    }

    void DropItem()
    {
  
        float ran = Random.Range(0f, 1f);


        if (ran < 0.3f)
        {
     
            GameObject item = GameManager.instance.poolManager.Get(Random.Range(8, 11));
            item.transform.position = transform.position;
        }
 
        else if (ran < 0.4f && GameManager.instance.level >= 10)
        {
              
                GameObject item = GameManager.instance.poolManager.Get(Random.Range(6,8));
                item.transform.position = transform.position;
          
        }
      
        else
        {
            
            print("꽝!");
        }
    }


    void EnemyLv3()
    {
        if (level == 3 && !isLive)
        {
            float currentTime = Time.time;

            if (currentTime - sfxDelay >= 3f)
            {
                AudioManager.instance.playSfx(AudioManager.Sfx.enemyAttack); ;
                sfxDelay = currentTime;
            }
           
            int numberOfBullets4 = 20; 
            float bulletSpeed4 = Spawner.instance.spawnDate[level].bulletSpeed;


            for (int i = 0; i < numberOfBullets4; i++)
            {
              
                float angleRadians4 = Mathf.PI * 2 * i / numberOfBullets4;
                Vector2 dirVec4 = new Vector2(Mathf.Cos(angleRadians4), Mathf.Sin(angleRadians4)).normalized;

         

                GameObject bulletObj = GameManager.instance.poolManager.Get(bulletPrefabID);
                Bullet bullet = bulletObj.GetComponent<Bullet>();
                bullet.Init(damage);
                bulletObj.transform.position = transform.position;
                Rigidbody2D rb4 = bulletObj.GetComponent<Rigidbody2D>();
                rb4.AddForce(dirVec4 * bulletSpeed4, ForceMode2D.Impulse);

     
                Vector3 rotVec4 = Vector3.forward * (360f * i / numberOfBullets4 + 90f);
                rb4.transform.rotation = Quaternion.Euler(rotVec4);
            }
        }
    }

    void IsDead()
    {
        gameObject.SetActive(false);
    }

    void ReColor()
    {
        sp.color = new Color(1, 0.3f, 0.3f,1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") && isLive)
        {
            // PlayerBullet과 충돌했을 때의 처리
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            Ondamaged(bullet.damage);

            if (GameManager.instance.player.weapon.lv2_Power >= 1)
                return;

            collision.gameObject.SetActive(false);

        }

        if (collision.gameObject.tag == "Enemy Boder")
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(spawnDate date, int spawnPointIndex)
    {
        anim.runtimeAnimatorController = controller[date.enemyType];
        spawnTime = date.spawnTime;
        speed = date.speed;
        maxSpeed = date.speed;
        damage = maxDamage;
        maxDamage = date.damage;
        health = date.health;
        maxHealth = date.health;
        score = date.score;
        exp = date.exp;
        level = date.enemyType;
        bulletSpeed = date.bulletSpeed;
        maxBulletSpeed = date.bulletSpeed;
        maxFireDelay = date.maxFireDelay;
        this.spawnPointIndex = spawnPointIndex;
        UpdateStat();
    }
}
