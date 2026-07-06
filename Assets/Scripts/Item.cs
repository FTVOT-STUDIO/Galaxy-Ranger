using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { WeaponType01, WeaponType02, health, InviBlity, Boom }
    public ItemType Type;

    BoxCollider2D boxColl;
    SpriteRenderer sp;

    Rigidbody2D rb;

    public float moveItem = 3.5f;

    public Player player;

    void Awake()
    {
        boxColl = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

    }


    void OnEnable()
    {
        player = GameManager.instance.player.GetComponent<Player>();
        rb.velocity = Vector2.down * moveItem;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (Type)
            {
                case ItemType.WeaponType01:
                    print("무기 01");
                    if (GameManager.instance.player.weapon.lv1_Power >= 3)
                    {
                        AudioManager.instance.playSfx(AudioManager.Sfx.itemGet);
                        GameManager.instance.score += 300;
                        gameObject.SetActive(false);
                        return;
                    }
                    AudioManager.instance.playSfx(AudioManager.Sfx.WeaponGet);
                    GameManager.instance.player.weapon.lv1_Power++;

                    gameObject.SetActive(false);

                    break;
                case ItemType.WeaponType02:
                    print("무기 02");
                    if (GameManager.instance.player.weapon.lv2_Power >= 3)
                    {
                        AudioManager.instance.playSfx(AudioManager.Sfx.itemGet);
                        GameManager.instance.score += 300;
                        gameObject.SetActive(false);
                        return;
                    }
                    AudioManager.instance.playSfx(AudioManager.Sfx.WeaponGet);
                    GameManager.instance.player.weapon.lv2_Power++;

                    gameObject.SetActive(false);

                    break;
                case ItemType.health:
                    print("체력 회복");
                   
                    GameManager.instance.health = GameManager.instance.maxhealth;
                    AudioManager.instance.playSfx(AudioManager.Sfx.HealGet);
                    gameObject.SetActive(false);
                    break;
                case ItemType.InviBlity:
                    print("무적");
                    StartCoroutine(IsInviBlityItem());
                    if (GameManager.instance.player.isInviblity == true)
                        return;
                    AudioManager.instance.playSfx(AudioManager.Sfx.InvitGet);
                    

                    break;
                case ItemType.Boom:
                    print("폭탄");
                    AudioManager.instance.playSfx(AudioManager.Sfx.boomGet);
                    GameManager.instance.exp += 10;
                    StartCoroutine(BoomItem());

                    break;
            }
        }
        else if (collision.gameObject.CompareTag("Boder"))
        {
            moveItem *= -1;
            rb.velocity = Vector2.down * moveItem;
        }
    }

    IEnumerator BoomItem()
    {
        GameManager.instance.Boom.gameObject.SetActive(true);

        GameManager.instance.BoomObject();

        yield return new WaitForSeconds(0.2f);

        GameManager.instance.Boom.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


    IEnumerator IsInviBlityItem()
    {
        if (player.isInviblityItem == true || player.weapon.isAblityActive == true)
            yield break;

        player.isInviblity = true;
        player.isInviblityItem = true;
        boxColl.enabled = false;
        sp.enabled = false;
        GameManager.instance.player.barriar.SetActive(true);
        yield return new WaitForSeconds(7f);
        
        GameManager.instance.player.barriar.SetActive(false);
        player.isInviblity = false;
        player.isInviblityItem = false;
        gameObject.SetActive(false);
    }

}
