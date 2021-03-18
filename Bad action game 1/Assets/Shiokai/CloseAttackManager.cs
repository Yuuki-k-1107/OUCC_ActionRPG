using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CloseAttackManager : MonoBehaviour
{
    

    [Header("攻撃アニメspear")] public string stanim_spear;
   
    [Header("攻撃アニメbat")] public string stanim_bat;
    private Animator anim = null;
    //    private Rigidbody2D rb = null;

    private string stanim;


    private int wepindex = 0;



    private void SetCurrentParamOfWeponIndex(int id)
    {
        switch(id)
        {
            case 1:
                stanim = stanim_spear;
                anim.SetBool(stanim_bat, false);
                break;
            case 2:
                stanim = stanim_bat;
                anim.SetBool(stanim_spear, false);
                break;
            default:
                stanim = null;
                break;
        }
    }







    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        wepindex = WepomIndexContainer.CloseWeponIndex;

        SetCurrentParamOfWeponIndex(wepindex);

            if (Input.GetKeyDown(KeyCode.X))
            {
                anim.SetBool(stanim, true);
                Debug.Log("Activate close attack");
                
            }
            else
            {
                anim.SetBool(stanim, false);
            }
            
            //        rb = GetComponent<Rigidbody2D>();
        


    }
}

