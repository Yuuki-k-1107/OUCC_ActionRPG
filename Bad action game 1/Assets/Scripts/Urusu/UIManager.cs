using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject exp_object = null;
    public GameObject hp_object = null;
    public GameObject GameOver_object = null;

    public GameObject Player;

    StatusManager status_script;

    private Text exp_text;
    private Text hp_text;
    // Start is called before the first frame update
    void Start()
    {
        status_script = Player.GetComponent<StatusManager>();
        GameOver_object.SetActive(false);
        exp_text = exp_object.GetComponent<Text>();
        hp_text = hp_object.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        exp_text.text = "EXP:"+status_script.GetEXP();
        hp_text.text = "HP:"+ status_script.GetHP();

        if (Input.GetKey(KeyCode.X))
        {
            SetGameOver(true);
        }
    }

    public void SetGameOver(bool flag)
    {
        GameOver_object.SetActive(flag);
    }

    public void Retry()
    {
        SetGameOver(false);
        //初期化処理を行う
    }
}
