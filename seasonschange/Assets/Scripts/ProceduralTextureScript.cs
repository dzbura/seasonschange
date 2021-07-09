using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // used for Sum of array

public class ProceduralTextureScript : MonoBehaviour {
    public float snowHeight;
    public float rockSlope;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    float transition(float start, float end,float value) {
        if (start > end) {
            return 1 - transition(end, start, value);
        }
        if (value < start) {
            return 0;
        }
        if (value >= end) {
            return 1;
        }
        var scaledValue = (value - start) / (end - start);
        return scaledValue;
    }

    public void runProcedrualTexturing() {
        var terrain = gameObject.GetComponent<Terrain>();
        var layers = terrain.terrainData.alphamapLayers;
        var height = terrain.terrainData.alphamapWidth;
        var width = terrain.terrainData.alphamapHeight;


        Debug.Log(layers);
        Debug.Log(height);
        Debug.Log(width);

        Debug.Log(terrain.terrainData.GetInterpolatedHeight(0.5f,0.5f));

        var newSplatMap = new float[width, height, layers];
        //for (int i=0; i<)
        
        for (int i=0; i < width;i++) {
            for (int j = 0; j < width; j++) {
                float x = j / (float)height;
                float y = i / (float)width;
                var splatWeights = new float[layers];
                var terrainHeight = terrain.terrainData.GetInterpolatedHeight(x,y);

                for (int k = 0; k < layers; k++) {
                    splatWeights[k] = Random.RandomRange(0.0f,1.0f);
                }



                float sum = splatWeights.Sum();

                for (int k = 0; k < layers; k++) {
                    newSplatMap[i,j,k]=splatWeights[k]/sum;
                }
            }
        }
        terrain.terrainData.SetAlphamaps(0, 0, newSplatMap);

        var a = terrain.terrainData.treeInstances;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
