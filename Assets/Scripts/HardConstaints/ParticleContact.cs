using System;
using UnityEngine;

namespace SPE
{
    public class ParticleContact
    {
        public Particle[] Particles = new Particle[2];//表示碰撞的双方。当有一方为静止物时， Particles[1]为NULL
        public float Restitution;
        public Vector3 ContactNormal;
        public float Penetration; //沿着ContactNormal的碰撞的渗透深度
        public Vector3[] ParticleMovement = new Vector3[2];//因为Penetration，每个Object应该移动的距离


        private float GetTotalInverseMass()
        {
            float totalInverseMass = Particles[0].Mass;
            if (Particles[1] != null)
            {
                totalInverseMass += Particles[1].Mass;
            }
            return 1 / totalInverseMass;
        }

        public void Resolve(float dt)
        {
            ResolveVelovity(dt);
            ResolveInterpenetration(dt);
        }

        public float CalculateSeparatingVelocity()
        {
            Vector3 relativeVelocity = Particles[0].Velocity;
            if (Particles[1] != null)
            {
                relativeVelocity -= Particles[1].Velocity;
            }
            return Vector3.Dot(relativeVelocity, ContactNormal);
        }

        private void ResolveVelovity(float dt)
        {
            float separateVelocity = CalculateSeparatingVelocity();
            if(separateVelocity > 0)
            {
                //说明不会有碰撞，也就不会发生速度改变
                return;
            }

            float newSeperateVelocity = -separateVelocity * Restitution;

            Vector3 accCausedVelocity = Particles[0].Acceleration;
            if(Particles[1] != null)
            {
                accCausedVelocity += Particles[1].Acceleration;
            }
            float accCausedSepVelocity = Vector3.Dot(accCausedVelocity, ContactNormal) * dt;
            if(accCausedSepVelocity < 0)
            {
                newSeperateVelocity += Restitution * accCausedSepVelocity;
                newSeperateVelocity = newSeperateVelocity < 0 ? 0 : newSeperateVelocity;
            }

            float deltaVelocity = newSeperateVelocity - separateVelocity;

            float totalInverseMass = GetTotalInverseMass();
            if (totalInverseMass <= 0)
            {
                return;
            }

            float impluse = deltaVelocity / totalInverseMass;
            Vector3 dirImpluse = ContactNormal * impluse;

            Particles[0].Velocity += dirImpluse * Particles[0].Mass;
            if(Particles[1] != null)
            {
                Particles[1].Velocity -= dirImpluse * Particles[1].Mass;
            }
        }

        private void ResolveInterpenetration(float dt)
        {
            if(Penetration <= 0)
            {
                return;//没有穿插，不需要移动
            }

            float totalInverseMass = GetTotalInverseMass();
            if(totalInverseMass <= 0)
            {
                return;
            }

            ParticleMovement[0] = Particles[1].Mass * totalInverseMass * Penetration * ContactNormal;
            Particles[0].transform.position += ParticleMovement[0];
            if(Particles[1] != null)
            {
                ParticleMovement[1] = -Particles[0].Mass * totalInverseMass * Penetration * ContactNormal;
                Particles[1].transform.position += ParticleMovement[1];
            }
        }
    }
 }