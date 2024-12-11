using Crogen.AttributeExtension;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NavmeshBaker : MonoBehaviour
{
    [SerializeField] private Tilemap _baseMap;
    [SerializeField] private Tilemap _outsideMap;
    [SerializeField] private TileBase _outsideTile;
    
    private NavMeshSurface _navMeshSurface;
    
    [Button("Bake Navmesh!", 20)]
    public void Bake()
    {
        if (_navMeshSurface == null)
            _navMeshSurface = GetComponent<NavMeshSurface>();
        
        if (_baseMap == null)
        {
            Debug.LogError("Base Map is null");
            return;
        }

        if (_outsideMap == null)
        {
            Debug.LogError("Background Map is null");
            return;
        }

        if (_outsideTile == null)
        {
            Debug.LogError("Background Tile is null");
            return;
        }
        
        _baseMap.CompressBounds();
        _outsideMap.ClearAllTiles();

        for (int i = _baseMap.cellBounds.xMin - 1 ; i < _baseMap.cellBounds.xMax + 1; i++)
        {
            for (int j = _baseMap.cellBounds.yMin - 1; j < _baseMap.cellBounds.yMax + 1; j++)
            {
                if (_baseMap.GetTile(new Vector3Int(i, j, 0))== null)
                {
                    _outsideMap.SetTile(new Vector3Int(i, j, 0), _outsideTile);
                }
            }
        }
        _navMeshSurface.BuildNavMesh();
        print("Back Complete!");
    }
}
