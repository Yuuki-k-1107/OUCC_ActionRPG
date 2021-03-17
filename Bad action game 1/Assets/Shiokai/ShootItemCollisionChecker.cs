using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootItemCollisionChecker : MonoBehaviour
{

    // このオブジェクトに触れたときに取得する近接武器のindex
    [SerializeField]
    [Header("遠距離武器のid")]private int ShootWeponIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            WepomIndexContainer.ShootWeponIndex = this.ShootWeponIndex;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
