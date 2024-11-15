using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{


    [SerializeField] private int ChunkRenderDistance;
    [SerializeField] public int ChunkWidth=12;
    [SerializeField] public readonly int ChunkHeight=12;

    public Chunk[,] chunks = new Chunk[6,6];
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<6;i++){
            for(int j=0;j<6;j++){
               chunks[i,j] = new Chunk(new ChunkCoords(i,j),this);
            }
        }
        
    }
    public VoxelType  GetVoxelType( Vector3 voxelPosition){
        Debug.Log(voxelPosition);
        if(!IsVoxelInWorld(voxelPosition)) return VoxelType.Air;
        return VoxelType.Solid;
    }
    public bool IsChunkInWorld(ChunkCoords pos){
        return (pos.x>0 && pos.x<ChunkRenderDistance && pos.y>0 && pos.y<ChunkRenderDistance );
    }
    public bool IsVoxelInWorld(Vector3 pos){
        return (pos.x>0 && pos.x<ChunkRenderDistance*ChunkWidth-1 && pos.z>0 && pos.z<ChunkRenderDistance*ChunkWidth-1);
    }
}
