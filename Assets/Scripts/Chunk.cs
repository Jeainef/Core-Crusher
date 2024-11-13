using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    List<int> triangles = new List<int>();
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    int vertexIndex=0;
    private Mesh chunkMesh;

    private void Start() {
        Initialize();
    }


    public void Initialize(){
        for(int x=0;x<VoxelData.ChunkWidth; x++){
            for(int y=0;y<VoxelData.ChunkHeight; y++){
                for(int z=0;z<VoxelData.ChunkWidth; z++){
                    AddVoxelData(new Vector3(x,y,z));
                }
            }
        }
        CreateMesh();
            
    }
    private void AddVoxelData(Vector3 pos){
        
        for(int i=0; i<6; i++){
            for(int j=0; j<6; j++){
                int triangleIndex = VoxelData.Triangles[i,j];
                vertices.Add(VoxelData.Vertices[triangleIndex] + pos);
                triangles.Add(vertexIndex);

                uvs.Add(VoxelData.UVs[j]); //Current triangle Index
                vertexIndex++;
                }
            }
    }
    private  void CreateMesh(){
            chunkMesh = new Mesh(){
                vertices = vertices.ToArray(),
                triangles = triangles.ToArray(),
                uv = uvs.ToArray()
            };
            chunkMesh.RecalculateNormals();
            meshFilter.mesh=chunkMesh;
    }
}
