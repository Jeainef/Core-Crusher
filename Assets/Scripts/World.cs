using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Vector3 spawnPoint = new Vector3(0,0,0);
    [SerializeField] private int ChunkRenderDistance;
     [SerializeField] private int ChunkWorldSize;
    [SerializeField] public int ChunkWidth=12;
    [SerializeField] public readonly int ChunkHeight=12;
    [SerializeField] private Texture2D terrainTexture;
    [SerializeField] public BlockData[] blocks;
    private Dictionary<string,Chunk> chunks_saved=new Dictionary<string,Chunk> ();
    private List<ChunkCoords> activeChunks= new List<ChunkCoords> ();

  
    public Material material;


    private ChunkCoords PlayerCurrentChunk;
    private ChunkCoords PlayerPreviousChunk;
    // Start is called before the first frame update
    void Start()
    {
        if(terrainTexture==null) terrainTexture= NoiseGenerator.BasicNoise(1028,1028);


        playerPosition.position=spawnPoint;
        PlayerCurrentChunk=ChunkCoordFromWorldPos(playerPosition.position);
        GenerateWorld();
        
    }
    private void Update() {
        PlayerPreviousChunk=PlayerCurrentChunk;
        PlayerCurrentChunk=ChunkCoordFromWorldPos(playerPosition.position);
        if(!PlayerCurrentChunk.Equals(PlayerPreviousChunk)){
            CheckRenderDistance();
        }
    }
    public void GenerateWorld(){
        for(int i=-ChunkRenderDistance/2;i<=ChunkRenderDistance/2;i++){
            for(int j=-ChunkRenderDistance/2;j<=ChunkRenderDistance/2;j++){
                ChunkCoords chunkPos = new ChunkCoords(i,j);
                Chunk currentChunk=new Chunk(chunkPos,this);
                chunks_saved.Add(chunkPos.Code,currentChunk);
                activeChunks.Add(chunkPos);
            }
        }
    }
    public void CheckRenderDistance(){
        List<ChunkCoords> previouslyActiveChunks=new List<ChunkCoords>(activeChunks);

        for(int i=PlayerCurrentChunk.x -ChunkRenderDistance/2;i<=PlayerCurrentChunk.x + ChunkRenderDistance/2;i++){
            for(int j=PlayerCurrentChunk.y-ChunkRenderDistance/2;j<=PlayerCurrentChunk.y + ChunkRenderDistance/2;j++){
                ChunkCoords chunkPos = new ChunkCoords(i,j);
                Chunk chunk;
                if (chunks_saved.TryGetValue(chunkPos.Code, out chunk))
                {
                  
                         //Make it visible 
                    if(!chunk.IsActive){
                        chunk.IsActive=true;
                        activeChunks.Add(chunkPos);
                    }

               
                }
                else
                {
                    chunk=new Chunk(new ChunkCoords(i,j),this);
                    chunks_saved.Add(chunkPos.Code,chunk);
                    activeChunks.Add(chunkPos);
                }

                for(int w=0; w<previouslyActiveChunks.Count;w++){
                    if(previouslyActiveChunks[w].Equals(chunkPos)){
                        previouslyActiveChunks.RemoveAt(w);
                    }
                }

                
            }

        }
        foreach (ChunkCoords c in previouslyActiveChunks){

            chunks_saved[c.Code].IsActive = false;
            activeChunks.Remove(c);
                    
        }
    }
    public byte  GetVoxelType( Vector3 voxelPosition){

        if(!IsVoxelInWorld(voxelPosition)) return 0;
        if(voxelPosition.y>ChunkHeight-1 || voxelPosition.y<0) return 0;
        return 1;
    }



    public bool IsChunkInRenderDistance(ChunkCoords pos){
        ChunkCoords playerChunk = ChunkCoordFromWorldPos(playerPosition.position);
        int distance_x = Mathf.Abs(playerChunk.x-pos.x);
        int distance_z = Mathf.Abs(playerChunk.y-pos.y);
        return distance_x< ChunkRenderDistance/2 && distance_z<ChunkRenderDistance;

    }

    public ChunkCoords ChunkCoordFromWorldPos(Vector3 pos){
        int x= (int) pos.x/ChunkWidth;
        int z= (int) pos.z/ChunkWidth;
        return new ChunkCoords(x,z);
    }
    public bool IsChunkInWorld(ChunkCoords pos){
        return (pos.x>0 && pos.x<ChunkRenderDistance && pos.y>0 && pos.y<ChunkRenderDistance );
    }
    public bool IsVoxelInWorld(Vector3 pos){
        return (pos.x>=-ChunkWorldSize/2*ChunkWidth  && pos.x<ChunkWorldSize/2*ChunkWidth && pos.z>=-ChunkWorldSize/2*ChunkWidth  && pos.z<ChunkWorldSize/2*ChunkWidth);
    }

}
