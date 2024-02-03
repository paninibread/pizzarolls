using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum Orientation
    {
        North,
        East,
        West,
        South
    }
    public struct Cube
    {
        public int Value { get; set; }
        public Orientation Orientation { get; set; }
        public float Angle
        {
            get
            {
                // You can adjust this logic based on your specific angle representation
                switch (Orientation)
                {
                    case Orientation.South:
                        return 0f;
                    case Orientation.West:
                        return 90f;
                    case Orientation.North:
                        return 180f;
                    case Orientation.East:
                        return 270f;
                    default:
                        throw new InvalidOperationException("Invalid orientation");
                }
            }
        }
        public Cube(int value, string orientation)
        {
            Value = value;
            Orientation = ConvertToOrientation(orientation);
        }
        private static Orientation ConvertToOrientation(string orientation)
        {
            switch (orientation.ToUpper())
            {
                case "N":
                    return Orientation.North;
                case "E":
                    return Orientation.East;
                case "W":
                    return Orientation.West;
                case "S":
                    return Orientation.South;
                default:
                    throw new ArgumentException("Invalid orientation");
            }
        }

    }
    public GameObject tileContainer;
    public GameObject waterVolume;
    public Tile[] tileTypes;
    public Cube[,,] tiles;
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
        //everything faces south by default, for diagonal blocks SW is south
        //floor size
        mapSizeX = 12; mapSizeY = 12;
        //height
        mapSizeZ = 3;
        tiles = new Cube[mapSizeZ, mapSizeX, mapSizeY];//tiles[height, x coord, y coord]
        int x, y, z;
        //empty
        for(z = 0; z < mapSizeZ; z++) for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++) tiles[z, x, y] = new Cube(0, "S");

        /*//grass
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
            }*/

        /*for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++)
            {
                tiles[0, x, y] = 2;
            }
        tiles[0, 0, 0] = 1;
        tiles[0, 0, 1] = 1;
        tiles[0, 1, 0] = 1;
        tiles[0, 1, 1] = 1;
        tiles[0, 1, 2] = 1;
        tiles[0, 1, 3] = 1;
        tiles[0, 1, 7] = 1;
        tiles[0, 1, 8] = 1;
        tiles[0, 2, 3] = 1;
        tiles[0, 2, 3] = 1;
        tiles[0, 2, 7] = 1;
        tiles[0, 2, 8] = 1;
        tiles[0, 3, 3] = 1;
        tiles[0, 3, 4] = 1;
        tiles[0, 3, 5] = 1;
        tiles[0, 3, 6] = 1;
        tiles[0, 3, 7] = 1;
        tiles[0, 4, 3] = 1;
        tiles[0, 4, 4] = 1;
        tiles[0, 4, 5] = 1;
        tiles[0, 5, 2] = 1;
        tiles[0, 5, 3] = 1;
        tiles[0, 5, 4] = 1;
        tiles[0, 5, 5] = 1;
        tiles[0, 6, 2] = 1;
        tiles[0, 6, 5] = 1;
        tiles[0, 7, 1] = 1;
        tiles[0, 7, 2] = 1;
        tiles[0, 7, 5] = 1;
        tiles[0, 7, 6] = 1;
        tiles[0, 7, 7] = 1;
        tiles[0, 7, 8] = 1;
        tiles[0, 8, 1] = 1;
        tiles[0, 8, 2] = 1;
        tiles[0, 8, 8] = 1;
        tiles[0, 8, 9] = 1;
        tiles[0, 9, 8] = 1;
        tiles[0, 9, 9] = 1;*/

        for (x = 0; x < mapSizeX; x++) 
        { 
            tiles[0, x, 0] = new Cube(1, "S"); 
            tiles[0, x, mapSizeY-1] = new Cube(1, "S"); 
        }
        for (x = 0; x < 8; x++) 
        { 
            tiles[0, x, 3] = new Cube(1, "S"); 
            tiles[0, x, 8] = new Cube(1, "S"); 
        }
        for (y = 0; y < mapSizeY; y++)
        {
            tiles[0, mapSizeX-1, y] = new Cube(1, "S");
            tiles[0, 0, y] = new Cube(1, "S");
        }
        for (y = 3; y < 9; y++)
        {
            tiles[0, 7, y] = new Cube(1, "S");
        }
        for (x = 0; x < 7; x++) for (y = 4; y < 8; y++)
            {
                tiles[0, x, y] = new Cube(2, "S");
            }
        for (x = 0; x < mapSizeX; x++)
        {
            tiles[1, x, 0] = new Cube(1, "S");
            tiles[1, x, mapSizeY - 1] = new Cube(1, "S");
        }
        for (y = 0; y < mapSizeY; y++)
        {
            tiles[1, mapSizeX - 1, y] = new Cube(1, "S");
        }
        tiles[1, 0, 1] = new Cube(1, "S");
        tiles[1, 0, 10] = new Cube(1, "S");
        for (x = 0; x < 8; x++)
        {
            tiles[1, x, 2] = new Cube(3, "N");
            tiles[1, x, 9] = new Cube(3, "S");
        }
        for (y = 3; y < 9; y++)
        {
            tiles[1, 8, y] = new Cube(3, "W");
        }
        tiles[1, 8, 2] = new Cube(4, "W");
        tiles[1, 8, 9] = new Cube(4, "S");
        for (x = 0; x < mapSizeX; x++)
        {
            tiles[2, x, 0] = new Cube(1, "S");
            tiles[2, x, mapSizeY - 1] = new Cube(1, "S");
        }
        for (y = 0; y < mapSizeY; y++)
        {
            tiles[2, mapSizeX - 1, y] = new Cube(1, "S");
            tiles[2, mapSizeX - 2, y] = new Cube(1, "S");
        }
        for (x = 0; x < 9; x++)
        {
            tiles[2, x, 1] = new Cube(3, "N");
            tiles[2, x, 10] = new Cube(3, "S");
        }
        for (y = 2; y < 10; y++)
        {
            tiles[2, 9, y] = new Cube(3, "W");
        }
        tiles[2, 9, 1] = new Cube(5, "W");
        tiles[2, 9, 10] = new Cube(5, "S");
    }
    public void generateMapVisuals()
    {
        tilesOnMap = new GameObject[mapSizeZ, mapSizeX, mapSizeY];        
        int index;
        int x, y, z;
        float angle;
        for (z = 0; z < mapSizeZ; z++) for (x = 0; x < mapSizeX; x++) for (y = 0; y < mapSizeY; y++) 
                {
                    index = tiles[z, x, y].Value;
                    angle = tiles[z, x, y].Angle;
                    GameObject newTile = Instantiate(tileTypes[index].tileVisualPrefab, new Vector3(x, z, y), Quaternion.Euler(new Vector3(0, angle, 0)));
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
