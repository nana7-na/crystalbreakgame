using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//UIとシーン切替をする
public class SceneScript : MonoBehaviour
{
    //UIの設定用
    Text HP;
    Text Score;

    int myScore;
    int myHP;
    const int EndLine = 130;

    public float pl_pos;

    // Start is called before the first frame update
    void Start()
    {
        myScore = 0;
        pl_pos = EndLine+20;
        //UIを表示するためにUIのオブジェクトをとってくる
        HP = GameObject.Find("myHP").GetComponentInChildren<Text>();
        Score = GameObject.Find("Score").GetComponentInChildren<Text>();
        myHP = 0;
        //スコア表示
        HP.text = "HP:" + myHP;
        Score.text = "Score:" + myScore;
    }

    // Update is called once per frame
    void Update()
    {
        //表示画面移動
        
        if(transform.position.x <= 150) {
            transform.Translate(new Vector2(5.0f*Time.deltaTime , 0.0f));
        }
        //プレイヤーがあるか
        GameObject pl = GameObject.Find("Player");
        //プレイヤー消えたらゲームオーバー
        if (pl == null)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            pl_pos = pl.transform.position.x;
            //ゴールに着いたらクリア画面
            if (pl_pos >= EndLine) SceneManager.LoadScene("GameClear");
        }
    }

    public void hp_ui(int hp)
    {
        HP.text = "HP:" + hp;
    }

    public void calcScore(int col_s)
    {
        myScore += col_s;
        Score.text = "Score:" + myScore;
    }
}
