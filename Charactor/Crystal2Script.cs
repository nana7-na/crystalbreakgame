using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal2Script : Charactor
{
    const int HP = 5;
    private SceneScript director;
    private int point = 5; //このオブジェクトの得点
    private AudioSource se1;
    Camera mc;

    // Start is called before the first frame update
    void Start()
    {
        GameObject mc_go = GameObject.Find("Main Camera");
        director = mc_go.GetComponent<SceneScript>();
        base.make(HP, null);
        this.tag = "Enemy";
        se1 = GetComponent<AudioSource>();
        if (this.name == "RedCrystal") point = 10;
        else if (this.name == "GreenCrystal") point = 15;
        mc = mc_go.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.tag)
        {
            damege();
            if (get_HP() <= 0)
            {
                StartCoroutine(playse1());
                director.calcScore(point);
            }
        }
    }

    //破壊時の効果音
    IEnumerator playse1()
    {
        se1.PlayOneShot(se1.clip);
        Destroy(this.GetComponent<Collider2D>());

        yield return new WaitForSeconds(se1.clip.length);

        Destroy(this.gameObject);
    }
}