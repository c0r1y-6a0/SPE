using UnityEngine;
using System.Collections.Generic;

namespace SPE
{
    public class PhysicsWorld : GCTools.CreateSingleton<PhysicsWorld>
    {
        [Header("水")]
        public float WaterHeight;
        public float WaterDensity;

        public ParticleForceRegistry Registry;

        private HashSet<Particle> m_allParticles;

        protected override void InitSingleton()
        {
            Registry = new ParticleForceRegistry();
            m_allParticles = new HashSet<Particle>();
        }

        void FixedUpdate()
        {
            Registry.UpdateForces(Time.fixedDeltaTime);
            foreach(Particle p in m_allParticles)
            {
                p.Integrate(Time.fixedDeltaTime);
            }
        }

        public void AddParticle(Particle p)
        {
            if (m_allParticles.Contains(p))
                return;
            m_allParticles.Add(p);
        }

        public void RemoveParticle(Particle p)
        {
            m_allParticles.Remove(p);
        }
    }

}