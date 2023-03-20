using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControll : MonoBehaviour
{
    private ParticleSystem thisParticleSystem;

    private void Awake()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (thisParticleSystem.isPlaying) return;

        this.gameObject.SetActive(false);
    }
}
