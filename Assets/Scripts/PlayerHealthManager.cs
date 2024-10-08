using UnityEngine;
using Yarn.Unity;

public class PlayerHealthManager : MonoBehaviour
{
    public float health;
    public GameObject damageFilter;
    public GameObject dialogueSystem;
    private float startHealth;
    public void Start() { startHealth = health; }
    public void TakeDamage(float damage)
    {
        health -= damage;
        damageFilter.SetActive(true);
        Invoke("DisableDamageFilter", 0.2f);
        //Respawn when player dies
        if (health <= 0)
        {
            GetComponent<RespawnBehaviour>().Respawn();
            health = startHealth;
            dialogueSystem.GetComponent<DialogueRunner>().StartDialogue("DiedToTurret");
        }
    }
    public void DisableDamageFilter() { damageFilter.SetActive(false); }
}