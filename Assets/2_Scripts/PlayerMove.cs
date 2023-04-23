using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float dir_h;
    float dir_v;
    float speed = 15f;
    [SerializeField]
    float jumpPow = 5f;

    bool canJump = true;

    Rigidbody player;

    Vector3 movVec;
    void Start()
    {
        player = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetDir();
        Move();
        Jump();
    }

    void GetDir()
    {
        dir_h = Input.GetAxis("Horizontal");
        dir_v = Input.GetAxis("Vertical");

        movVec = new Vector3(dir_h, 0, dir_v).normalized;
    }

    void Move()
    {
        this.transform.position += movVec * speed * Time.deltaTime;

        transform.LookAt(this.transform.position + movVec);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            player.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
            canJump = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}
