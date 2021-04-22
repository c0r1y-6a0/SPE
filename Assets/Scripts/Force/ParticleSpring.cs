using UnityEngine;
using UnityEditor;

namespace SPE
{
    public class ParticleSpring : ParticleForceGenerator
    {
        Spring m_s;

        public ParticleSpring(Spring s)
        {
            m_s = s;
        }
        public override void UpdateForce(float dt)
        {
            if (!m_s.Valid)
                return;

            Vector3 dp = m_s.A.transform.position - m_s.B.transform.position;
            Vector3 force = -m_s.K* (dp.magnitude - m_s.RestLength) * dp.normalized;
            m_s.A.AddForce(force);

            m_s.B.AddForce(-force);
        }
    }

}