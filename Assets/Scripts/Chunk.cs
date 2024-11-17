using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChunkCoords{
    public int x;
    public int y;

    public ChunkCoords(int _x, int _y){
        x=_x;
        y=_y;
    }
    public bool Equals(ChunkCoords other)
    {
        if(other==null) return false;
        else if(x==other.x && y==other.y) return true;
        else return false;
    }
    public string Code{
        get{return x + "_" + y;}
    }
}
public class Chunk 
{

    private GameObject ChunkObject;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    List<int> triangles = new List<int>();
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();

    BlockData[,,] chunkBlockData;  //Very inneficient, should refactor later

    public int vertexIndex=0;
    private Mesh chunkMesh;
    //public bool isActive;

    ChunkCoords ChunkPosition;
    World world;
    public Chunk(ChunkCoords position,World _world){
        ChunkPosition=position;
        world=_world;

        ChunkObject = new GameObject();
        ChunkObject.transform.position=ChunkToWorldPos();
        ChunkObject.transform.SetParent(world.transform);
        ChunkObject.name = "Chunk_"+ChunkPosition.x+"_"+ChunkPosition.y;
        meshFilter= ChunkObject.AddComponent<MeshFilter>();
        meshRenderer= ChunkObject.AddComponent<MeshRenderer>();
        meshRenderer.material=world.material;
        
        chunkBlockData=new BlockData[world.ChunkWidth,world.ChunkHeight,world.ChunkWidth];


        Initialize();
    }

    public void Initialize(){
        AddChunkVoxelData();
        for(int x=0;x<world.ChunkWidth; x++){
            for(int y=0;y<world.ChunkHeight; y++){
                for(int z=0;z<world.ChunkWidth; z++){
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

                    //Set Vertices and UVs
                    for(int j=0; j<4; j++){

                        int triangleIndex = VoxelData.Triangles[i,j];
                        vertices.Add(VoxelData.Vertices[triangleIndex] + pos);
             
                        uvs.Add(VoxelData.UVs[j]); //Current triangle Index

                       
                    }
                    //Set Triangles
                    for(int tri=0;tri < 6;tri++){
                        triangles.Add(vertexIndex+VoxelData.TriangleOrder[tri]);

                    }
                    vertexIndex+=4;
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
        if(!CheckVoxel(CurrentVoxelPos).isSolid) return false;
        if(!CheckVoxel(NeighbourVoxelPos).isSolid) return true;
        return false;

    }
    private bool OutsideChunkBorder(Vector3 pos){

        if(pos.x<0 || pos.x>=world.ChunkWidth) return true;
        if(pos.y<0 || pos.y>=world.ChunkHeight) return true;
        if(pos.z<0 || pos.z>=world.ChunkWidth) return true;
        else return false;
    }
    private void AddChunkVoxelData(){
        
        for(int x=0;x<world.ChunkWidth; x++){
            for(int y=0;y<world.ChunkHeight; y++){
                for(int z=0;z<world.ChunkWidth; z++){
                    
                   // chunkBlockData[x,y,z]=world.GetVoxelType(VoxelToWorldPos(new Vector3(x,y,z)));
                    chunkBlockData[x,y,z]= world.blocks[world.GetVoxelType(VoxelToWorldPos(new Vector3(x,y,z)))];
                }
            }
        }
    }
    private BlockData CheckVoxel(Vector3 pos){
        if(!OutsideChunkBorder(pos)) return chunkBlockData[(int)pos.x,(int)pos.y,(int)pos.z];
        return world.blocks[world.GetVoxelType(VoxelToWorldPos(pos))];
    }
    private Vector3 VoxelToWorldPos(Vector3 voxelPos){
        return voxelPos + ChunkObject.transform.position;
    }
    private Vector3 ChunkToWorldPos(){
        return new Vector3(ChunkPosition.x*world.ChunkWidth,0,ChunkPosition.y*world.ChunkWidth);
    }
    public bool IsActive{
        get{
            return ChunkObject.activeSelf;}
        set{ChunkObject.SetActive(value);}
    }


    

}
