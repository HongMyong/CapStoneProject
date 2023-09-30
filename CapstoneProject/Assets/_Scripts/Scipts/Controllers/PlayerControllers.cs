using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Players�� ���
public class PlayerControllers : MonoBehaviour
{
    
    public StatsScriptableObject playerScriptable;

    bool isWalk;
    bool isDash;
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
        PlayerCharging(3);
    }
    private void FixedUpdate()
    {
        PlayerMovement();
        
    }
    public Vector3 GetInputMove()
    {
        float horizontal = 0;
        float vertical = 0;
        //Vector3 inputXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }

        return new Vector3(horizontal, 0, vertical);
    }
    public float GetInputVertical()
    {
        float vertical = 0;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = Input.GetAxisRaw("Vertical");
        }
        return vertical;
    }
    public float GetInputHorizontal()
    {
        float horizontal = 0;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        return horizontal;
    }
    public KeyCode GetAttackKey()
    {
        return KeyCode.X;
    }
    public KeyCode GetDashKey()
    {
        return KeyCode.C;
    }

    // ��¡ Ÿ�̸�, �Ű����� �Ŀ� ��¡��
    public void PlayerCharging(float Timer)
    {
        if(Input.GetKeyDown(GetAttackKey()))
            atkDown = true;
        if (Input.GetKey(GetAttackKey()))
            pressAtkTime += Time.deltaTime;
        if (Input.GetKeyUp(GetAttackKey()))
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
        PlayerDirection();
        PlayerVelocity();
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
        if(InputManager.GetInputMove()!=Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(InputManager.GetInputMove());
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * playerScriptable.rotateSpd);
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
        rigid.velocity=
            InputManager.GetInputMove() * playerScriptable.moveSpd;
    }

    // �÷��̾� ĳ������ �뽬
    void PlayerDash()
    {
        bool dash = Input.GetKeyDown(GetDashKey());
        float curDashTime=2;
        if (dash)
        {
            isDash = true;
            gameObject.layer = 0;
            rigid.useGravity = false;
            rigid.velocity = this.transform.forward * 10;
        }
        if (isDash)
        {
            curDashTime-=Time.deltaTime;
            if (curDashTime <= 0)
            {
                rigid.useGravity = true;
                isDash = false;
            }
                
        }
    }
}