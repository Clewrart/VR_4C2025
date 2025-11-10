using UnityEngine;

public class RotateObject : MonoBehaviour
{
    //每秒旋转速度（度数/秒）
    //计算旋转速度：360度/（20分钟*60秒/分钟）
    //使物体每20分钟旋转一圈
    private float rotationSpeed = 360f / (20f * 60f);//计算每秒旋转的读书

    void Update()
    {
        //每帧更新时，绕X轴旋转物体
        //旋转的角度是：每秒旋转速度*时间增量（Time.deltaTime）
        transform.Rotate(rotationSpeed * Time.deltaTime, 0f, 0f);
    }
}