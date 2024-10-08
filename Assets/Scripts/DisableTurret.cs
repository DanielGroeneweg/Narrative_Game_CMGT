using UnityEngine;
public class DisableTurret : MonoBehaviour
{ public void KillSwitchTurret() { GameObject.Find("Turret").GetComponent<TurretScript>().isActive = false; } }