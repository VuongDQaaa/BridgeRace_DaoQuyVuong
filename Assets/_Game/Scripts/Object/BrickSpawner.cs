using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    public bool isSpawned;
    public Vector3 myPostion;

    void Awake()
    {
        isSpawned = false;
    }
    // Update is called once per frame
    void Update()
    {
        myPostion = transform.position;
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
