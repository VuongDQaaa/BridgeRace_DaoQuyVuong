using UnityEngine;

public class StageCheck : MonoBehaviour
{
    //[SerializeField] private StateController stateController;
    [SerializeField] private StageController stageController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            stageController.stageEntered = true;
            stageController.characters.Add(other.transform.GetComponent<Character>());
        }
    }

}
