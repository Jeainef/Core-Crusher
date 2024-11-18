using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseGenerator
{
    
    private static float Random(int x,int y){
        float dot= Vector2.Dot(new Vector2(x,y),new Vector2(32.23232f,72.3232f));
        float sinVal= Mathf.Sin(dot)*723232.232f;
        return sinVal - Mathf.Floor(sinVal);

    }

    public static Texture2D BasicNoise(int width, int height){
        Texture2D noiseTexture= new Texture2D(width,height);

        for(int x=0;x<width;x++){
            for(int y=0;y<width;y++){
                noiseTexture.SetPixel(x,y,new Vector4(Random(x,y),Random(x,y),Random(x,y),1));
            }
        }
        noiseTexture.Apply();
        return noiseTexture;
    }
    
}
