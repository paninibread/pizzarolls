using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    public Tile[] tileTypes;
    public int[,,] tiles;
    public GameObject[,,] tilesOnMap;
    //[Header("Board Size")]
    int mapSizeX;
    int mapSizeY;
    int mapSizeZ;
    public GameObject tileContainer;
    // Start is called before the first frame update
    void Start()
    {
        generateMapInfo();
        generateMapVisuals();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void generateMapInfo()
    {
        //floor size
        mapSizeX = 10; mapSizeY = 10;
        //height
        mapSizeZ = 5;
        tiles = new int[mapSizeZ, mapSizeX, mapSizeY];//tiles[height, x coord, y coord]
        int x, y, z;
        //empty
        for(z = 0; z < mapSizeZ; z++) for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++) tiles[z, x, y] = 0;

        //grass
        for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++) 
            { 
                tiles[0, x, y] = 1; tiles[4, x, y] = 1; 
            }
        
    }
    public void generateMapVisuals()
    {
        tilesOnMap = new GameObject[mapSizeZ, mapSizeX, mapSizeY];        
        int index;
        int x, y, z;
        for (z = 0; z < mapSizeZ; z++) for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++) 
                {
                    index = tiles[z, x, y];
                    GameObject newTile = Instantiate(tileTypes[index].tileVisualPrefab, new Vector3(x, z, y), Quaternion.identity);
                    //newTile.GetComponent<Tile>().tileX = x;
                    //newTile.GetComponent<Tile>().tileY = y;
                    //newTile.GetComponent<Tile>().tileZ = z;
                    newTile.GetComponent<Tile>().tileArrayPos = new Vector3(z, x, y);
                    newTile.GetComponent<Tile>().map = this;
                    newTile.transform.SetParent(tileContainer.transform);
                    tilesOnMap[z, x, y] = newTile;                
                }
    }
}
