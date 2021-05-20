using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactor : MonoBehaviour{
    private int HP = 1; //
    private GameObject bullet;
    private int direction = 1;

    //hpなどの設定
    public void make(int hp, GameObject b)
    {
        HP = hp;
        if (b != null){
            set_bul(b);
            direction = this.tag =="Player" ? 1 : -1;
        }
        else bullet = null;
    }

    //値取得
    public int get_HP()
    {
        return this.HP;
    }

    //変更
    public void set_bul(GameObject b)
    {
        bullet = Instantiate(b,this.transform.position,Quaternion.identity);
        bullet.tag = this.tag;
    }

    //死亡処理
    public void destroing(){
        if(HP <= 0) Destroy(this.gameObject);
    }

    //衝突
    public virtual void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag != this.tag) {
            damege();
            destroing();
        }
    }

    public void damege() {
        this.HP--;
    }


    //基本的な射撃
    public virtual void Shot() {
        Vector2 pos_bul = new Vector2(this.transform.position.x + 3*direction, this.transform.position.y);
        bullet.transform.position = pos_bul;
        bullet.GetComponent<MoveBulScript>().Shot();
    }
}
