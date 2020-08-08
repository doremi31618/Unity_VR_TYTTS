﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeControl : MonoBehaviour
{
    public ParticleSystem subParticle;

    ParticleSystem system
    {
        get
        {
            if (_CachedSystem == null)
                _CachedSystem = GetComponent<ParticleSystem>();
            return _CachedSystem;
        }
    }
    private ParticleSystem _CachedSystem;

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(gameObject.name);
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        ParticlePhysicsExtensions.GetCollisionEvents(system, other, collisionEvents);
        for (int i = 0; i< collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        subParticle.transform.position = particleCollisionEvent.intersection;
        subParticle.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        subParticle.Emit(1);
    }
}
