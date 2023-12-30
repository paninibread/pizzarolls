using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tileContainer;
    public GameObject waterVolume;
    public Tile[] tileTypes;
    public int[,,] tiles;
    public GameObject[,,] tilesOnMap;
    int mapSizeX;
    int mapSizeY;
    int mapSizeZ;
    void Start()
    {
        generateMapInfo();
        generateMapVisuals();
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

        //water
        for (x = mapSizeX/2; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++)
            {
                tiles[0, x, y] = 2;
            }
        for (x = 0; x < mapSizeX/2; x++) for (y = mapSizeY/2; y < mapSizeY; y++)
            {
                tiles[0, x, y] = 2;
            }
        for (x = 0+1; x < mapSizeX-1; x++) for (y = 0+1; y < mapSizeY-1; y++)
            {
                tiles[4, x, y] = 2;
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
                    newTile.GetComponent<Tile>().tileType = index;
                    newTile.GetComponent<Tile>().map = this;
                    if (newTile.GetComponent<Tile>().tileType == 2) newTile.transform.SetParent(waterVolume.transform);
                    else newTile.transform.SetParent(tileContainer.transform);
                    tilesOnMap[z, x, y] = newTile;                
                }
    }
}
