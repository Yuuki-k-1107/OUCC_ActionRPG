using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlCollider_spear : MonoBehaviour
{
    public Collider2D spearCollider;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spearcollider = transform.Find("spear_collider").gameObject;

        spearCollider = spearcollider.GetComponent<BoxCollider2D>();

        spearCollider.enabled = false;
    }
    public void on_spear()
    {
        spearCollider.enabled = true;

    }

    public void off_spear()
    {
        spearCollider.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
