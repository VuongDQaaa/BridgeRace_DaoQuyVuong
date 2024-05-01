using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private MeshRenderer meshRenderer;
    public ColorType brickColor;

    private void Awake()
    {
        meshRenderer.material = colorData.GetMat(brickColor);
    }
}
