using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] private GameObject charBrickPrefab;
    [SerializeField] private Transform root;
    protected List<GameObject> myBrick = new List<GameObject>();
    protected bool isValidStep = true;
    public ColorType myColor;

    private void Awake()
    {
        meshRenderer.material = colorData.GetMat(myColor);
    }

    private void Update()
    {
        InteractWithStep();
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

    protected void InteractWithStep()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Step"))
            {
                Step step = hit.transform.GetComponent<Step>();
                if (step.stepColor != myColor)
                {
                    if(myBrick.Count > 0)
                    {
                        isValidStep = true;
                        RemoveCharacterBrick(step);
                    }
                    else
                    {
                        isValidStep = false;
                    }
                }
                else
                {
                    isValidStep = true;
                }
            }
        }
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
        if (other.CompareTag("Finish"))
        {
            ClearCharacterBrick();
        }
    }


}

