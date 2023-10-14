using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    private ModeBase currentMode; // ���� ��� ���庯��

    void Start()
    {
        Init();
    }
    void Update()
    {
        //currentMode.ApplyPassive();//�нú�� ��������̶� update �÷��̾� ��ũ��Ʈ�� �Űܵ���
    }
    public void SetMode(ModeBase mode)//��� �⺻���� �޾ƿͼ� �����Ű�� ��
    {
        currentMode = mode;
        currentMode.OnModeActivated(); // ��尡 Ȱ��ȭ �Ǿ��ٰ� �˸��� �޼���
    }

    public void Attack()//���ݸ޼��� 
    {
        currentMode.Attack();
    }
    public void UseSkill()//��ų���޼���
    {
        currentMode.UseSkill();
    }

    public CoreType GetCoreType()//���� �ھ�Ÿ�� ��ȯ
    {
        return currentMode.GetCoreType();
    }

    public int GetAttackDamage()//���ݵ����� ��ȯ
    {
        return currentMode.GetAttackDamage();
    }
    public int GetSkillDamage()//��ų������ ��ȯ
    {
        return currentMode.GetSkillDamage();
    }
    public void Init()
    {
        SetMode(new GreatSwordMode());//�⺻��� ����
    }


}
