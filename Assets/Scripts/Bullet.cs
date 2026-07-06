using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    Transform trans;
    Rigidbody2D rb;

    void Awake()
    {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boder")
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        trans.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
    }

    public void Init(int bulletDamage)
    {
        damage = bulletDamage;
    }
}
