using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class TestMonster : Enemy
{
    private void Start()
    {
        maxPattern = 3;
    }

    private void Update()
    {

        PatternSelecter();
        WallCheck();
        FloorCheck();
        // rigid.velocity = Vector3.forward * enemyScriptable.moveSpd;
        TestPattern();
    }

    public void TestPattern()
    {

        if (isTrace && target != null)
        {

            if (target.transform.position.x - transform.position.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            Direction();
            rigid.velocity = new Vector2(dir * enemyScriptable.moveSpd, rigid.velocity.y);

        }
        else
        {
            if (!damaged)
            {
                switch (curPattern)
                {
                    case 0:
                        dir = 0;
                        break;
                    case 1:
                        dir = 1;
                        Direction();
                        break;
                    case 2:
                        dir = -1;
                        Direction();
                        break;
                }

                if ((isWall || !isFloor))
                {
                    rigid.velocity = Vector3.zero;
                }
                else
                {
                    rigid.velocity = new Vector3(dir * enemyScriptable.moveSpd, rigid.velocity.y, 0);
                }



                //if ((isWall || !isFloor))
                //{
                //    rigid.velocity = Vector2.zero;
                //}
                //else
                //{
                //    rigid.velocity = new Vector2(-dir * stats.moveSpeed, rigid.velocity.y);
                //}

            }
            else if (rigid.velocity.x == 0)
            {
                damaged = false;
            }
        }
        if (rigid.velocity != Vector3.zero)
            ani.SetBool("Walk Forward", true);
        else
            ani.SetBool("Walk Forward", false);
    }
}
