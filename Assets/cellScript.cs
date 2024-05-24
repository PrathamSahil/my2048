using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cellScript : MonoBehaviour
{
    //public logicScript logic;

    public Sprite spr0;
    public Sprite spr2;
    public Sprite spr4;
    public Sprite spr8;
    public Sprite spr16;
    public Sprite spr32;
    public Sprite spr64;
    public Sprite spr128;
    public Sprite spr256;
    public Sprite spr512;
    public Sprite spr1024;
    public Sprite spr2048;
    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        //logic = GameObject.FindGameObjectWithTag("logicTag").GetComponent<logicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //logic.grid[r][c]
    }
    public void updateSprite(int val)
    {
        if (val == 0)
        {
            sp.sprite = spr0;
        }
        else if (val == 2)
        {
            sp.sprite = spr2;
        }
        else if (val == 4)
        {
            sp.sprite = spr4;
        }
        else if (val == 8)
        {
            sp.sprite = spr8;
        }
        else if (val == 16)
        {
            sp.sprite = spr16;
        }
        else if (val == 32)
        {
            sp.sprite = spr32;
        }
        else if (val == 64)
        {
            sp.sprite = spr64;
        }
        else if (val == 128)
        {
            sp.sprite = spr128;
        }
        else if (val == 256)
        {
            sp.sprite = spr256;
        }
        else if (val == 512)
        {
            sp.sprite = spr512;
        }
        else if (val == 1024)
        {
            sp.sprite = spr1024;
        }
        else if (val == 2048)
        {
            sp.sprite = spr2048;
        }
        //Debug.Log("Updated sprite to "+val);
    }
}
