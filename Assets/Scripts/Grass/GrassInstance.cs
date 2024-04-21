using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInstance : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    SpriteRenderer spriteRenderer;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        chooseSprite();
    }

    void chooseSprite()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length - 1)];
    }

}
