using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private MeshRenderer meshRenderer;
    public ColorType stepColor;

    private void Awake()
    {
        ChangeColor(stepColor);
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor(stepColor);
    }

    private void ChangeColor(ColorType colorType)
    {
        meshRenderer.material = colorData.GetMat(colorType);
    }
}
