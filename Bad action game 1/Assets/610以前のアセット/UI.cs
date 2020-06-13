using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameObject hpbar;
    GameObject hptext;

    // Start is called before the first frame update
    void Start()
    {
        this.hpbar = GameObject.Find("HPBar");
        this.hptext = GameObject.Find("HPText");
    }

    // Update is called once per frame
    void Update()
    {
        this.hpbar.GetComponent<Image>().fillAmount = Plyrctlr.curHP * 1.0f / Plyrctlr.maxHP;
        this.hptext.GetComponent<Text>().text = "HP:" + Plyrctlr.curHP.ToString() + " / " + Plyrctlr.maxHP.ToString();
    }
}
