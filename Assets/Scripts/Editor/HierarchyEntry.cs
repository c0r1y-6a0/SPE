using UnityEngine;
using UnityEditor;

using SPE;

public class HierarchyEntry
{
    [MenuItem("GameObject/SimplePhysicsEngine/Create Particle", false, priority = 3)]
    private static void CreateParticle()
    {
        Particle.Create();
    }

    [MenuItem("GameObject/SimplePhysicsEngine/Create Spring", false, priority = 4)]
    private static void CreateSpring()
    {
        Spring.Create();
    }

    [MenuItem("GameObject/SimplePhysicsEngine/Create Cable", false, priority = 4)]
    private static void CreateCable()
    {
        ParticleCable.Create();
    }

    [MenuItem("GameObject/SimplePhysicsEngine/Create Rod", false, priority = 4)]
    private static void CreateRod()
    {
        ParticleRod.Create();
    }

}