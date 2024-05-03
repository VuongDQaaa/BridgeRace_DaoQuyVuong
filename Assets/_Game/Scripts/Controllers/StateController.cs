using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<BrickSpawner> brickSpawners;
    [SerializeField] private List<GameObject> brickPrefabs;

    public List<GameObject> newBrick = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFirtTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBrick()
    {
        foreach (var brickSpawner in brickSpawners)
        {
            if (brickSpawner.isSpawned == false)
            {
                Vector3 spawnPostion = brickSpawner.myPostion;
                spawnPostion.y = 0.2f;
                int randomPrefabs = Random.Range(0, brickPrefabs.Count);
                Debug.Log("spawn");
                newBrick.Add(Instantiate(brickPrefabs[randomPrefabs], spawnPostion, Quaternion.identity, parent));
            }
        }
    }

    IEnumerator SpawnFirtTime()
    {
        yield return new WaitForSeconds(1);
        SpawnBrick();
    }
}
