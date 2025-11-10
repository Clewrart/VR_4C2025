using UnityEngine;
using OVR;

public class test_input_get : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        // 检测右手柄A键
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("A键按下");
        }

        // 检测左手扳机键力度
        float triggerValue = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);
        if (triggerValue > 0.1f)
        {
            Debug.Log("左扳机键力度: " + triggerValue);
        }
    }
}
