using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumCtrl : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite = new Sprite[10];
    public void ChangeSprite(int number)
    {
        if (number > 9 || number < 0) number = 0;

        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite[number];
    }
}
