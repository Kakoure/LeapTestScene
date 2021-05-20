using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : MonoBehaviour
{
    public GoInHoop hoopController;

    private Rigidbody rigid;

    private int goingRight;
    private float force;
    // Start is called before the first frame update
    void Start()
    {
        goingRight = 1;
        rigid = GetComponent<Rigidbody>();
    }

 
    void FixedUpdate()
    {
        force = hoopController.score / 3f;
        rigid.velocity = new Vector3(force * goingRight, 0, 0);


    }

    private void OnTriggerEnter(Collider other)
    {
        goingRight = -goingRight;
        rigid.velocity = new Vector3(-rigid.velocity.x, 0, 0);
        Debug.Log("nani");
    }
}
