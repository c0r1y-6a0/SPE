using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SPE
{
    public class Particle : MonoBehaviour
    {
        public bool Static;

        public Vector3 Velocity; 

        public Vector3 Acceleration;  //可以直接改变

        public float Damping;  //衰减，阻尼, 用于模拟真实世界中的摩擦力. 是个百分比，在每次update之后，下次的速度 = 当前速度 * damping.

        public float K1;
        public float K2;


        [field: SerializeField]
        public float Mass
        {
            get
            {
                return m_mass;
            }
            set
            {
                if (value <= 0.00001)
                {
                    Debug.LogError("Mass must be positive");
                    return;
                }
                m_mass = value;
                m_inverseMass = 1f / value;
            }
        }

        private float m_mass = 1;
        private float m_inverseMass; 
        private Vector3 m_forceAccu;  //记录了力的总合。用于一次迭代计算，迭代完一次后清零

        [field: SerializeField]
        public Vector3 Gravity { get; set; }

        private Gravity m_g;
        private Drag m_d;

        private void Awake()
        {
            Mass = Mass;

            PhysicsWorld.Instance.AddParticle(this);
            m_g = new Gravity(this);
            m_d = new Drag(this);
        }

        private void OnDestroy()
        {
            m_g.OnDestroy();
            m_d.OnDestroy();
        }

        public void Integrate(float dt)
        {
            if (Static)
                return;

            transform.position += Velocity * dt;

            Vector3 resultingAcc = Acceleration;
            resultingAcc += m_forceAccu * m_inverseMass;

            Velocity += resultingAcc * dt;
            Velocity *= Mathf.Pow(Damping, dt);

            m_forceAccu = Vector3.zero;
        }

        public void AddForce(Vector3 force)
        {
            m_forceAccu += force;
        }

        private static int S_COUNT = 0;
        //测试接口
        public static Particle Create()
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Particle" + S_COUNT++;
            go.transform.position = new Vector3(Random.value, Random.value, Random.value);
            Particle p = go.AddComponent<Particle>();
            p.Mass = Random.Range(1, 10);
            p.Damping = 0.99f;
            return p;
        }
    }

}
