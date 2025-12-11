using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;        // 따라갈 대상
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -4f); // 기준 위치
    [SerializeField] private float rotateSpeedX = 120f;
    [SerializeField] private float rotateSpeedY = 80f;
    [SerializeField] private float distanceSmooth = 0.05f;

    private float minPitch = -40f;
    private float maxPitch = 80f;

    private float yaw;
    private float pitch;

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        if (target == null) return;

        // 마우스 입력
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * rotateSpeedX * Time.deltaTime;
        pitch -= mouseY * rotateSpeedY * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // 회전된 오프셋 적용
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = target.position + rotation * offset;

        // 부드럽게 이동 (Lerp)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, distanceSmooth);

        // 타겟 바라보기
        transform.LookAt(target.position + Vector3.up * 1.5f);
    }
}
