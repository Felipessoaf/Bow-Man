using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float Dmg;

    public void TakeDamage()
    {
        transform.parent.gameObject.GetComponent<Player>().Damage(Dmg);
        GetComponentInChildren<ParticleSystem>().Play();
    }
}
