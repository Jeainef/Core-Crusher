using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    private Mesh chunkMesh;

    private void Start() {
        Initialize();
    }

    public void Initialize(){
        int vertexIndex=0;
        List<int> triangles = new List<int>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();

        for(int i=0; i<6; i++){
            for(int j=0; j<6; j++){
                int triangleIndex = VoxelData.Triangles[i,j];
                vertices.Add(VoxelData.Vertices[triangleIndex]);
                triangles.Add(vertexIndex);

                uvs.Add(VoxelData.UVs[j]); //Current triangle Index
                vertexIndex++;
                }
            }
        for(int i=0; i<6; i++){
            for(int j=0; j<6; j++){
                int triangleIndex = VoxelData.Triangles[i,j];
                vertices.Add(VoxelData.Vertices[triangleIndex]);
                triangles.Add(vertexIndex);

                uvs.Add(VoxelData.UVs[j]); //Current triangle Index
                vertexIndex++;
                }
            }

            chunkMesh = new Mesh(){
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                uv = uvs.ToArray()
            };
            chunkMesh.RecalculateNormals();
            meshFilter.mesh=chunkMesh;
    }
}
