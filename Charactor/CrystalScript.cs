using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : Charactor
{
    const int HP = 6;
    private SceneScript director;
    private int point = 5; //このオブジェクトの得点
    private AudioSource se1;
    private ParticleSystem ps;
    private bool ps_b;
    Camera mc;

    // Start is called before the first frame update
    void Start()
    {
        //設定
        GameObject mc_go = GameObject.Find("Main Camera");
        director = mc_go.GetComponent<SceneScript>();
        ps = GetComponentInChildren<ParticleSystem>();
        base.make(HP, null);
        this.tag = "Enemy";
        se1 = GetComponent<AudioSource>();
        if (this.name == "RedCrystal") point = 10;
        else if (this.name == "GreenCrystal") point = 15;
        mc = mc_go.GetComponent<Camera>();
        ps_b = false;
    }

    // Update is called once per frame
    void Update()
    {
        //画面外の場合
        if (mc != null)
        {
            Vector2 right = mc.ScreenToWorldPoint(new Vector3(mc.pixelWidth, 0, 0.0f));//表示範囲の右端をとって、world座標に変換
            Vector2 left = mc.ScreenToWorldPoint(new Vector3(0, 0, 0));//左端

            if (this.transform.position.x >= right.x || this.transform.position.x <= left.x)//画面外
            {
                if (ps_b)
                {
                    //放出状態から画面外の場合破棄
                    ps.Stop();
                    Destroy(this.gameObject);
                }
            }
            else if(!ps_b)//画面に入った場合
            {
                if (!ps.isEmitting){ ps.Play();}
                ps_b = true;
            }
        }
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != this.tag)
        {
            damege();//ダメージ処理
            if (get_HP() <= 0)//HPがなくなったら破壊
            {
                StartCoroutine(playse1());
                director.calcScore(point);
            }
        }
    }

    //破壊時の効果音
    IEnumerator playse1()
    {
        se1.PlayOneShot(se1.clip);//再生
        //破壊エフェクト
        ps.Stop();
        ps.Clear();
        ParticleSystem.MainModule main = ps.main;
        main.loop = false;
        main.duration = 0.15f;
        main.maxParticles = 500;
        main.startLifetime = 0.5f;
        main.startSpeed = 60;
        main.startSize = 2.0f;
        ParticleSystem.EmissionModule emi = ps.emission;
        emi.rateOverTime = 300;
        ParticleSystem.LimitVelocityOverLifetimeModule LVOL = ps.limitVelocityOverLifetime;
        LVOL.enabled = true;
        LVOL.dampen = 0.3f;
        LVOL.limit = 5;
        ps.Play();
        //破壊して見えなくする
        Destroy(this.GetComponent<Collider2D>());
        Destroy(this.GetComponent<MeshFilter>());
        //効果音再生待ち
        yield return new WaitForSeconds(se1.clip.length);
        //オブジェクト破棄
        Destroy(this.gameObject);
    }
}