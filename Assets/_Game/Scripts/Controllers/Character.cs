using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private Renderer meshRenderer;
    public ColorType color;

    public void ChangeColor(ColorType colorType)
    {
        meshRenderer.material = colorData.GetMat(colorType);
    }

    private void Awake()
    {
        ChangeColor(color);
    }
}

