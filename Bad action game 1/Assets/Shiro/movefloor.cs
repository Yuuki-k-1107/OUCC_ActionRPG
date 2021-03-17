using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movefloor : MonoBehaviour
{
    [Header("移動経路")] public GameObject[] movePoint;
    [Header("速さ")] public float speed = 1.0f;
    private Rigidbody2D rb;
    private int nowPoint = 0;
    private bool returnPoint = false;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[0].transform.position;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (movePoint != null && movePoint.Length > 1 && rb != null) {
            if (!returnPoint) {
                int nextPoint = nowPoint + 1;
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    rb.MovePosition(toVector);
                }
                else {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;
                    if (nowPoint + 1 >= movePoint.Length)
                    {
                        returnPoint = true;
                    }
                }
            }
            else
            {
                int nextPoint = nowPoint - 1;
                if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f){
                    Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;
                    if (nowPoint <= 0)
                    {
                        returnPoint = false;
                    }
                }
            }
                }
    }
}
