using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { start, playing, win, loose }
    [Header("Required elements")]
    [SerializeField] private GameObject mapPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<GameObject> botprefabs;
    public List<Transform> startPositons;

    [Header("Atributes")]
    public GameState currentGameState;
    public ColorType playerColor;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.start;
        UIManager.Instance.OpenUI<CanvasMainMenu>();
    }

    public void StartGame()
    {
        if (currentGameState == GameState.playing)
        {
            SpawnGameElements();
        }
    }

    private void SpawnGameElements()
    {
        //Spawn map
        GameObject currentMap = Instantiate(mapPrefabs);
        //Spawn character
        StartCoroutine(SpawnCharacter(currentMap));
    }

    public void ChangePlayerColor(ColorType selectedColor, GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.myColor = selectedColor;
            playerController.UpdateCharacterColor(selectedColor);
        }
        else
        {
            Debug.Log("can not change color");
        }
    }

    private void MixListOrder(List<Transform> mixedList)
    {
        for (int i = 0; i < mixedList.Count - 1; i++)
        {
            int randomIdex = Random.Range(0, mixedList.Count - 1);
            // swap element
            Transform temp = mixedList[i];
            mixedList[i] = mixedList[randomIdex];
            mixedList[randomIdex] = temp;
        }
    }

    IEnumerator SpawnCharacter(GameObject currentMap)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject startPosParent = currentMap.transform.Find("StartPos").gameObject;
        //get all spawn pos in map
        if (startPosParent != null)
        {
            foreach (Transform startPos in startPosParent.transform)
            {
                startPositons.Add(startPos);
            }
        }
        //mix the start postion oder in list
        MixListOrder(startPositons);

        //Spawn bot
        for (int i = 0; i < startPositons.Count - 1; i++)
        {
            Vector3 postion = startPositons[i].position;
            postion.y = 1;
            Instantiate(botprefabs[i], postion, Quaternion.identity);
        }

        //Spawn player
        GameObject spawnedPlayer = Instantiate(playerPrefab, startPositons[startPositons.Count - 1].position, Quaternion.identity);
        ChangePlayerColor(playerColor, spawnedPlayer);
        CameraController.Instance.SetTarGet(spawnedPlayer);
    }
}
