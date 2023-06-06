using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Bullet
{
    ParticleSystem particle;

    protected override void SetSO()
    {
        particle = GetComponent<ParticleSystem>();
        summoner = FindObjectOfType<Wizard>();
        base.SetSO();

        if (particle != null)
        {
            var particleMain = particle.main;
            particleMain.startRotation = -(transform.rotation.eulerAngles.z) * Mathf.Deg2Rad;
        }
    }
}
