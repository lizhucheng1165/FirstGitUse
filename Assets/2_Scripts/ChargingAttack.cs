using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargingAttack : MonoBehaviour
{
    [SerializeField]
    Slider charge;

    [SerializeField]
    float att;
    float jumpPower = 300f;
    float horizontal = 0f;

    public bool canJump = true;
    void Start()
    {
        charge.maxValue = 5f;
        charge.minValue = 0;
    }

    void Update()
    {
        Charging();
        ShowGage();
        CheckGround();
        GetDir();
    }


    void Charging()
    {
        if (att < 5 && canJump == true)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                print("��¡��");
                att += 0.025f;
            }
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            print("����" + att);
            Jump();
            att = 0;
        }
    }

    void ShowGage()
    {
        charge.value = att;
    }


    void GetDir()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontal = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }
    }

    void Jump()
    {
        print("�����ߵ�");

        this.GetComponent<Rigidbody>().AddForce(new Vector3(horizontal, 1, 0) * att * Time.deltaTime * jumpPower, ForceMode.Impulse);
    }


    void CheckGround()
    {
        float characterWidth = GetComponent<Collider>().bounds.size.x / 2;
        Vector3 leftRayOrigin = new Vector3(transform.position.x - characterWidth, transform.position.y, transform.position.z);
        Vector3 rightRayOrigin = new Vector3(transform.position.x + characterWidth, transform.position.y, transform.position.z);

        Ray leftRay = new Ray(leftRayOrigin, Vector3.down);
        Ray rightRay = new Ray(rightRayOrigin, Vector3.down);

        RaycastHit leftHit;
        RaycastHit rightHit;

        Debug.DrawRay(leftRayOrigin, Vector3.down, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector3.down, Color.red);

        bool leftRayHitGround = Physics.Raycast(leftRay, out leftHit, 1f);
        bool rightRayHitGround = Physics.Raycast(rightRay, out rightHit, 1f);

        if (leftRayHitGround && leftHit.transform.CompareTag("Ground") || rightRayHitGround && rightHit.transform.CompareTag("Ground"))
        {
            print("���� �ִ�");
            canJump = true;
        }
        else
        {
            print("�����̴�");
            canJump = false;
        }
    }

    
}
