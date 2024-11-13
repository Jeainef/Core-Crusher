using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    [SerializeField] public  int ChunkWidth=12;
    [SerializeField] public  int ChunkHeight=12;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    List<int> triangles = new List<int>();
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();

    VoxelType[,,] chunkBlockData;  //Very inneficient, should refactor later

    int vertexIndex=0;
    private Mesh chunkMesh;

    private void Start() {
        chunkBlockData=new VoxelType[ChunkWidth,ChunkHeight,ChunkWidth];
        Initialize();
    }


    public void Initialize(){

        AddChunkVoxelData();
        for(int x=0;x<ChunkWidth; x++){
            for(int y=0;y<ChunkHeight; y++){
                for(int z=0;z<ChunkWidth; z++){
                    AddVoxelData(new Vector3(x,y,z));
                }
            }
        }
        CreateMesh();
            
    }
    private void AddVoxelData(Vector3 pos){
        
        

        for(int i=0; i<6; i++){
            Vector3 neighbourPosition= pos+VoxelData.NeighbourVoxelPos[i];
                if(FaceVisible(pos,neighbourPosition)){
                    for(int j=0; j<6; j++){

                        int triangleIndex = VoxelData.Triangles[i,j];
                        vertices.Add(VoxelData.Vertices[triangleIndex] + pos);
                        triangles.Add(vertexIndex);

                        uvs.Add(VoxelData.UVs[j]); //Current triangle Index
                        vertexIndex++;
                    }
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
    private bool FaceVisible(Vector3 CurrentVoxelPos,Vector3 NeighbourVoxelPos){

        if(chunkBlockData[(int)CurrentVoxelPos.x,(int)CurrentVoxelPos.y,(int)CurrentVoxelPos.z] == VoxelType.Air) return false;
        //Face index order is stored in VoxelData
        if (OutsideChunkBorder(NeighbourVoxelPos)) //Is outside the chunk borders
        {
            Debug.Log("its outside");
            return true;
        }
        if(chunkBlockData[(int)NeighbourVoxelPos.x,(int)NeighbourVoxelPos.y,(int)NeighbourVoxelPos.z] != VoxelType.Solid) return true;
        return false; 

    }
    private bool OutsideChunkBorder(Vector3 pos){

        if(pos.x<0 || pos.x>=ChunkWidth) return true;
        if(pos.y<0 || pos.y>=ChunkHeight) return true;
        if(pos.z<0 || pos.z>=ChunkWidth) return true;
        else return false;
    }
    private void AddChunkVoxelData(){
        
        float radius=10;

        for(int x=0;x<ChunkWidth; x++){
            for(int y=0;y<ChunkHeight; y++){
                for(int z=0;z<ChunkWidth; z++){
                    
                        chunkBlockData[x,y,z]=VoxelType.Solid;
                    
                }
            }
        }
    }
}
