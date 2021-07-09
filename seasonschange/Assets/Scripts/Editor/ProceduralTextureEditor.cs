using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine;
using System.Collections;


[CustomEditor(typeof(ProceduralTextureScript))]
public class ProceduralTextureEditor : Editor {


    public override void OnInspectorGUI() {


        DrawDefaultInspector();
        if (GUILayout.Button("Run")) {
            ((ProceduralTextureScript)target).runProcedrualTexturing();
        }
    }
}
