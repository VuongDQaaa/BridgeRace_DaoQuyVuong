using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("Spawn postion and brick prefab")]
    [SerializeField] private List<BrickSpawner> brickSpawners;
    [SerializeField] private List<GameObject> brickPrefabs;

    [Header("Current character in sate")]
    [SerializeField] public List<Character> characters;

    [Header("Bricks in this state")]
    public List<GameObject> spawnedBricks = new List<GameObject>();

    [Header("Spawn parent for brick")]
    public bool stageEntered = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnBrick());
    }

    void Update()
    {

    }

    //return number of spawned brick same color in List<GameObject> spawnedBrick
    private int CountBrickByColor(ColorType color)
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
                if (spawnedBricks[i].GetComponent<Brick>().brickColor == color)
                {
                    brickNum++;
                }
            }
        }
        return brickNum;
    }

    private void GenarateBrick(GameObject brickPrefab, Vector3 postion)
    {
        GameObject newBrick = Instantiate(brickPrefab, postion, Quaternion.identity, transform);
        //Update spawn place for new brick
        newBrick.GetComponent<Brick>().spawnedState = transform.GetComponent<StageController>();
        //Add new brick in this state List<spawnedBrick>
        spawnedBricks.Add(newBrick);
    }

    IEnumerator SpawnBrick()
    {
        yield return new WaitForSeconds(1);
        if (stageEntered == true)
        {
            //Get all valid spawn positions (isSpawned == false)
            List<BrickSpawner> randomSpawnPostion = brickSpawners.FindAll(x => x.isSpawned == false);
            //Mix the positions list
            RandomizeList<BrickSpawner>(randomSpawnPostion);
            //The limit of each type of brick
            int bricksPerColor = brickSpawners.Count / 5;

            foreach (Character item in characters)
            {
                //count current brick same color with character on stage
                int currentBrickNum = CountBrickByColor(item.myColor);

                if (currentBrickNum < bricksPerColor)
                {
                    for (int i = 0; i < bricksPerColor - currentBrickNum; i++)
                    {   
                        //get brick prefab
                        GameObject brickPrefab = brickPrefabs.Find(x => x.GetComponent<Brick>().brickColor == item.myColor);
                        //spawn brick 
                        GenarateBrick(brickPrefab, randomSpawnPostion[0].brickPosition);
                        //remove the spawn postion in list
                        randomSpawnPostion.RemoveAt(0);
                    }
                }
            }
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(SpawnBrick());
    }

    private void RandomizeList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
