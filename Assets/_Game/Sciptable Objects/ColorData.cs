using UnityEngine;

public enum ColorType
{
    None = 0,
    Red = 1,
    Blue = 2,
    Yellow = 3,
    Green = 4,
    Pink = 5
}
[CreateAssetMenu(menuName = "ColorData")]
public class ColorData : ScriptableObject
{

    [SerializeField] private Material[] materials;

    public Material GetMat(ColorType colorType)
    {
        return materials[((int)colorType)];
    }
}

