using UnityEngine;

[CreateAssetMenu(menuName = "MapData")]
public class MapData : ScriptableObject
{
    public int id;
    [SerializeField] private GameObject mapPrefabs;

    public GameObject GetMap()
    {
        return mapPrefabs;
    }
}
