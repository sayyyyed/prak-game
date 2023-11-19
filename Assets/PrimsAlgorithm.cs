using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismAlgorithm : MazeLogic
{
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public override void GenerateMaps() {

        int x = 2;
        int z = 2;
        map[x, z] = 0;

        List<MapLocation> walls = new List<MapLocation>();
        walls.Add(new MapLocation(x + 1, z));
        walls.Add(new MapLocation(x - 1, z));
        walls.Add(new MapLocation(x, z + 1));
        walls.Add(new MapLocation(x, z - 1));

        int countloops = 0;
        while (walls.Count > 0 && countloops < 5000) {
            int rwall = Random.Range(0, walls.Count);
            x = walls[rwall].x;
            z = walls[rwall].z;
            walls.RemoveAt(rwall);
            if (CountSquareNeighbours(x,z) == 1) {
                map[x, z] = 0;
                walls.Add(new MapLocation(x + 1, z));
                walls.Add(new MapLocation(x - 1, z));
                walls.Add(new MapLocation(x, z + 1));
                walls.Add(new MapLocation(x, z - 1));
            }
            countloops++;
        }

        // for (int z = 0; z < depth; z++) {
            //     for (int x = 0; x < width; x++) {
        //         if (Random.Range(0, 100) < 50) {
        //             map[x, z] = 0;
        //         }
        //     }
        // }
    }
}