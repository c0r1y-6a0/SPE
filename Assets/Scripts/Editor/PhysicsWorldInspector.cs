using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SPE.PhysicsWorld))]
public class PhysicsWorldInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SPE.PhysicsWorld world = target as SPE.PhysicsWorld;
        world.MaxResolverIterations = EditorGUILayout.IntField("Max Resolver Iterations", world.MaxResolverIterations);
    }
}
