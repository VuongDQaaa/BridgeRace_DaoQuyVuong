using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.currentGameState = GameManager.GameState.win;
            GameManager.Instance.Win();
        }
        if (other.CompareTag("Bot"))
        {
            GameManager.Instance.currentGameState = GameManager.GameState.loose;
            GameManager.Instance.Loose();
        }
    }
}
