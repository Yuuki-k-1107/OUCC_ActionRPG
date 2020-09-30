using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttackManager : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private Collider2D spearCollider;

    [SerializeField]
    private Collider2D batCollider;

    public int CloseWeponIndex = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bat")
        {
            CloseWeponIndex = 1;
        }
        else if (other.gameObject.tag == "spear")
        {
            CloseWeponIndex = 2;
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




        if (Input.GetButtonDown("CloseAttack"))
        {
                if (CloseWeponIndex == 1)
                {
                    anim.SetBool("Spear", true);
                }
                else if (CloseWeponIndex == 2)
                {
                    anim.SetBool("Bat", true);
                }

        }
        else if (Input.GetButtonUp("CloseAttack"))
        {
            anim.SetBool("Spear", false);

            anim.SetBool("Bat", false);
        }
    }

}
