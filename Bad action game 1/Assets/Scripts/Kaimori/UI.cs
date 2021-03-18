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





    [SerializeField]private GameObject attackUIIcon;
    [SerializeField]private GameObject shootUIIcon;
    [SerializeField]private Sprite empwepsprite;
    [SerializeField]private Sprite spearsprite;
    [SerializeField]private Sprite batsprite;
    [SerializeField]private Sprite arrowsprite;
    [SerializeField]private Sprite bulletsprite;
    private Sprite attacksprite;
    private Sprite shootsprite;

    private void SetCurrentSpriteOfWeponIndex(int id, in Sprite spr1, in Sprite spr2, out Sprite outspr)
    {
        switch(id)
        {
            case 1:
                outspr = spr1;
                break;
            case 2:
                outspr = spr2;
                break;
            default:
                outspr = empwepsprite;
                break;
        }
    }

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
        if (PlayerController.Level <= 10)
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

        SetCurrentSpriteOfWeponIndex(WepomIndexContainer.CloseWeponIndex, spearsprite, batsprite, out attacksprite);
        SetCurrentSpriteOfWeponIndex(WepomIndexContainer.ShootWeponIndex, bulletsprite, arrowsprite, out shootsprite);

        this.attackUIIcon.GetComponent<Image>().sprite = attacksprite;
        this.shootUIIcon.GetComponent<Image>().sprite = shootsprite;
    }
}
