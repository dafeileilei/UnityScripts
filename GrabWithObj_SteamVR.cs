
 /*
 * 2020-02-10
 * Written By Yeliheng
 * 基于SteamVR的原生物体抓取
 * 可投掷
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabWithObj_SteamVR : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	[SerializeField]private string grabbleObj = "Grabable";//可抓取物体的标签
	private ulong gripButton = SteamVR_Controller.ButtonMask.Grip;//Grip按键定义
	private SteamVR_Controller.Device device;//控制器
	private GameObject currentObj; //当前交互的游戏对象
	private Color grabableMat = Color.green;//可抓取时物体的材质
	private Color unGrabableMat = Color.red;//不可抓取
	public bool isToss = true;//是否允许被投掷
	void Awake () {
		trackedObject = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate () {
		if (trackedObject == null)
			return;
		device = SteamVR_Controller.Input((int)trackedObject.index);
		if (device == null)
			return;
		//按下按钮抓取物体
		if (device.GetPressDown(gripButton))
		{

			objPick();
		}
		if (device.GetPressUp(gripButton))
		{
			objRelease();
		}
	}

	/*
	* 物体被抓取逻辑
	* */
	private void objPick()
	{
		if (currentObj != null)
		{	
			currentObj.transform.parent = transform;//让交互物体成为当前控制器的子物体
														 //材质的相关操作
			Material material = currentObj.GetComponentInChildren<MeshRenderer>().material;
			material.color = Color.white;
			//刚体的相关操作
			Rigidbody rigidbody = currentObj.GetComponent<Rigidbody>();
			rigidbody.useGravity = false;
			rigidbody.isKinematic = true;//不受外力影响
			//currentObj.transform.localPosition = Vector3.zero;//从中心点处抓取
		}
	}
	/*
	* 物体被释放逻辑
	* */
	private void objRelease()
	{
		if (currentObj != null) { 
		currentObj.transform.parent = null;//不再是任何物体的子物体
		 //恢复刚体属性
		Rigidbody rigidbody = currentObj.GetComponent<Rigidbody>();
		rigidbody.useGravity = true;
		rigidbody.isKinematic = false;
			if (isToss)
			{
				//设置速度
				rigidbody.velocity = device.velocity*5;
				//设置角速度
				rigidbody.angularVelocity = device.angularVelocity*5;
			}
		}
	}

	//触发器事件
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.tag == grabbleObj)
		{
			Material material = other.gameObject.GetComponentInChildren<MeshRenderer>().material;
			currentObj = other.gameObject;
			material.color = grabableMat;
			device.TriggerHapticPulse(5000);//震动
		}
		else
		{
			Material material = other.gameObject.GetComponentInChildren<MeshRenderer>().material;
			material.color = unGrabableMat;
			device.TriggerHapticPulse(5000);//震动
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other != null)
		{
			Material material = other.gameObject.GetComponentInChildren<MeshRenderer>().material;
			material.color = Color.white;
			currentObj = null;
		}
	}
}
