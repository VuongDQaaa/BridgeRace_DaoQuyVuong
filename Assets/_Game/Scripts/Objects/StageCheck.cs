using UnityEngine;

public class StageCheck : MonoBehaviour
{
    public StageController stageController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            stageController.stageEntered = true;
            stageController.characters.Add(other.transform.GetComponent<Character>());
        }
    }

}
