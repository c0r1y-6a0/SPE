using UnityEngine;
using UnityEditor;

namespace SPE
{
    public class Drag : ParticleForceGenerator
    {
        Particle m_p;

        public Drag(Particle p)
        {
            m_p = p;
        }

        public override void UpdateForce(float dt)
        {
            float dragCoeff = m_p.Velocity.magnitude;
            dragCoeff = m_p.K1 * dragCoeff + m_p.K2 * dragCoeff * dragCoeff;

            m_p.AddForce(dragCoeff * (-m_p.Velocity.normalized) * m_p.Mass);
        }
    }

}