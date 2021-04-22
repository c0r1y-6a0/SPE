using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SPE.Particle))]
public class ParticleInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SPE.Particle particle = target as SPE.Particle;
        particle.Mass = EditorGUILayout.FloatField("Mass", particle.Mass);
    }
}
