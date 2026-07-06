using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EventBall : MonoBehaviour
{
    SpriteRenderer sp;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    void Ondamaged()
    {
        sp.color = new Color(0, 0, 0, 0.5f);
        Invoke("ReColor", 0.1f);
    }

    void ReColor()
    {
        sp.color = new Color(1, 0.2f, 0.2f,1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            if (GameManager.instance.ablity < 100 && !GameManager.instance.player.weapon.isAblityActive)
            {
                GameManager.instance.ablity++;
            }
            Ondamaged();
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            GameManager.instance.boss.health -= bullet.damage;
            GameManager.instance.score += 30;
            collision.gameObject.SetActive(false);

            DropItem();

        }

        void DropItem()
        {
            float ran = Random.Range(0, 1f);

            if (ran <= 0.05f)
            {
                GameObject eventBall = GameManager.instance.poolManager.Get(Random.Range(6, 10));
                eventBall.transform.position = ScreenPosition();
            }
            
            else
            {
                print("꽝");
                
            }
        
        }
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
}
