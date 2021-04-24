using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace SPE
{
    public class Particle : MonoBehaviour
    {
        public bool Static;

        public Vector3 Velocity; 

        public Vector3 Acceleration;  //可以直接改变

        public float Damping;  //衰减，阻尼, 用于模拟真实世界中的摩擦力. 是个百分比，在每次update之后，下次的速度 = 当前速度 * damping.
        public Vector3 Gravity;

        [Header("摩擦力")]
        public float K1;
        public float K2;

        [Header("浮力")]
        public float Volume;
        public float HalfHeight;

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
        public float InverseMass
        {
            get
            {
                return m_inverseMass;
            }
        }

        private float m_mass = 1;
        private float m_inverseMass; 
        private Vector3 m_forceAccu;  //记录了力的总合。用于一次迭代计算，迭代完一次后清零
#if UNITY_EDITOR
        private Vector3 m_gizmoForce;
#endif


        private Gravity m_gravity;
        private Drag m_drag;
        private ParticleBuoyancy m_bouyancy;

        private void Awake()
        {
            Mass = Mass;

            PhysicsWorld.Instance.AddParticle(this);
            m_gravity = new Gravity(this);
            m_drag = new Drag(this);
            m_bouyancy = new ParticleBuoyancy(this);
        }

        private void OnDestroy()
        {
            m_gravity.UnReg();
            m_drag.UnReg();
            m_bouyancy.UnReg();
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

#if UNITY_EDITOR
            m_gizmoForce = m_forceAccu;
#endif
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

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.Label(transform.position, "Force: " + m_gizmoForce);
#endif
        }
    }

}
