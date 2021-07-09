using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(GrassController))]
public class GrassEditor : Editor {


    public override void OnInspectorGUI() {

        GrassController terrainController = (GrassController)target;

        DrawDefaultInspector();
        if (GUILayout.Button("Reset")) {
            terrainController.resetGrass();
        }
    }
}
