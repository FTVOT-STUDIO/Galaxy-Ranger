using System.Collections;
using System.Net;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Vector2 inputVec;
    public GameObject shadow;

    public bool isInviblity;
    public bool isInviblityItem;

    public bool isHit;

    public GameObject levelGroup;

    public GameObject[] levelSKin;

    public GameObject barriar;

    Rigidbody2D rb;
    Animator anim;
    Animator shadowAnim;
    public BulletWeapon weapon;

    SpriteRenderer sp;

    void Awake()
    {
        weapon = GetComponentInChildren<BulletWeapon>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        shadowAnim = shadow.GetComponent<Animator>();

    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        if (!isHit)
        {
            Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

            rb.MovePosition(nextVec + rb.position);
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sp.flipX = inputVec.x < 0;
        }

        anim.SetFloat("MoveDir", Mathf.Abs(inputVec.x)); // 양수든 음수든 절대값 반환
        shadowAnim.SetFloat("MoveDir", Mathf.Abs(inputVec.x)); // 양수든 음수든 절대값 반환
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isInviblity || isInviblityItem)
                return;

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            GameManager.instance.health -= enemy.damage;

            // print("쳐맞았습네다.");
            AudioManager.instance.playSfx(AudioManager.Sfx.PlayerDamage);
            StartCoroutine(InviBlity());

        }
        else if (collision.gameObject.CompareTag("Enemy Bullet"))
        {
            if (isInviblity || isInviblityItem)
                return;

            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            GameManager.instance.health -= bullet.damage;
            AudioManager.instance.playSfx(AudioManager.Sfx.PlayerDamage);

            StartCoroutine(InviBlity());
        }

        if (GameManager.instance.health <= 0)
        {
            AudioManager.instance.PlayBgm(false);

            GameManager.instance.isLive = false;

            Time.timeScale = 0;
            AudioManager.instance.playSfx(AudioManager.Sfx.playerDead);
            GameManager.instance.GameOver.SetActive(true);
        }
    }


    IEnumerator InviBlity()
    {
        isHit = true;
        transform.position = Vector3.down * 3.5f;
        sp.enabled = false;
        levelGroup.SetActive(false);
        shadow.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        isHit = false;
        levelGroup.SetActive(true);
        sp.enabled = true;
        shadow.SetActive(true);
        anim.SetBool("isHit", true);
        shadowAnim.SetBool("isHit", true);
        isInviblity = true;

        yield return new WaitForSeconds(2f);
        anim.SetBool("isHit", false);
        shadowAnim.SetBool("isHit", false);
        if (weapon.isAblityActive)
            yield break;         // 무적 중복 활성화 방지

        isInviblity = false;
    }

}

