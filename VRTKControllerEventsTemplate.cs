using UnityEngine;
using System.Collections;
using VRTK;
public class VRTKControllerEventsUtil : MonoBehaviour
{

    public VRTK_ControllerEvents controllerEvents;

    //注册事件
    void OnEnable() {
        controllerEvents.TriggerPressed += ControllerEvents_TriggerPressed;
    }

    //注销
    void OnDisable() {
        controllerEvents.TriggerPressed -= ControllerEvents_TriggerPressed; 
    }

    //事件详细处理逻辑....
    void ControllerEvents_TriggerPressed(object sender, ControllerInteractionEventArgs e)
    {
        print("Trigger Pressed !!");
    }
}
