using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾
public class Bullet : MonoBehaviour
{
    //float attack;
    int direction　= 1;
    float remain_time = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float pos = this.transform.position.x;
        //画面外に出た場合消去する
        Camera mc = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (mc != null){
            Vector2 right = mc.ScreenToWorldPoint(new Vector3(mc.pixelWidth, 0, 0.0f));//表示範囲の右端をとって、world座標に変換
            Vector2 left = mc.ScreenToWorldPoint(new Vector3(0, 0, 0));//左端
            if (this.transform.position.x >= right.x || this.transform.position.x <= left.x){
                Destroy(this.gameObject);
            }
        }
        //生存時間が終わったら消える
        if (remain_time <= 0)
        {
            Destroy(this.gameObject);
            Destroy(this);
        }
        else remain_time -= Time.deltaTime;
    }

    //tag設定
    public void set_tag(string name)
    {
        tag = name;
        //射撃方向の設定
        if(tag == "Player")direction = 1;
        else　direction = -1;
    }

    //方向取得
    public int get_dir(){return direction;}

    //生存時間設定
    public void set_remTime(float n) { remain_time = n; }

    //当たり判定
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //別グループのオブジェクトに当たったら弾は廃棄される。
        if (collision.gameObject.tag != this.tag){
            Destroy(this.gameObject);
        }
    }


}
