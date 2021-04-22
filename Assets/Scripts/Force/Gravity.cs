using UnityEngine;

namespace SPE
{
    public class Gravity : ParticleForceGenerator
    {
        private Particle m_p;

        public Gravity(Particle p)
        {
            m_p = p;
        }

        public override void UpdateForce(float dt)
        {
            m_p.AddForce(m_p.Gravity * m_p.Mass);
        }
    }

}