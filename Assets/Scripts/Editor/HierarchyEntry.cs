﻿using UnityEngine;
using UnityEngine;
using UnityEditor;

using SPE;

public class HierarchyEntry
{
    [MenuItem("GameObject/SimplePhysicsEngine/Create Particle", false, priority = -10)]
    private static void CreateParticle()
    {
        Particle.Create();
    }

    [MenuItem("GameObject/SimplePhysicsEngine/Create Spring", false, priority = -10)]
    private static void CreateSpring()
    {
        Spring.Create();
    }

}