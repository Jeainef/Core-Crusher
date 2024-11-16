using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;

    [SerializeField] private int ChunkRenderDistance;
     [SerializeField] private int ChunkWorldSize;
    [SerializeField] public int ChunkWidth=12;
    [SerializeField] public readonly int ChunkHeight=12;
    [SerializeField] public BlockData[] blocks;
    private Dictionary<Vector3,Chunk> chunks_saved=new Dictionary<Vector3,Chunk> ();
    public Material material;

    // Start is called before the first frame update
    void Start()
    {

        for(int i=-ChunkRenderDistance/2;i<ChunkRenderDistance/2;i++){
            for(int j=-ChunkRenderDistance/2;j<ChunkRenderDistance/2;j++){
                Vector3 chunkPos = new Vector3(i,0,j);
                Chunk chunk;
                if (chunks_saved.TryGetValue(chunkPos, out chunk))
                {
                    //Make it visible 
                }
                else
                {
                    chunks_saved.Add(chunkPos,new Chunk(new ChunkCoords(i,j),this));
                }
                
            }
        }
        
    }

    public byte  GetVoxelType( Vector3 voxelPosition){
        Debug.Log(voxelPosition);

        if(!IsVoxelInWorld(voxelPosition)) return 0;
        if(voxelPosition.y>ChunkHeight-1 || voxelPosition.y<0) return 0;
        return 1;
    }






    public bool IsChunkInWorld(ChunkCoords pos){
        return (pos.x>0 && pos.x<ChunkRenderDistance && pos.y>0 && pos.y<ChunkRenderDistance );
    }
    public bool IsVoxelInWorld(Vector3 pos){
        return (pos.x>=-ChunkRenderDistance/2*ChunkWidth  && pos.x<ChunkRenderDistance/2*ChunkWidth && pos.z>=-ChunkRenderDistance/2*ChunkWidth  && pos.z<ChunkRenderDistance/2*ChunkWidth);
    }
}
