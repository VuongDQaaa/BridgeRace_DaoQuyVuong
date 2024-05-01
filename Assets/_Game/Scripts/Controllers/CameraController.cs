using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform target;
    //[SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        GameObject otherObject = GameObject.Find("Player");
        if (otherObject != null)
        {
            target = otherObject.transform;
        }
        else
        {
            Debug.Log("Not find player");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = desiredPosition;

            transform.LookAt(target.position);
        }
    }
}
