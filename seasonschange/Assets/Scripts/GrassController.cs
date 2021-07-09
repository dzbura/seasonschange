using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

struct DifferentialEquationData {
    public float water;
    public float vegetation;
}


public class GrassController : MonoBehaviour {
    int _structSize() {
        return sizeof(float) + sizeof(float);
    }
    private GameObject cube;

    public float updateInterval;
    float timePassed;
    Terrain terrain;
    public ComputeShader computeShader;

    [Header("equation coeffs")]
    public float DWater = 1.0f;
    public float DVegetation = 0.5f;
    public float feedRate = 0.055f;
    public float killRate = 0.0462f;
    public float dT = 0.10f;
    public int stepsNumber = 1;


    int sizeX;
    int sizeY;
    // Start is called before the first frame update
    void Start() {
        terrain = gameObject.GetComponent<Terrain>();

        sizeX = terrain.terrainData.detailWidth;
        sizeY = terrain.terrainData.detailWidth;

        initComputeShader();
    }


    bool doUpdate() {
        timePassed += Time.deltaTime;
        if (timePassed < updateInterval) {
            return false;
        }
        else {
            timePassed = 0;
            return true;
        }

    }

    void initKillFeedBuffers() {
        //tutaj stworz
    }
    public void initBufferValues() {

        // tutaj uzupelnij wartosci buforow


    }

    public void resetGrass() {
        initBufferValues();
    }
    void initParameters() {
        //tutaj zainicjalizuj parametry
    }
    void initComputeShader() {
        //tutaj zainicjalizuj compute shader


        initKillFeedBuffers();
        initBufferValues();
        initParameters();
    }


    void swapBuffers() {
        //tutaj zamien bufory ze soba
    }
    void doSimulationSteps(int steps) {
        //tutaj wykonaj kroki symulacji
    }

    void recoverData() {
        //tutaj odczytaj bufor i przeslij wynik jako mapa detali

    }
    // Update is called once per frame
    void Update() {
        if (doUpdate()) {
            terrain = gameObject.GetComponent<Terrain>();
            initParameters();
            doSimulationSteps(stepsNumber);
            recoverData();
        }
    }
}
