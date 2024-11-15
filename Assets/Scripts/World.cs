using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{


    [SerializeField] private int ChunkRenderDistance;
     [SerializeField] private int ChunkWorldSize;
    [SerializeField] public int ChunkWidth=12;
    [SerializeField] public readonly int ChunkHeight=12;

    public Chunk[,] chunks;
    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        chunks=new Chunk[ChunkRenderDistance,ChunkRenderDistance];

        for(int i=-ChunkRenderDistance/2;i<ChunkRenderDistance/2;i++){
            for(int j=-ChunkRenderDistance/2;j<ChunkRenderDistance/2;j++){
               chunks[i+ChunkRenderDistance/2,j+ChunkRenderDistance/2] = new Chunk(new ChunkCoords(i,j),this);
            }
        }
        
    }
    public VoxelType  GetVoxelType( Vector3 voxelPosition){
        Debug.Log(voxelPosition);
        if(!IsVoxelInWorld(voxelPosition)) return VoxelType.Air;
        if(voxelPosition.y>ChunkHeight-1 || voxelPosition.y<0) return VoxelType.Air;
        return VoxelType.Solid;
    }
    public bool IsChunkInWorld(ChunkCoords pos){
        return (pos.x>0 && pos.x<ChunkRenderDistance && pos.y>0 && pos.y<ChunkRenderDistance );
    }
    public bool IsVoxelInWorld(Vector3 pos){
        return (pos.x>=-ChunkRenderDistance/2*ChunkWidth  && pos.x<ChunkRenderDistance/2*ChunkWidth && pos.z>=-ChunkRenderDistance/2*ChunkWidth  && pos.z<ChunkRenderDistance/2*ChunkWidth);
    }
}
