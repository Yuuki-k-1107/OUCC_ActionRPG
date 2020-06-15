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
        this.hpbar.GetComponent<Image>().fillAmount = Plyrctlr.curHP * 1.0f / Plyrctlr.maxHP;
        this.hptext.GetComponent<Text>().text = "HP:" + Plyrctlr.curHP.ToString() + " / " + Plyrctlr.maxHP.ToString();
        if (Plyrctlr.Level <= 10)
        {
            this.expbar.GetComponent<Image>().fillAmount = 1.0f * (Plyrctlr.curEXP - Plyrctlr.needEXP[Plyrctlr.Level - 1])
                / (Plyrctlr.needEXP[Plyrctlr.Level] - Plyrctlr.needEXP[Plyrctlr.Level - 1]);

            this.exptext.GetComponent<Text>().text = "EXP:" + Plyrctlr.curEXP.ToString() + " / " + Plyrctlr.needEXP[Plyrctlr.Level].ToString();
        }
        else
        {
            this.expbar.GetComponent<Image>().fillAmount = 1; this.exptext.GetComponent<Text>().text = "Level Maximized.";
        }
        this.lvltext.GetComponent<Text>().text = "Level: " + Plyrctlr.Level.ToString();
    }
}
