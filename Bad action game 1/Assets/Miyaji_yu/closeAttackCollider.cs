using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeAttackCollider : MonoBehaviour
{
    private float count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if (count > 0.3f)
        {
            count = 0.0f;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            count = 1.0f;
        }
    }
}
