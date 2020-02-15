using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 2020-02-15
 * SteamVR控制器事件模板
 * Written By Yeliheng
 */
public class SteamVRControllerEventsTemplate : MonoBehaviour {

	// Use this for initialization
	SteamVR_TrackedObject trackedObject;
	void Start () {
		trackedObject = GetComponent<SteamVR_TrackedObject>();

	}
	
	// Update is called once per frame
	void Update () {
		if (trackedObject == null)
			return;
		int index = (int)trackedObject.index;
		SteamVR_Controller.Device device = SteamVR_Controller.Input(index);
		if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
		{
			Debug.Log("菜单键被按下!");
		}
	}
}
