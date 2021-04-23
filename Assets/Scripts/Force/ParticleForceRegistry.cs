using System.Collections.Generic;

namespace SPE
{
    public abstract class ParticleForceGenerator
    {
        public ParticleForceGenerator()
        {
            Reg();
        }

        public void Reg()
        {
            PhysicsWorld.Instance.Registry.Add(this);
        }

        public abstract void UpdateForce(float dt);

        public void UnReg()
        {
            if(PhysicsWorld.Instance != null)
                PhysicsWorld.Instance.Registry.Remove(this);
        }
    }

    public class ParticleForceRegistry
    {

        List<ParticleForceGenerator> m_registries;
        public ParticleForceRegistry()
        {
            m_registries = new List<ParticleForceGenerator>();
        }

        public void Add(ParticleForceGenerator generator)
        {
            m_registries.Add(generator);
        }

        public void Remove(ParticleForceGenerator generator)
        {
            m_registries.Remove(generator);
        }

        public void Clear()
        {
            m_registries.Clear();
        }

        public void UpdateForces(float dt)
        {
            foreach(var p in m_registries)
            {
                p.UpdateForce(dt);
            }
        }
    }

}