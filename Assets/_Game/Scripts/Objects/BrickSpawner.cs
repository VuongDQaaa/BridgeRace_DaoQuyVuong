using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask brickLayer;
    public bool isSpawned;

    void Awake()
    {
        isSpawned = false;
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
