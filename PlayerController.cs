/**
* 第三人称玩家控制器
* WSAD控制 SHIFT跑
* 可实现走、跑、跳
* 请绑定第三人称摄影机
* Written By Yeliheng
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
	// Inspector
	[SerializeField] private float m_WalkSpeed = 2.0f;//行走速度
	[SerializeField] private float m_RunSpeed = 3.5f;//跑步速度
	[SerializeField] private float m_RotateSpeed = 8.0f;//转弯速度
	[SerializeField] private float m_JumpForce = 200.0f;//弹跳力
	[SerializeField] private float m_RunningStart = 1.0f;//跑步开始

	// member
	private Rigidbody m_RigidBody = null;
	private Animator m_Animator = null;
	private float m_MoveTime = 0;
	private float m_MoveSpeed = 0.0f;
	private bool m_IsGround = true;
	private CharacterController m_Controller;//角色控制组件
	private Transform m_CameraTransform,m_Transform;//摄影机以及玩家的三维坐标
	public Camera playerCamera;//摄影机对象
	private float tmp;
	/*
	 * 初始化
	 */
	private void Awake()
	{
		m_RigidBody = this.GetComponentInChildren<Rigidbody>();
		m_Animator = this.GetComponentInChildren<Animator>();
		m_MoveSpeed = m_WalkSpeed;
	}

	void Start()
    {
		m_Transform = this.transform;
		m_CameraTransform = playerCamera.transform;
		m_Controller = GetComponent<CharacterController>();
		tmp = m_WalkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
		if (null == m_RigidBody) return;
		if (null == m_Animator) return;
		//检查地面
		float rayDistance = 0.3f;
		Vector3 rayOrigin = (this.transform.position + (Vector3.up * rayDistance * 0.5f));
		bool ground = Physics.Raycast(rayOrigin, Vector3.down, rayDistance, LayerMask.GetMask("Default"));//发射一条射线判断是否到达地面
		if (ground != m_IsGround)
		{
			m_IsGround = ground;

			// 着陆动画
			if (m_IsGround)
			{
				m_Animator.Play("landing");
			}
		}
		//输入
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool isMove = ((0 != moveHorizontal) || (0 != moveVertical));
		m_Animator.SetBool("isMove", isMove);
		bool isRun;
		//走和跑
		if (Input.GetKey(KeyCode.LeftShift) && isMove)
		{
			isRun = true;
		}
		else
		{
			isRun = false;
		}
		//跳
		if (Input.GetButtonDown("Jump") && m_IsGround)//判断地面防止n段跳
		{
            if (!isMove)
            {
                m_Animator.Play("jump");
			    m_RigidBody.AddForce(Vector3.up * m_JumpForce);
            }
			
		}
		if (isRun) {
			m_WalkSpeed = m_RunSpeed;
			
		}
        else{
			m_WalkSpeed = tmp;
        }
		m_Animator.SetBool("isRun", isRun);
		//根据摄影机位置决定人物朝向

		if (Input.GetKey(KeyCode.W) && m_IsGround)
			{
				m_Controller.transform.eulerAngles = new Vector3(0, m_CameraTransform.transform.eulerAngles.y, 0);
				m_RigidBody.velocity = m_Controller.transform.forward * m_WalkSpeed;

			}
			if (Input.GetKey(KeyCode.S) && m_IsGround)
			{
				m_Controller.transform.eulerAngles = new Vector3(0, m_CameraTransform.transform.eulerAngles.y + 180f, 0);
				m_RigidBody.velocity = m_Controller.transform.forward * m_WalkSpeed;
			}
			if (Input.GetKey(KeyCode.A) && m_IsGround)
			{
				m_Controller.transform.eulerAngles = new Vector3(0, m_CameraTransform.transform.eulerAngles.y + 270f, 0);
				m_RigidBody.velocity = m_Controller.transform.forward * m_WalkSpeed;
			}
			if (Input.GetKey(KeyCode.D) && m_IsGround)
			{
				m_Controller.transform.eulerAngles = new Vector3(0, m_CameraTransform.transform.eulerAngles.y + 90f, 0);
				m_RigidBody.velocity = m_Controller.transform.forward * m_WalkSpeed;
			}
		}
}
