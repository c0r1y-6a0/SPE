using UnityEngine;
using UnityEditor;

using SPE;
[CustomEditor(typeof(Spring))]
public class SpringInspector : Editor
{
    public override void OnInspectorGUI()
    {
        Spring sp = target as Spring;

        sp.K = float.Parse(EditorGUILayout.TextField("K", sp.K.ToString()));
        sp.RestLength = float.Parse(EditorGUILayout.TextField("Rest Length", sp.RestLength.ToString()));
        sp.A = EditorGUILayout.ObjectField("A", sp.A, typeof(Particle), true) as Particle;
        sp.B = EditorGUILayout.ObjectField("B", sp.B, typeof(Particle), true) as Particle;

        sp.Bungee = EditorGUILayout.Toggle("Bungee", sp.Bungee);
    }
}