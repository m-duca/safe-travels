using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehaviourScript : MonoBehaviour
{
    [SerializeField] private int projectileCount = 3;
    [SerializeField] private int projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnpoint;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.5f, 0f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cow") && BalloonStats.HasGun)
        {
            Debug.Log("BALA NA AGULHA");
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlaySFX("Shoot");

            Invoke("ReloadSFX", 0.5f);

            for (int i = 0; i <  projectileCount; i++) 
            {
                GameObject projectile = Instantiate(projectilePrefab, projectileSpawnpoint.position + offset, Quaternion.identity);
                Vector3 direction = (other.transform.position - transform.position).normalized;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
            }
        }
    }

    private void ReloadSFX()
    {
        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("reload");
    }
}
