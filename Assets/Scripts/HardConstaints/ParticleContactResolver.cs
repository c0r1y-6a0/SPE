using System;
using System.Collections.Generic;
using UnityEngine;

namespace SPE
{
    class ParticleContactResolver
    {
        public uint MaxIterations { get; set; }

        private uint m_usedIterations;

        public ParticleContactResolver(uint iter)
        {
            MaxIterations = iter;
        }

        public void ResolveContact(List<ParticleContact> contacts, float dt)
        {
            m_usedIterations = 0;
            while(m_usedIterations < MaxIterations)
            {
                float max = float.MaxValue;
                int index = contacts.Count;
                for(int i = 0; i < contacts.Count; i++)
                {
                    float sepVal = contacts[i].CalculateSeparatingVelocity();
                    if(sepVal < max && (sepVal < 0 || contacts[i].Penetration > 0))
                    {
                        max = sepVal;
                        index = i;
                    }
                }

                if (index == contacts.Count)
                {
                    break;
                }

                ParticleContact target = contacts[index];
                target.Resolve(dt);

                Vector3[] pm = contacts[index].ParticleMovement;
                foreach(var contact in contacts)
                {
                    if(contact.Particles[0] == target.Particles[0])
                    {
                        contact.Penetration -= Vector3.Dot(pm[0] , contact.ContactNormal);
                    }
                    else if(contact.Particles[0] == target.Particles[1])
                    {
                        contact.Penetration -= Vector3.Dot(pm[1] , contact.ContactNormal);
                    }

                    if(contact.Particles[1] != null)
                    {
                        if (contact.Particles[1] == target.Particles[0])
                        {
                            contact.Penetration += Vector3.Dot(pm[0], contact.ContactNormal);
                        }
                        else if (contact.Particles[1] == target.Particles[1])
                        {
                            contact.Penetration += Vector3.Dot(pm[1], contact.ContactNormal);
                        }
                    }
                }

                m_usedIterations++;
            }
        }
    }
}
