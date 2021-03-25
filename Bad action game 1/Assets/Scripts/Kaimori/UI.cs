using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject hpbar;
    GameObject hptext;
    GameObject expbar;
    GameObject exptext;
    GameObject lvltext;

    // Start is called before the first frame update
    void Start()
    {
        this.hpbar = GameObject.Find("HPBar");
        this.hptext = GameObject.Find("HPText");
        this.expbar = GameObject.Find("EXPBar");
        this.exptext = GameObject.Find("EXPText");
        this.lvltext = GameObject.Find("LVLText");
    }

    // Update is called once per frame
    void Update()
    {
        this.hpbar.GetComponent<Image>().fillAmount = PlayerController.curHP * 1.0f / PlayerController.maxHP;
        this.hptext.GetComponent<Text>().text = "HP:" + PlayerController.curHP.ToString() + " / " + PlayerController.maxHP.ToString();
        if (PlayerController.Level <= 20)
        {
            this.expbar.GetComponent<Image>().fillAmount = 1.0f * (PlayerController.curEXP - PlayerController.needEXP[PlayerController.Level - 1])
                / (PlayerController.needEXP[PlayerController.Level] - PlayerController.needEXP[PlayerController.Level - 1]);

            this.exptext.GetComponent<Text>().text = "EXP:" + PlayerController.curEXP.ToString() + " / " + PlayerController.needEXP[PlayerController.Level].ToString();
        }
        else
        {
            this.expbar.GetComponent<Image>().fillAmount = 1; this.exptext.GetComponent<Text>().text = "Level Maximized.";
        }
        this.lvltext.GetComponent<Text>().text = "Level: " + PlayerController.Level.ToString();
    }
}
