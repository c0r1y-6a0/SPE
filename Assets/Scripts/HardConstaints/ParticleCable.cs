using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SPE
{
    public class ParticleCable : MonoBehaviour, ParticleContactGenerator
    {
        public Particle[] ends;
        public float Restitution;
        public float MaxLength;

        public float CurrentLength
        {
            get
            {
                return (ends[0].transform.position - ends[1].transform.position).magnitude;
            }
        }

        void Awake()
        {
            PhysicsWorld.Instance.AddContactGenerator(this);
        }

        void OnDestroy()
        {
            PhysicsWorld.Instance.RemoveContactGenerator(this);
        }


        public int AddContacts(List<ParticleContact> contacts, int limit)
        {
            if (contacts.Count >= limit)
                return limit;


            float l = CurrentLength;
            if (l <= MaxLength)
                return contacts.Count;


            ParticleContact pc = new ParticleContact();
            pc.Particles[0] = ends[0];
            pc.Particles[1] = ends[1];
            pc.ContactNormal = (ends[1].transform.position - ends[0].transform.position).normalized;
            pc.Restitution = Restitution;
            pc.Penetration = l - MaxLength;
            contacts.Add(pc);
            return 1;
        }

        private void OnDrawGizmos()
        {
            if (ends == null || ends[0] == null || ends[1] == null)
            {
                return;
            }
            Gizmos.color = Color.green;
            var p0 = ends[0].transform.position;
            var p1 = ends[1].transform.position;
            Gizmos.DrawLine(p0, p1);
#if UNITY_EDITOR
            Handles.Label((p1 + p0) / 2, CurrentLength.ToString());
#endif
        }

        static uint COUNT = 0;
        public static ParticleCable Create()
        {
            GameObject go = new GameObject();
            go.transform.position = Vector3.zero;
            go.name = "Cable_" + COUNT++;
            return go.AddComponent<ParticleCable>();
        }
    }

}
