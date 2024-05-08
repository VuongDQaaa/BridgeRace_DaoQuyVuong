using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("Spawn postion and brick prefab")]
    [SerializeField] private List<BrickSpawner> brickSpawners;
    [SerializeField] private List<GameObject> brickPrefabs;

    [Header("Current character in sate")]
    [SerializeField] public List<Character> characters;

    [Header("Bricks in this state")]
    public List<GameObject> spawnedBricks = new List<GameObject>();

    [Header("Spawn parent for brick")]
    [SerializeField] private Transform parent;
    public bool stateEntered;

    // Start is called before the first frame update
    void Start()
    {
        stateEntered = false;
        StartCoroutine(SpawnBrickControl());
    }

    //return number of spawned brick same color in List<GameObject> spawnedBrick
    private int CountBrickByColor(GameObject brick)
    {
        int brickNum = 0;
        if (spawnedBricks.Count <= 0)
        {
            brickNum = 0;
        }
        else
        {
            for (int i = 0; i <= spawnedBricks.Count - 1; i++)
            {
                if (spawnedBricks[i].GetComponent<Brick>().brickColor == brick.GetComponent<Brick>().brickColor)
                {
                    brickNum++;
                }
            }
        }
        return brickNum;
    }

    //Find brick prefabs same color with given color
    private GameObject FindBrickPrefabInList(ColorType brickColor)
    {
        GameObject foundedBrickPrefab = new GameObject();
        for (int i = 0; i <= brickPrefabs.Count; i++)
        {
            if (brickPrefabs[i].GetComponent<Brick>().brickColor == brickColor)
            {
                foundedBrickPrefab = brickPrefabs[i];
                break;
            }
        }
        return foundedBrickPrefab;
    }

    private void SpawnBrick(GameObject brickPrefab, Vector3 postion)
    {
        Vector3 spawnPosition = postion;
        spawnPosition.y = 0.2f;

        GameObject newBrick = Instantiate(brickPrefab, spawnPosition, Quaternion.identity, parent);
        //Update spawn place for new brick
        Brick spawnedBrick = newBrick.GetComponent<Brick>();
        spawnedBrick.spawnedState = transform.GetComponent<StateController>();
        //Add new brick in this state List<spawnedBrick>
        spawnedBricks.Add(newBrick);
    }

    private List<BrickSpawner> ValidSpawnPostions()
    {
        List<BrickSpawner> validSpawnPos = new List<BrickSpawner>();
        foreach (BrickSpawner item in brickSpawners)
        {
            if (item.isSpawned == false)
            {
                validSpawnPos.Add(item);
            }
        }
        return validSpawnPos;
    }


    IEnumerator SpawnBrickControl()
    {
        if (stateEntered == true)
        {
            yield return new WaitForSeconds(1);
            //check character in character list
            if (characters.Count > 0)
            {
                //spawn brick for each character in list
                for (int i = 0; i <= characters.Count - 1; i++)
                {
                    //get brick prefab same color with character
                    GameObject validBrick = FindBrickPrefabInList(characters[i].myColor);

                    //count current spawned brick same color with player
                    int brickNum = CountBrickByColor(validBrick);
                    if (brickNum >= brickSpawners.Count / characters.Count)
                    {
                        continue;
                    }
                    else
                    {
                        while (brickNum < brickSpawners.Count / characters.Count)
                        {
                            //get all spawn postion with isSpawned = false
                            List<BrickSpawner> currentSpawnPos = ValidSpawnPostions();
                            //select random spawn position in above list
                            int randomIndex = Random.Range(0, currentSpawnPos.Count - 1);
                            //spawn brick
                            SpawnBrick(validBrick, currentSpawnPos[randomIndex].transform.position);
                            brickNum++;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnBrickControl());
    }
}
