using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using System;

public class SeasonsController : MonoBehaviour
{
	int season;
	Tuple<int,Vector3>[] treePlacements = new Tuple<int, Vector3>[]{
		new Tuple<int, Vector3> (1, new Vector3(25,0,25)),
		new Tuple<int, Vector3> (2, new Vector3(35,0,25)),
		new Tuple<int, Vector3> (3, new Vector3(45,0,25)),
		new Tuple<int, Vector3> (3, new Vector3(25,0,35)),
		new Tuple<int, Vector3> (1, new Vector3(35,0,35)),
		new Tuple<int, Vector3> (2, new Vector3(45,0,35)),
		new Tuple<int, Vector3> (2, new Vector3(25,0,45)),
		new Tuple<int, Vector3> (3, new Vector3(35,0,45)),
		new Tuple<int, Vector3> (1, new Vector3(45,0,45)),
		new Tuple<int, Vector3> (3, new Vector3(25,5,60)),
		new Tuple<int, Vector3> (2, new Vector3(35,5,60)),
		new Tuple<int, Vector3> (1, new Vector3(45,5,60))
	};
	int[,] detailMap;

    void Start()
    {
		detailMap = Terrain.activeTerrain.terrainData.GetDetailLayer(0, 0, Terrain.activeTerrain.terrainData.detailWidth, Terrain.activeTerrain.terrainData.detailHeight, 0);
		season = -5;
		var parent = GameObject.Find("trees");
		foreach (Tuple<int, Vector3> placement in treePlacements) {
			Instantiate(
			(GameObject)AssetDatabase.LoadAssetAtPath(
				"Assets/Prefabs/gotowe drzewa/0" + placement.Item1.ToString() + ".prefab", 
				typeof(GameObject)), 
			placement.Item2, 
			Quaternion.identity, parent.transform);
		
		}
	}

    void Update()
    {
		if (season > 48) {
			season = 10;
		} else {
			season += 1;
		}
		if (season%10==0){
			InstantiateTrees(season/10);
		}
    }
	
	void InstantiateTrees(int season) {
		var objects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		var parent = GameObject.Find("trees");
		if (parent != null) {
			foreach (Transform tree in parent.transform)
				Destroy(tree.gameObject);
		}

		foreach (Tuple<int, Vector3> placement in treePlacements) {
			var placementWithHeiht = new Vector3( placement.Item2.x, Terrain.activeTerrain.SampleHeight(placement.Item2), placement.Item2.z);
			var newObj = Instantiate(
				(GameObject)AssetDatabase.LoadAssetAtPath(
					"Assets/Prefabs/gotowe drzewa/" + season.ToString() + placement.Item1.ToString() + ".prefab",
					typeof(GameObject)), 
				placementWithHeiht,
				Quaternion.identity, parent.transform); 
		}
		if (season == 4) {
			toggleGrass(false);
		} else if (season == 1) {
			toggleGrass(true);
		}
		
	}
	
	void toggleGrass(Boolean enabled) {
		
		var terrain = Terrain.activeTerrain;
		if (enabled) { 
			terrain.terrainData.SetDetailLayer(0, 0, 0, detailMap);
			UpdateTerrainTexture(terrain.terrainData, 6, 4);
		} else {
			var zeroes = terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);
			for (int y = 0; y < terrain.terrainData.detailHeight; y++) {
				for (int x = 0; x < terrain.terrainData.detailWidth; x++) {
				   zeroes[x, y] = 0;
				}
			}
			terrain.terrainData.SetDetailLayer(0, 0, 0, zeroes);
			UpdateTerrainTexture(terrain.terrainData, 4, 6);
		}
	}
	
	static void UpdateTerrainTexture(TerrainData terrainData, int textureNumberFrom, int textureNumberTo) {
		float[, ,] alphas = terrainData.GetAlphamaps(0, 0, terrainData.alphamapWidth, terrainData.alphamapHeight);
        for (int i = 0; i < terrainData.alphamapWidth; i++)
        {
            for (int j = 0; j < terrainData.alphamapHeight; j++)
            {
                alphas[i, j, textureNumberTo] = Mathf.Max(alphas[i, j, textureNumberFrom], alphas[i, j, textureNumberTo]);
                alphas[i, j, textureNumberFrom] = 0f;
            }
        }
        terrainData.SetAlphamaps(0, 0, alphas);
    }
}
