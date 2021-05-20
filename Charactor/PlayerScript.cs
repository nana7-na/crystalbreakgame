using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Charactor
{

    const int HP_PL = 10;

    float bul_waittime;
    float sleeptime;

    private SceneScript director;
    private AudioSource player_as;
    private Rigidbody2D rb2D;

    private AudioClip se1;//射撃音
    private AudioClip se2;//ぶつかった音

    // Start is called before the first frame update
    void Start()
    {
        //HPなどの設定
        this.tag = "Player";
        base.make(HP_PL, Resources.Load("3wayBul") as GameObject);
        bul_waittime = -1;
        sleeptime = 0;
        rb2D = GetComponent<Rigidbody2D>();
        director = GameObject.Find("Main Camera").GetComponent<SceneScript>();
        director.hp_ui(get_HP());
        player_as = GetComponent<AudioSource>();
        //オーディオ読み込み
        se1 = Resources.Load("shot1") as AudioClip;
        se2 = Resources.Load("crash") as AudioClip;
    }


    // Update is called once per frame
    void Update()
    {
        //画面外から出たら消える。ゲームオーバー
        Camera mc = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (mc != null)
        {
            Vector2 right = mc.ScreenToWorldPoint(new Vector3(mc.pixelWidth, 0, 0.0f));//表示範囲の右端をとって、world座標に変換
            Vector2 left = mc.ScreenToWorldPoint(new Vector3(0, 0, 0));//左端
            if (this.transform.position.x >= right.x + 5 || this.transform.position.x <= left.x - 5) Destroy(this.gameObject);
        }

        //射撃クールタイム
        if (bul_waittime >= 0)
        {
            bul_waittime+=Time.deltaTime;
            if (bul_waittime > 0.2f) bul_waittime = -1;
        }
        //跳ね返り後一定時間移動を受け付けない
        if (sleeptime > 0)
        {
            sleeptime-=Time.deltaTime;
            if (sleeptime <= 0) sleeptime = 0;
        }
    }

    //物理演算
    private void FixedUpdate()
    {
        //移動
        if (sleeptime == 0)
        {
            if (Input.GetKey(KeyCode.A)) rb2D.MovePosition(new Vector2(transform.position.x - 0.5f, transform.position.y));
            else if (Input.GetKey(KeyCode.D)) rb2D.MovePosition(new Vector2(transform.position.x + 0.5f, transform.position.y));
            else if (Input.GetKey(KeyCode.W)) rb2D.MovePosition(new Vector2(transform.position.x, transform.position.y + 0.5f));
            else if (Input.GetKey(KeyCode.S)) rb2D.MovePosition(new Vector2(transform.position.x, transform.position.y - 0.5f));
            if (Input.GetKey(KeyCode.Space)) myShot();
        }
        //抵抗
        if (rb2D.velocity != Vector2.zero)
        {
            rb2D.AddForce(rb2D.velocity / rb2D.velocity.magnitude * -1, ForceMode2D.Impulse);
            if (rb2D.velocity.magnitude <= 0) rb2D.velocity = Vector2.zero;
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //charactorの動作
        base.OnCollisionEnter2D(collision);
        //UI更新
        director.hp_ui(get_HP());

        //物に当たったら跳ね返る
        GameObject col = collision.gameObject;
        if (col.GetComponent<Bullet>() == null && col.tag != "Player")
        {
            player_as.volume = 1.0f;
            player_as.PlayOneShot(se2);//効果音再生
            //跳ね返り
            Vector2 pl_pos = this.gameObject.transform.position;
            Vector2 v = pl_pos - collision.GetContact(0).point;
            v = v / v.magnitude * 25 + new Vector2(-10,0);
            rb2D.velocity = Vector2.zero;
            rb2D.AddForce(v, ForceMode2D.Impulse);
            sleeptime = 0.4f;
        }
    }

    //弾撃つ
    public void myShot()
    {
        if (bul_waittime < 0)
        {
            Shot();
            player_as.volume = 0.2f;
            player_as.PlayOneShot(se1);//効果音再生
            bul_waittime = 0;
        }
    }
}
