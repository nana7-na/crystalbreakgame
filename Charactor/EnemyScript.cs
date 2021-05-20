using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Charactor
{
    const int hp = 10;
    const int point = 1;

    int count = 0;
    private static int Benttime = 80;
    private SceneScript director;

    // Start is called before the first frame update
    void Start()
    {
        this.tag = "Enemy";
        make(hp,Resources.Load("StanderdShot") as GameObject);
        director = GameObject.Find("Main Camera").GetComponent<SceneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float pl_pos = director.pl_pos;
        if (pl_pos != 150)
        {
            float distance = Mathf.Abs(pl_pos - this.transform.position.x);
            if (distance <= 50.0f)
            {
                if (count == Benttime)
                {
                    Shot();
                    count = 0;
                }
                else count++;
            }
        }

    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != this.tag) damege();
        if (get_HP() <= 0)
        {
            destroing();
            director.calcScore(point);
        }
    }

}
