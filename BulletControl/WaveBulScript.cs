using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBulScript : MoveBulScript
{
    /// <summary>
    /// 三方向に撃つ
    /// </summary>

    //弾
    GameObject bul;

    int vel = 20;//速度

    // Start is called before the first frame update
    override public void Start()
    {
        //設定
        bul = Resources.Load("bullet_pl2") as GameObject;
        base.Start();
        remain = 0.5f;
        str_bul.Add(bul);
    }

    public override void create_bulset(int n)
    {
        base.create_bulset(n);
    }

    public override void Shot()
    {
        create_bulset(3);
        buls[0].GetComponent<Rigidbody2D>().velocity = new Vector2(vel * dir, 0);
        buls[1].GetComponent<Rigidbody2D>().velocity = new Vector2(vel * dir, 10);
        buls[2].GetComponent<Rigidbody2D>().velocity = new Vector2(vel * dir, -10);
        base.Shot();
    }
}
