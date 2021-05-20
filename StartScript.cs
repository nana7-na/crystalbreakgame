using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//スタート画面
public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステージ遷移
    public void stage1()
    {
        SceneManager.LoadScene("EazyStage");
        //SceneManager.LoadScene("testStage");
    }
    public void stage2()
    {
        SceneManager.LoadScene("NormalGame");
    }
    public void stage3()
    {
        SceneManager.LoadScene("ExpertStage");
    }
    public void Menu() {
        Debug.Log("Menu");
        SceneManager.LoadScene("Start");
    }
}
