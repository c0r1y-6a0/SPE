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
        public int MaxContacts;

        public int MaxResolverIterations
        {
            get
            {
                return m_resolverIterions;
            }
            set
            {
                m_resolverIterions = value;
                if (Application.isPlaying)
                    m_resolver.MaxIterations = value;
            }
        }

        [HideInInspector, SerializeField]
        private int m_resolverIterions;

        private HashSet<Particle> m_allParticles;
        private HashSet<ParticleContactGenerator> m_contactGenerators;
        private List<ParticleContact> m_contacts;

        private ParticleContactResolver m_resolver;

        protected override void InitSingleton()
        {
            Registry = new ParticleForceRegistry();
            m_allParticles = new HashSet<Particle>();
            m_contactGenerators = new HashSet<ParticleContactGenerator>();
            m_contacts = new List<ParticleContact>();
            m_resolver = new ParticleContactResolver(MaxResolverIterations);
        }

        protected override void DuplicateDetection(PhysicsWorld duplicate)
        {
        }

        void FixedUpdate()
        {
            Registry.UpdateForces(Time.fixedDeltaTime);
            foreach(Particle p in m_allParticles)
            {
                p.Integrate(Time.fixedDeltaTime);
            }

            int contactCount = GenerateContacts();
            if(contactCount > 0)
            {
                m_resolver.ResolveContact(m_contacts, Time.fixedDeltaTime);
            }

            foreach (Particle p in m_allParticles)
            {
                p.OnPostPhysicsUpdate();
            }
        }

        public void AddParticle(Particle p)
        {
            if (m_allParticles.Contains(p))
            {
                Debug.LogError("重复添加Particle");
                return;
            }
            m_allParticles.Add(p);
        }

        public void RemoveParticle(Particle p)
        {
            m_allParticles.Remove(p);
        }

        public void AddContactGenerator(ParticleContactGenerator pcg)
        {
            if (m_contactGenerators.Contains(pcg))
            {
                Debug.LogError("重复添加Contact");
                return;
            }
            m_contactGenerators.Add(pcg);
        }

        public void RemoveContactGenerator(ParticleContactGenerator pcg)
        {
            m_contactGenerators.Remove(pcg);
        }

        private int GenerateContacts()
        {
            m_contacts.Clear();

            int count = 0;
            foreach(ParticleContactGenerator pgc in m_contactGenerators)
            {
                count += pgc.AddContacts(m_contacts, MaxContacts);
                if (count >= MaxContacts)
                    break;
            }

            return count;
        }
    }

}