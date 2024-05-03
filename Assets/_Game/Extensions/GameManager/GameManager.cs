using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { start, playing, win, loose }
    [Header("Required elements")]
    [SerializeField] private GameObject mapPrefabs;
    [SerializeField] private GameObject playerPrefab;

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
            SpawnGameObjects();
        }
    }

    private void SpawnGameObjects()
    {
        //Spawn map
        GameObject currentMap = Instantiate(mapPrefabs);
        //Spawn player
        GameObject spawnPos = currentMap.transform.Find("Start Position").gameObject;

        if (spawnPos != null)
        {
            GameObject spawnedPlayer = Instantiate(playerPrefab, spawnPos.transform.position, Quaternion.identity);
            ChangePlayerColor(playerColor, spawnedPlayer);
            CameraController.Instance.SetTarGet(spawnedPlayer);
        }
        else
        {
            Debug.Log("spawn pos not found");
        }

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

    private void SpawnBot()
    { }

}
