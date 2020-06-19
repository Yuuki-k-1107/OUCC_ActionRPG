using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCollider_bat : MonoBehaviour
{
    public Collider2D batCollider;
    // Start is called before the first frame update
    void Start()
    {
        //子オブジェクト"bat_collider"を取得
        GameObject batcollider = transform.Find("bat_collider").gameObject;

        batCollider = batcollider.GetComponent<BoxCollider2D>();

        batCollider.enabled = false;

    }

    public void on_bat()
    {
        batCollider.enabled = true;
    }

    public void off_bat()
    {
        batCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
