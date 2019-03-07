using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Tout fusil, arme doivent hériter de Shooter

public class Riffle : Shooter
{
    public override void Fire()
    {
        base.Fire();

        if(canFire)
        {
            // we can Fire the gun
        }
    }
}
