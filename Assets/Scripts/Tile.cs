using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Tile : MonoBehaviour
{
    //public int tileX;
    //public int tileY;
    //public int tileZ;
    public Vector3 tileArrayPos;
    public GameObject tileVisualPrefab;
    public int tileType;
    public float movementCost = 1;
    public bool isWalkable=true;
    public MapGenerator map;
}
