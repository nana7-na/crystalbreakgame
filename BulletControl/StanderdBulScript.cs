using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanderdBulScript : MoveBulScript
{
    /// <summary>
    /// 直線に一つの弾を撃つ
    /// </summary>

    //弾
    private GameObject bul;

    // Start is called before the first frame update
    override public void Start()
    {
        //設定
        bul = Resources.Load("bul_enemy") as GameObject;
        base.Start();
        str_bul.Add(bul);
        cooltime = 0.2f;
    }

    public override void Shot()
    {
        create_bulset(1);
        buls[0].GetComponent<Rigidbody2D>().velocity = new Vector2(20 * dir, 0);
        base.Shot();
    }
}
