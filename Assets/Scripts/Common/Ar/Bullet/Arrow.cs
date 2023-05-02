using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    ParticleSystem particle;

    protected override void SetSO()
    {
        particle = GetComponent<ParticleSystem>();
        summoner = FindObjectOfType<Archer>();
        base.SetSO();

        var particleMain = particle.main;
        particleMain.startRotation = -(transform.rotation.eulerAngles.z) * Mathf.Deg2Rad;

    }
}
