using Crogen.AttributeExtension;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Crogen.TileMapBaker
{
    public class TileMapBaker : MonoBehaviour
    {
        [SerializeField] private Tilemap _baseMap;
        [SerializeField] private Tilemap _outsideMap;
        [SerializeField] private TileBase _outsideTile;
        
        [Button("Bake tilemap!", 20)]
        public void Bake()
        {
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
            
            print("Back Complete!");
        }
    }
}