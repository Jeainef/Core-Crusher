using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{


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
}
