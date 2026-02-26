using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticleInstance;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        SpawnDamageParticles();
    }

    public void Die()
    {
        SpawnDamageParticles();
    }

    [ContextMenu("SpawnDamageParticles")]
    private void SpawnDamageParticles()
    {
        float topOfSprite = spriteRenderer.bounds.max.y;

        Vector3 spawnPosition = new Vector3(
            transform.position.x,
            topOfSprite + 2.5f, // adjust the offset if needed
            transform.position.z
        );

        damageParticleInstance = Instantiate(damageParticles, spawnPosition, Quaternion.identity);
    }
}
