using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using OpenCover.Framework.Model;

public class StateController : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private List<BrickSpawner> brickSpawners;
    [SerializeField] private List<GameObject> brickPrefabs;
    [SerializeField] public List<GameObject> characters;
    public bool stateEntered;
    public List<GameObject> spawnedBricks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        stateEntered = false;
        StartCoroutine(SpawnBrick());
    }

    //return number of spawned brick same color
    private int CountBrickByColor(GameObject brick)
    {
        int brickNum = 0;
        if (spawnedBricks.Count > 0)
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
        GameObject foundPrefabs = new GameObject();
        for (int i = 0; i <= brickPrefabs.Count; i++)
        {
            if (brickPrefabs[i].GetComponent<Brick>().brickColor == brickColor)
            {
                foundPrefabs = brickPrefabs[i];
                break;
            }
        }
        return foundPrefabs;
    }

    private void SpawnBrick(Vector3 postion, GameObject brickPrefab)
    {
        Vector3 spawnPosition = postion;
        spawnPosition.y = 0.2f;

        //Debug.Log("spawn" + validBrick.name);
        GameObject newBrick = Instantiate(brickPrefab, spawnPosition, Quaternion.identity, parent);
        //Update spawn place for new brick
        Brick spawnedBrick = newBrick.GetComponent<Brick>();
        spawnedBrick.spawnedState = transform.GetComponent<StateController>();
        //Add new brick in this state List<spawnedBrick>
        spawnedBricks.Add(newBrick);
    }


    IEnumerator SpawnBrick()
    {
        yield return new WaitForSeconds(1);
        //check character in character list
        if (characters.Count > 0)
        {
            //spawn brick for each character in list
            for (int i = 0; i <= characters.Count - 1; i++)
            {
                //get brick prefab same color with character
                GameObject validBrick = FindBrickPrefabInList(characters[i].GetComponent<Character>().myColor);

                //count current spawned brick same color with player
                int brickNum = CountBrickByColor(validBrick);
                if (brickNum >= brickSpawners.Count / characters.Count)
                {
                    continue;
                }
                else
                {
                    //spawn
                    
                }
            }
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnBrick());
    }
}
