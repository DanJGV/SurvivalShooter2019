﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawn : MonoBehaviour
{
    ParticleSystem hitParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
    }
}
