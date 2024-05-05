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
    protected List<GameObject> myBrick = new List<GameObject>();
    public ColorType myColor;
    protected bool isMoveable, isOnstair;

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
                    if (myBrick.Count > 0)
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
        GameObject newCharacterBrick = Instantiate(charBrickPrefab, root);
        myBrick.Add(newCharacterBrick);
        //change brick myColor
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
        brickPos.y = brickPos.y + myBrick.Count * 0.2f;
        newCharacterBrick.transform.position = brickPos;
    }

    private void RemoveCharacterBrick(Step step)
    {
        //change step color
        step.stepColor = myColor;
        //remove 1 brick in list
        Destroy(myBrick[myBrick.Count - 1]);
        myBrick.RemoveAt(myBrick.Count - 1);
    }

    private void ClearCharacterBrick()
    {
        foreach (GameObject currentCharacterBrick in root.transform)
        {
            Destroy(currentCharacterBrick);
        }
        myBrick.Clear();
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
        if(other.CompareTag("Finish"))
        {
            ClearCharacterBrick();
        }
    }


}

