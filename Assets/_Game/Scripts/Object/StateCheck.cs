using UnityEngine;

public class StateCheck : MonoBehaviour
{
    //[SerializeField] private StateController stateController;
    [SerializeField] private StateController stateController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Bot"))
        {
            stateController.stateEntered = true;
            stateController.characters.Add(other.transform.GetComponent<Character>());
        }
    }

}
