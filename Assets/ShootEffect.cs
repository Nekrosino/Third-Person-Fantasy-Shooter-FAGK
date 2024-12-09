using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    public GameObject particlePrefab; 
    public Transform spawnPoint;      

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Sprawdzenie wciœniêcia przycisku (domyœlnie LPM w Unity)
        {
            TriggerEffect();
        }
    }

    void TriggerEffect()
    {
        
        GameObject effect = Instantiate(particlePrefab, spawnPoint.position, spawnPoint.rotation);
        
        Destroy(effect, 5f); 
    }
}
