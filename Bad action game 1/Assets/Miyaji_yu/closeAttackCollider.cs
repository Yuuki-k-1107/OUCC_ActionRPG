using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeAttackCollider : MonoBehaviour
{
    private float delay = 0.3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
