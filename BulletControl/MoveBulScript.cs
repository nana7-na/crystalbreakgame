using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBulScript : MonoBehaviour
{
    /// <summary>
    /// 射撃時の弾管理。複数の弾を扱う、軌道の設定を行う
    /// </summary>
    protected List<GameObject> str_bul;
    protected List<GameObject> buls;

    //向き
    protected int dir;
    //生存時間
    protected float remain;

    protected float cooltime;

    // Start is called before the first frame update
    public virtual void Start()
    {
        remain = 1.0f;
        str_bul = new List<GameObject>();
        buls = new List<GameObject>();
        set_tag(this.tag);
        cooltime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    //弾を動かす。各スクリプトで設定
    public virtual void Shot(){
        buls.Clear();
    }

    //タグ設定
    public void set_tag(string tag_n)
    {
        this.tag = tag_n;
        dir = (tag_n == "Player") ? 1 : -1;
    }

    //実際の弾を作る
    public virtual void create_bulset(int n)
    {
        for (int i = 0; i < n; i++)
        {
            buls.Add(Instantiate(str_bul[0], this.transform.position, Quaternion.identity));
            buls[i].tag = this.tag;
            buls[i].GetComponent<Bullet>().set_remTime(remain);
        }
    }


}
