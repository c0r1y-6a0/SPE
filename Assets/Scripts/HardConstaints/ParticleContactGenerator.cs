using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SPE
{
    public interface ParticleContactGenerator
    {
        int AddContacts(List<ParticleContact> contacts, int limit);
    }
}
