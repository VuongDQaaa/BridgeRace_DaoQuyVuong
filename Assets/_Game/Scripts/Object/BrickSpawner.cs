using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    public bool isSpawned;
    public Vector3 brickPosition;

    void Awake()
    {
        isSpawned = false;
        brickPosition = transform.position;
        brickPosition.y = 0.3f;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, 1.1f, brickLayer))
        {
            isSpawned = true;
        }
        else
        {
            isSpawned = false;
        }
    }
}
