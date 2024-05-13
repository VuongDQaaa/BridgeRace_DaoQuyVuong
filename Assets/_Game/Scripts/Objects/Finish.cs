using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel() + 1;
            LevelManager.Instance.SaveCurrentLevel(currentLevel);

            GameManager.Instance.currentGameState = GameManager.GameState.win;
            StartCoroutine(Win());
        }

        if (other.CompareTag("Bot"))
        {
            Debug.Log("Loose");
            GameManager.Instance.currentGameState = GameManager.GameState.loose;
            StartCoroutine(Loose());
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.Win();
    }

    IEnumerator Loose()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.Loose();
    }
}
