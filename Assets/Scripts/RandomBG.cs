using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBG : MonoBehaviour
{
    [SerializeField] Sprite[] bgs;
    SpriteRenderer sprite;
    
    private void Awake() {
         sprite=GetComponent<SpriteRenderer>();
    }
    void Start()
    {
       int randomNum=Random.Range(0,4);
       sprite.sprite=bgs[randomNum];

    }

 
}
