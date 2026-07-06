using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
   
    public float health;
    float fireDelay;
    float maxfireDelay;

    bool isBarriar;

    public bool bossIsActive;

    public int bulletPrefabID;
    public int bulletSpeed;
    public float damage;
    public GameObject barriarObj;
    public GameObject[] eventBall;
    Animator[] childAnims;
    Rigidbody2D rb;
    SpriteRenderer sp;
    Animator anim;
    float sfxDelay = 0;
    public bool isLive = false;

    public GameObject healthBar;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        childAnims = GetComponentsInChildren<Animator>();
    }

    void OnEnable()
    {
        bossIsActive = bossIsActive = true;
        AudioManager.instance.playSfx(AudioManager.Sfx.Load);
        AudioManager.instance.playSfx(AudioManager.Sfx.ablitySFx);
        isBarriar = true;
        rb.velocity = Vector2.down * 3;
        Invoke("Stop", 1.8f);
        isLive = true;
        BoomObject();
        healthBar.SetActive(true);
        StartCoroutine(AttackPattern());
    }

    void Update()
    {
        if (isLive)
        {
            fireDelay += Time.deltaTime;
        }
    }

    IEnumerator AttackPattern()
    {
        while (isLive)
        {
            if (rb.velocity == Vector2.zero)
            {
               
                int patternIndex = Random.Range(0, 5); // 공격 패턴 개수
                int repeatCount = Random.Range(1, 6);  // 동일 공격 실행 횟수

                for (int i = 0; i < repeatCount; i++)
                {
                    switch (patternIndex)
                    {
                        case 0:
                            FireTarget();
                            break;
                        case 1:
                            FireCircle();
                            break;
                        case 2:
                            FireShotGun();
                            break;
                        case 3:
                            StartCoroutine(FireEvent());
                            break;
                        case 4:
                            BarriarEvnet();
                            break;
                    }

                    yield return new WaitForSeconds(Random.Range(1,2)); // 공격 대기 시간
                }
            }
            else
            {
                yield return null;
            }
        }
    }




    void FireTarget()
    {
        AudioManager.instance.playSfx(AudioManager.Sfx.bossAttack);
        Vector2 playerPosition = GameManager.instance.player.transform.position;
        Vector2 bossPosition = transform.position;

       
        Vector2 direction = (playerPosition - bossPosition).normalized;


        
        SpawnBullet(transform.position, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.left * 1.3f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.right * 1.3f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.left * 1.7f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.right * 1.7f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.left * 2f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.right * 2f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.left * 2.3f, bulletSpeed * direction);
        SpawnBullet(transform.position + Vector3.right * 2.3f, bulletSpeed * direction);

    }


    void FireCircle()
    {
        print("원형 미사일 발사");
        AudioManager.instance.playSfx(AudioManager.Sfx.bossAttack);
        int numberOfBullets4 = 40; // 발사할 총알의 개수

        for (int i = 0; i < numberOfBullets4; i++)
        {
            // 각도 계산 (라디안으로 변환하여 사용)
            float angleRadians4 = Mathf.PI * 2 * i / numberOfBullets4;
            Vector2 dirVec4 = new Vector2(Mathf.Cos(angleRadians4), Mathf.Sin(angleRadians4)).normalized;

            // 총알 발사

            GameObject bulletObj = GameManager.instance.poolManager.Get(bulletPrefabID);
            Bullet bullet = bulletObj.GetComponent<Bullet>();
            bullet.damage = damage;
            bulletObj.transform.position = transform.position;
            Rigidbody2D rb4 = bulletObj.GetComponent<Rigidbody2D>();
            rb4.AddForce(dirVec4 * bulletSpeed, ForceMode2D.Impulse);

            // 총알의 회전 설정
            Vector3 rotVec4 = Vector3.forward * (360f * i / numberOfBullets4 + 90f); // 적절한 회전 벡터 조정
            rb4.transform.rotation = Quaternion.Euler(rotVec4);
        }
    }


    void FireShotGun()
    {
        print("샷건 발사");
        AudioManager.instance.playSfx(AudioManager.Sfx.bossAttack);
        int numberOfBullets = 8; 
        float spreadAngle = 45f; 
        float startAngle = transform.eulerAngles.z - (spreadAngle / 2f); 

        for (int i = 0; i < numberOfBullets; i++)
        {
            // 각도 계산
            float angle = startAngle + (spreadAngle / (numberOfBullets - 1) * i);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            Vector2 bulletVelocity = rotation * Vector2.down * 3;

            SpawnBullet(transform.position, bulletVelocity, angle);
        }
    }

    IEnumerator FireEvent()
    {
        AudioManager.instance.playSfx(AudioManager.Sfx.bossEvent);
        print("이벤트 실행");
        foreach (var ball in eventBall)
        {
            ball.SetActive(true);
        }

        yield return new WaitForSeconds(5);

        foreach (var ball in eventBall)
        {
            ball.SetActive(false);
        }
    }

    void BarriarEvnet()
    {
        AudioManager.instance.playSfx(AudioManager.Sfx.barriar);
        print("보호막 실행");

        StartCoroutine(SetBarriar());
    }

    IEnumerator SetBarriar()
    {
        isBarriar = true;

        barriarObj.SetActive(true);

        yield return new WaitForSeconds(5f);

        isBarriar = false;

        barriarObj.SetActive(false);
    }

    void SpawnBullet(Vector3 position, Vector2 velocity, float angle = 0)
    {
        GameObject bulletObj = GameManager.instance.poolManager.Get(bulletPrefabID);
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.damage = damage;
        bulletObj.transform.position = position;
        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
        rb.AddForce(Vector2.down * bulletSpeed, ForceMode2D.Impulse);
        if (angle != 0)
        {
            rb.rotation = angle; 
        }
    }

    void Stop()
    {
        isBarriar = false;
        rb.velocity = Vector2.zero;
    }

    void Ondamaged(float damage)
    {
        if (!isLive)
            return;

        float currentTime = Time.time;

        if (currentTime - sfxDelay >= 0.2f)
        {
            AudioManager.instance.playSfx(AudioManager.Sfx.bossDamage);
            sfxDelay = currentTime;
        }
        anim.enabled = false;
        health -= damage;
        sp.color = new Color(0, 0, 0, 0.5f);
        Invoke("ReColor", 0.1f);

        if (health <= 0)
        {
            OnDead();
        }
    }

    void ReColor()
    {
        sp.color = new Color(0.3f, 0, 0.8f , 1);
        anim.enabled = true;
    }
    void DeadReaction()
    {
        StopCoroutine(AttackPattern());
        rb.velocity = Vector2.down * 12;
    }

   

    void OnDead()
    {
        anim.enabled = true;
        sp.sortingOrder = 2;
        isLive = false;
        AudioManager.instance.playSfx(AudioManager.Sfx.bossBoom);
        anim.SetBool("isDie", true);
        // 모든 자식 오브젝트의 애니메이션 재생
        foreach (Animator childAnim in childAnims)
        {
            childAnim.SetBool("isDie", true);
        }
    }

    void IsDead()
    {
        Time.timeScale = 0;
        isLive = false;
        gameObject.SetActive(false); 
        GameManager.instance.GameOver.SetActive(true);
        AudioManager.instance.PlayBgm(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet") && isLive && isBarriar == false)
        {
            // PlayerBullet과 충돌했을 때의 처리
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            Ondamaged(bullet.damage);

            if (GameManager.instance.player.weapon.lv2_Power >= 1)
                return;

            collision.gameObject.SetActive(false);

        }
    }

    public void BoomObject()
    {
        AudioManager.instance.playSfx(AudioManager.Sfx.bossBoom);

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
}
