using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> legalTiles;

    [SerializeField]
    public int[] tileType;
    public List<GameObject> GetLegalTiles() {
        return legalTiles;
    }

    public int[] GetTileType() {
        return tileType;
    }
}
