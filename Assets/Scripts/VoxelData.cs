using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VoxelType{
    Air,Solid
}
public static class VoxelData
{


    public static readonly Vector3[] Vertices = new Vector3[8]{
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(1.0f,0.0f,0.0f),
        new Vector3(1.0f,1.0f,0.0f),
        new Vector3(0.0f,1.0f,0.0f),
        new Vector3(0.0f,0.0f,1.0f),
        new Vector3(1.0f,0.0f,1.0f),
        new Vector3(1.0f,1.0f,1.0f),
        new Vector3(0.0f,1.0f,1.0f)
    } ;

    
    /*
        IMPORTANT: INDEX OF THE ARRAY MATTERS:
        0: Back
        1: Front
        2: Top
        3: Bottom
        4: Left
        5: Right
     */

    public static readonly Vector3[] NeighbourVoxelPos = new Vector3[6]{
        new Vector3(0.0f,0.0f,-1.0f), //Back Voxel
        new Vector3(0.0f,0.0f,1.0f), //Front Voxel
        new Vector3(0.0f,1.0f,0.0f), //Top Voxel
        new Vector3(0.0f,-1.0f,0.0f), //Bottom Voxel
        new Vector3(-1.0f,0.0f,0.0f), //Left Voxel
        new Vector3(1.0f,0.0f,0.0f) //Right Voxel
    };

    public static readonly int[] TriangleOrder= new int[6]{
        0,1,2,2,1,3
    };
    public static readonly int[,] Triangles = new int[6,4]{
          {0, 3, 1, 2}, // Back Face

         {5, 6, 4, 7}, // Front Face

         {3, 7, 2, 6}, // Top Face

         {1, 5, 0, 4}, // Bottom Face

         {4, 7, 0, 3}, // Left Face

         {1, 2, 5, 6}  // Right Face

    };

    public static readonly Vector2[] UVs = new Vector2[6]{

        new Vector2(0.0f,0.0f),
        new Vector2(0.0f,1.0f),
        new Vector2(1.0f,1.0f),
        new Vector2(1.0f,0.0f),
        new Vector2(0.0f,1.0f),
        new Vector2(1.0f,1.0f)
    };
}
