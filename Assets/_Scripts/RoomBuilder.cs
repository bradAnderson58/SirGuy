using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoomBuilder : MonoBehaviour {

    // .63 instead of .64 to have a tiny overlap - looks better
    public static float FLOOR_WIDTH = 0.63f;
    public static float HALF_WIDTH = 0.31f;
    public static float SIDE_WIDTH = 0.1f;
    public static float FRONT_WIDTH = 0.14f;

    // size and place specs for this room
    public float roomTop;
    public float roomLeft;
    public int tilesRight;
    public int tilesDown;

    // declare the sprites to use for this room type
    public Transform floorTile1;
    public Transform floorTile2;
    public Transform sideWall;
    public Transform backWall;
    public Transform frontWall;
    public Transform leftCorner;
    public Transform rightCorner;

	// Use this for initialization
	void Start () {

        // first build the floor based on the tile settings
        float currentLeft = roomLeft;
        float currentTop = roomTop;
        bool first = true;
        for (int row = 0; row < tilesRight; ++row) {
            for (int col = 0; col < tilesDown; ++col) {

                // add walls around the edges of the room (if needed)
                addWalls(row, col, currentLeft, currentTop);

                // floor tile
                Instantiate((first ? floorTile1: floorTile2), new Vector3(currentLeft, currentTop, 0), Quaternion.identity);
                currentTop -= FLOOR_WIDTH;
                first = !first;
            }
            currentLeft += FLOOR_WIDTH;
            currentTop = roomTop;
            first = !first;
        }
	}

    // method contains logic for handling wall creation
    void addWalls(int row, int column, float xPos, float yPos) {

        // first, set up the edgeWall x position and bottom corner transform
        float edgeWall = (row == 0) ? xPos - (HALF_WIDTH + SIDE_WIDTH) :
            (row == (tilesRight - 1)) ? xPos + (HALF_WIDTH + SIDE_WIDTH) : 0;
        Transform corner = (row == 0) ? leftCorner : rightCorner;

        // top of the column
        if (column == 0) {
            Instantiate(backWall, new Vector3(xPos, yPos + (FLOOR_WIDTH + HALF_WIDTH), 0), Quaternion.identity);
            // top corners
            if (row == 0 || row == (tilesRight - 1)) {
                Instantiate(sideWall, new Vector3(edgeWall, yPos + FLOOR_WIDTH, 0), Quaternion.identity);
                Instantiate(sideWall, new Vector3(edgeWall, yPos + (FLOOR_WIDTH *2), 0), Quaternion.identity);
            }
        // bottom of the column
        } else if (column == (tilesDown - 1)) {
            Instantiate(frontWall, new Vector3(xPos, yPos - (HALF_WIDTH + FRONT_WIDTH), 0), Quaternion.identity);
            // bottom corners
            if (row == 0 || row == (tilesRight - 1))
                Instantiate(corner, new Vector3(edgeWall, yPos - (HALF_WIDTH + FRONT_WIDTH), 0), Quaternion.identity);
        }

        // start and end of rows
        if (row == 0)
            Instantiate(sideWall, new Vector3(xPos - (HALF_WIDTH + SIDE_WIDTH), yPos, 0), Quaternion.identity);
        else if (row == (tilesRight - 1))
            Instantiate(sideWall, new Vector3(xPos + (HALF_WIDTH + SIDE_WIDTH), yPos, 0), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
