using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { start, playing, win, loose }
    [Header("Required elements")]
    [SerializeField] private GameObject mapPrefabs;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject botPrefab;
    public List<Transform> startPositons;

    [Header("Atributes")]
    public GameState currentGameState;
    public ColorType playerColor;
    private ColorType[] color = { ColorType.Red, ColorType.Blue, ColorType.Green, ColorType.Pink, ColorType.Yellow };
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

    public void ChangeCharacterColor(ColorType selectedColor, Character player)
    {
        Character character = player.GetComponent<Character>();
        if (character != null)
        {
            character.myColor = selectedColor;
            character.UpdateCharacterColor(selectedColor);
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

        //Spawn characters
        for (int i = 0; i <= color.Length - 1; i++)
        {
            if (color[i] != playerColor)
            {
                //Spawn bot
                GameObject newBot = Instantiate(botPrefab, startPositons[i].position, Quaternion.identity);
                ChangeCharacterColor(color[i], newBot.GetComponent<Character>());
            }
            else
            {
                //Spawn player
                GameObject spawnedPlayer = Instantiate(playerPrefab, startPositons[i].position, Quaternion.identity);
                ChangeCharacterColor(playerColor, spawnedPlayer.GetComponent<Character>());
                CameraController.Instance.SetTarGet(spawnedPlayer);
            }
        }
    }
}
