using System.Collections.Generic;
using UnityEngine;
public class DisableTurret : MonoBehaviour
{
    public List<TurretScript> turrets;
    public void KillSwitchTurret() 
    {
        foreach (TurretScript turret in turrets)
        {
            turret.isActive = false;
        }
    }
}