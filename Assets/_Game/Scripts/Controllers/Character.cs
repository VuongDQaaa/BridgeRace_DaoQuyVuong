using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum AnimationState { idle, runing, win }
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private GameObject charBrickPrefab;
    [SerializeField] private Transform root;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stepLayer;
    public List<GameObject> myBricks = new List<GameObject>();
    public ColorType myColor;
    [SerializeField] protected StageController currentStage;
    protected bool isMoveable, isOnstair;
    public bool isFinished = false;

    private void Awake()
    {
        isMoveable = true;
        isOnstair = false;
    }

    public void UpdateCharacterColor(ColorType newColor)
    {
        meshRenderer.material = colorData.GetMat(myColor);
    }

    public Vector3 CheckGrounded(Vector3 nextPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPos, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.2f;
        }
        return transform.position;
    }

    protected void CheckStepColor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, stepLayer))
        {
            isOnstair = true;
            Step step = hit.collider.GetComponent<Step>();
            if (step != null)
            {
                if (step.stepColor != myColor)
                {
                    if (myBricks.Count > 0)
                    {
                        isMoveable = true;
                        RemoveCharacterBrick(step);
                    }
                    else
                    {
                        isMoveable = false;
                    }
                }
                else
                {
                    isMoveable = true;
                }
            }
            else
            {
                Debug.Log("step not found");
            }
        }
        else
        {
            isOnstair = false;
        }
    }

    private void AddCharacterBrick()
    {
        //spawn character brick
        GameObject newCharacterBrick = Instantiate(charBrickPrefab, root);
        myBricks.Add(newCharacterBrick);
        //change brick color
        MeshRenderer brickMeshRenderer = newCharacterBrick.GetComponent<MeshRenderer>();
        if (brickMeshRenderer != null)
        {
            brickMeshRenderer.material = colorData.GetMat(myColor);
        }
        else
        {
            Debug.Log("no mesh renderer founded");
        }
        //update chacter brick postion;
        Vector3 brickPos = root.transform.position;
        brickPos.y = brickPos.y + myBricks.Count * 0.2f;
        newCharacterBrick.transform.position = brickPos;
    }

    private void RemoveCharacterBrick(Step step)
    {
        //change step color
        step.stepColor = myColor;
        //remove 1 brick in list
        Destroy(myBricks[myBricks.Count - 1]);
        myBricks.RemoveAt(myBricks.Count - 1);
    }

    private void ClearCharacterBrick()
    {
        if (myBricks.Count > 0)
        {
            foreach (GameObject currentCharacterBrick in myBricks)
            {
                Destroy(currentCharacterBrick);
            }
            myBricks.Clear();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick currentBrick = other.GetComponent<Brick>();
            if (currentBrick.brickColor == myColor)
            {
                Destroy(other.gameObject);
                AddCharacterBrick();
            }
        }
        if (other.CompareTag("Stage"))
        {
            currentStage = other.GetComponent<StageCheck>().stageController;
        }

        if (other.CompareTag("Finish"))
        {
            ClearCharacterBrick();
            isFinished = true;
        }
    }


}

