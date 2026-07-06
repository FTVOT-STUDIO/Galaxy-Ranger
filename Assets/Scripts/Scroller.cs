using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    public int count;
    public float baseSpeed; 
    public float speed;

    public Sprite[] sp;
    public Text stageText;

    private int currentScreenIndex = 0;

    void Start()
    {
        baseSpeed = speed; 
        count = transform.childCount - 1;
        UpdateValue();
    }

    void Update()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
        UpdateValue();
    }

    void UpdateValue()
    {
        int score = GameManager.instance.score;

        if (score < 1000)
        {
            speed = baseSpeed * 1;
            currentScreenIndex = 0;
        }
        else if (score >= 2000 && score < 3000)
        {
            speed = baseSpeed * 1.2f;
            currentScreenIndex = 0;
        }
        else if (score >= 3000 && score < 4000)
        {
            speed = baseSpeed * 1.4f;
            currentScreenIndex = 0;
        }
        else if (score >= 4000 && score < 5000) 
        {
            speed = baseSpeed * 1.6f;
            currentScreenIndex = 1;
        }
        else if (score >= 5000 && score < 6000) 
        {
            speed = baseSpeed * 1.8f;
            currentScreenIndex = 1;
        }
        else if (score >= 6000 && score < 7000) 
        {
            speed = baseSpeed * 2f;
            currentScreenIndex = 2;
        }
        else if (score >= 7000 && score < 8000)
        {
            speed = baseSpeed * 2.3f;
            currentScreenIndex = 2;
        }
        else if (score >= 8000 && score < 9000) 
        {
            speed = baseSpeed * 2.5f;
            currentScreenIndex = 2;
        }

        ChangeSprites(currentScreenIndex);
        UpdateScreenNumberText();
    }

    void ChangeSprites(int spriteIndex)
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();

            if (sp.Length > spriteIndex)
            {
                sr.sprite = sp[spriteIndex];
            }
        }
    }

    void UpdateScreenNumberText()
    {
        stageText.text = $"{currentScreenIndex + 1}/{count}";
    }
}
