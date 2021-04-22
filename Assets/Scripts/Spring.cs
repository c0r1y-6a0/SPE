using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SPE
{
    public class Spring : MonoBehaviour
    {
        public Particle A
        {
            get
            {
                return GetPartice(0);
            }
            set
            {
                SetParticle(value, 0);
            }
        }

        public Particle B
        {
            get
            {
                return GetPartice(1);
            }
            set
            {
                SetParticle(value, 1);
            }
        }

        public float RestLength;
        public float K;
        public bool Bungee; //橡皮筋式弹簧。 当距离小于restline时，不反弹

        [SerializeField]
        private Particle[] m_particles = new Particle[2];

        public bool Valid
        {
            get
            {
                return m_particles[0] != null && m_particles[1] != null;
            }
        }

        private ParticleSpring m_ps;

        private void Awake()
        {
            m_ps = new ParticleSpring(this);
        }

        private void SetParticle(Particle p, int index)
        {
            m_particles[index] = p;
        }

        private void OnDestroy()
        {
            if(m_ps != null)
            {
                m_ps.UnReg();
            }
        }

        private Particle GetPartice(int index)
        {

            return m_particles[index];
        }

        private void OnDrawGizmos()
        {
            if(!Valid)
            {
                Gizmos.DrawIcon(transform.position, "未初始化的Spring");
                return;
            }

            Gizmos.color = Color.yellow;
            Vector3 pa = A.transform.position;
            Vector3 pb = B.transform.position;
            Gizmos.DrawLine(pa, pb);

#if UNITY_EDITOR
            Handles.Label((pa + pb) / 2, (pa - pb).magnitude.ToString());
#endif
        }

        private static int S_COUNT = 0;
        public static Spring Create()
        {
            GameObject go = new GameObject();
            go.name = "Spring_" + S_COUNT;
            S_COUNT += 1;
            go.transform.position = Vector3.zero;
            Spring sp = go.AddComponent<Spring>();
            return sp;
        }
    }

}

