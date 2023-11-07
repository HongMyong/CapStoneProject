using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterController;
using Unity.VisualScripting;

[RequireComponent(typeof(Tester))]
public class TesterController : MonoBehaviour
{
    public Tester tester {  get; private set; }
    public Vector2 inputDirection {  get; private set; }
    public Vector2 calculatedDirection { get; private set; }
    public Vector2 gravity { get; private set; }

    [Header("��� ���� �˻�")]
    [SerializeField, Tooltip("����� �� �ִ� �ִ� ��� �����Դϴ�.")]
    float maxSlopeAngle;
    [SerializeField, Tooltip("��縦 üũ�� Raycast �߻� ���� �����Դϴ�.")]
    Transform raycastOrigin;

    private const float RAY_DISTANCE = 2f;
    private RaycastHit slopeHit;
    private bool isOnSlope;

    [Header("�� üũ")]
    [SerializeField, Tooltip("���� �پ� �ִ��� Ȯ���ϱ� ���� CheckBox ���� ����")]
    Transform groundCheck;
    private int groundLayer;
    private bool isGrounded;

    private void Start()
    {
        tester=GetComponent<Tester>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
    }
}
