using UnityEngine;
using System.Collections;

namespace SPE
{
    public class ParticleBuoyancy :ParticleForceGenerator
    {
        Particle m_p;


        public ParticleBuoyancy(Particle p)
        {
            m_p = p;
        }


        public override void UpdateForce(float dt)
        {
            Vector3 p = m_p.transform.position;
            if (p.y > PhysicsWorld.Instance.WaterHeight + m_p.HalfHeight)
                return ;

            Vector3 force = Vector3.zero;
            if(p.y < PhysicsWorld.Instance.WaterHeight - m_p.HalfHeight)
            {
                force.y = PhysicsWorld.Instance.WaterDensity * m_p.Volume;
                m_p.AddForce(force);
                return;
            }

            force.y = PhysicsWorld.Instance.WaterDensity * m_p.Volume * (PhysicsWorld.Instance.WaterHeight - (p.y - m_p.HalfHeight));
            m_p.AddForce(force);
        }
    }

}
