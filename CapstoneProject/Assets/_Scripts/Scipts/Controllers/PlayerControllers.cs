using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Players�� ���
public class PlayerControllers : MonoBehaviour
{
    
    public StatsScriptableObject playerScriptable;

    int dir=1;

    int maxDashCount = 2;
    int curdashCount = 0;
    int maxJumpCount = 2;
    int curjumpCount = 0;
    float pressJumpTimer = 0.3f;
    float pressJumpTime = 0;

    float dashTimer = 0.2f;
    float dashTime = 0;

    bool isWalk;
    bool dash;
    bool jump;
    bool isAtk;

    Animator ani;
    Rigidbody rigid;
    float pressAtkTime;
    bool atkDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpChecker();
        DashChecker();
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(Input.GetKeyUp(KeyCode.V))
            {
                Debug.Log("afd");
            }
            if(Input.GetKey(KeyCode.V)) {
                Debug.Log("afdasdfsfad");
            }
        }
    }
    private void FixedUpdate()
    {
        PlayerMovement();
    }

    // ��¡ Ÿ�̸�, �Ű����� �Ŀ� ��¡��
    public void PlayerCharging(float Timer)
    {
        if(Input.GetKeyDown(InputManager.GetAttackKey()))
            atkDown = true;
        if (Input.GetKey(InputManager.GetAttackKey()))
            pressAtkTime += Time.deltaTime;
        if (Input.GetKeyUp(InputManager.GetAttackKey()))
        {
            Debug.Log(pressAtkTime);
            if (pressAtkTime < Timer)
            {
                Debug.Log("ª�� ����");
            }
            else
            {
                Debug.Log("��� ����");
            }
            atkDown = false;
            pressAtkTime = 0.0f;
        }
    }

    

    //�̵����� �Լ� ������
    void PlayerMovement()
    {
        if (InputManager.GetIsCanInput())
        {
            PlayerVelocity();
            PlayerDirection();
            PlayerJump();
        }
        PlayerDash();


    }


    // �÷��̾� ĳ���Ͱ� �ٶ󺸴� ����
    void PlayerDirection()
    {
        // 1 == right, -1 == left

        // ��� 1 ( �� �� �θ� ������ )
        //if (InputManager.GetInputHorizontal() == 1)
        //{
        //    transform.LookAt(Vector3.right*100000);
        //}
        //else if(InputManager.GetInputHorizontal() == -1)
        //{
        //    transform.LookAt(Vector3.left*100000);
        //}

        // ��� 2 ( Ű �Էµ� Vector3���� ���� �ٶ� )
        if(InputManager.GetHorizontal()!=0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
                Vector3.left*InputManager.GetHorizontal());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerScriptable.rotateSpd);
            dir=(int)InputManager.GetHorizontal();
            ani.SetBool("isWalk", true);
        }
        else
        {
            ani.SetBool("isWalk", false);
        }
    }

    // �÷��̾� ĳ������ �̵�
    void PlayerVelocity()
    {
        rigid.velocity =
            new Vector3(
            InputManager.GetHorizontal() * playerScriptable.moveSpd,
            rigid.velocity.y,
            0);
    }
    void DashChecker()
    {
        if (Input.GetKeyDown(InputManager.GetDashKey()))
            dash = true;
    }
    // �÷��̾� ĳ������ �뽬
    void PlayerDash()
    {
        if (curdashCount<maxDashCount&&dash)
        {
            InputManager.SetIsCanInput(false);
            gameObject.layer = 0;
            rigid.useGravity = false;
            //rigid.velocity = transform.forward * 3;
            rigid.velocity = new Vector2(dir * 7,0);
            curdashCount++;
            Debug.Log("DashStart");
            Debug.Log(dash);
            Debug.Log(rigid.velocity);

        }
        if (dash)
        {
            dashTime += Time.deltaTime;
            Debug.Log(dashTime);
            if (dashTime >= dashTimer)
            {
                rigid.useGravity = true;
                dash = false;
                dashTime = 0;
                curdashCount = 0;
                InputManager.SetIsCanInput(true);
                Debug.Log("DashEnd");
                Debug.Log(rigid.velocity);
                Debug.Log("===================");
            }
        }
    }

    void JumpChecker()
    {
        if (Input.GetKeyDown(InputManager.GetJumpKey()))
            jump = true;
        if(Input.GetKeyUp(InputManager.GetJumpKey()))
            jump = false;
    }

    // �÷��̾� ĳ������ ����
    void PlayerJump()
    {
        //bool jump = Input.GetKey(InputManager.GetJumpKey());
       
        if (jump && curjumpCount < maxJumpCount)
        {
            pressJumpTime += Time.deltaTime;
            if (Input.GetKeyUp(InputManager.GetJumpKey()))
            {
                Debug.Log(pressJumpTime);
                Debug.Log("ª�� ����");
                Jump(5);
            }
            if (pressJumpTime >= pressJumpTimer)
            {
                Debug.Log(pressJumpTime);
                Debug.Log("��� ����");
                Jump(7);

            }
            
        }
        //if (!jump && curjumpCount < maxJumpCount)
        //{
        //    if (Input.GetKey(InputManager.GetJumpKey()))
        //        pressJumpTime += Time.deltaTime;
        //    if (pressJumpTime >= pressJumpTimer)
        //    {
        //        Debug.Log("��� ����");
        //        Jump(6);

        //    }
        //    else if (Input.GetKeyUp(InputManager.GetJumpKey()))
        //    {
        //        Jump(3);
        //        Debug.Log("ª�� ����");
        //    }
        //    Debug.Log(pressJumpTime);
        //}
        
    }
    void Jump(int value)
    {
        if (!dash)
        {
            rigid.velocity = Vector2.up * 0;
            rigid.velocity = Vector2.up * value;
            pressJumpTime = 0;
            jump = false;
            curjumpCount++;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer== 7)
        {
            curjumpCount = 0;
        }
    }
}