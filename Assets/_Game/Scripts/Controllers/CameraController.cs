using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform currentTarget;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        if (currentTarget != null)
        {
            Vector3 desiredPosition = currentTarget.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            transform.LookAt(currentTarget.position);
        }
    }

    public void SetTarGet(GameObject target)
    {
        currentTarget = target.transform;
    }

    public void DeleteTarget()
    {
        currentTarget = null;
    }
}
