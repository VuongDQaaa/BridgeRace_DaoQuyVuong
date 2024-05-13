using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private MeshRenderer meshRenderer;
    public StageController spawnedStage;
    public ColorType brickColor;

    private void Awake()
    {
        //Get material when awake
        meshRenderer.material = colorData.GetMat(brickColor);
    }

    private void OnDestroy()
    {
        //Remove this when have been destroyed
        spawnedStage.spawnedBricks.Remove(gameObject);
    }
}
