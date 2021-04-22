using UnityEngine;
using UnityEditor;

using SPE;
[CustomEditor(typeof(SPE.Spring))]
public class SpringInspector : Editor
{
    public override void OnInspectorGUI()
    {
        SPE.Spring sp = target as SPE.Spring;

        sp.K = float.Parse(EditorGUILayout.TextField("K", sp.K.ToString()));
        sp.RestLength = float.Parse(EditorGUILayout.TextField("Rest Length", sp.RestLength.ToString()));
        sp.A = EditorGUILayout.ObjectField("A", sp.A, typeof(Particle), true) as Particle;
        sp.B = EditorGUILayout.ObjectField("B", sp.B, typeof(Particle), true) as Particle;

    }
}