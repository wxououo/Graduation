
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float rotateSpeed = 2.0f; // 环视速度
    public float pitchSpeed = 2.0f; // 俯仰速度
    public float minPitch = -60.0f; // 最小俯仰角度
    public float maxPitch = 75.0f; // 最大俯仰角度

    private Vector3 targetPosition;
    private Vector3 lastMousePosition;

    void Start()
    {
        // 计算房间的中心点位置
        targetPosition = CalculateRoomCenter();
    }
    void Update()
    {
        // 检测鼠标按下
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

       // 检测鼠标拖动
        if (Input.GetMouseButton(0))
        {
            // 计算鼠标移动量
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // 计算摄像机的旋转角度
            float rotateY = delta.x * rotateSpeed * Time.deltaTime;
            float rotateX = -delta.y * pitchSpeed * Time.deltaTime;

            // 围绕房间中心点旋转摄像机
            transform.RotateAround(targetPosition, Vector3.up, rotateY);

            // 控制摄像机的俯仰角度
            float newPitch = transform.eulerAngles.x + rotateX;
            newPitch = Mathf.Clamp(newPitch, minPitch, maxPitch);
            transform.rotation = Quaternion.Euler(newPitch, transform.eulerAngles.y, transform.eulerAngles.z);

            // 更新上一帧的鼠标位置
            lastMousePosition = Input.mousePosition;
        }
    }
    // 计算房间的中心点位置
    private Vector3 CalculateRoomCenter()
    {
        // 获取场景中所有的房间物体
        GameObject[] roomObjects = GameObject.FindGameObjectsWithTag("Room");
        Vector3 center = Vector3.zero;

        // 计算所有房间物体的平均位置
        foreach (GameObject roomObject in roomObjects)
        {
            center += roomObject.transform.position;
        }
        center /= roomObjects.Length;

        return center;
    }
}
