using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public System.Action onDeathCallback; 

    public float shakeIntensity = 0.3f;
    public float shakeDuration = 0.15f;

    private SpriteRenderer sr;
    private Transform camTransform;
    private Vector3 originalCamPos;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponentInChildren<SpriteRenderer>();
        if (Camera.main != null) camTransform = Camera.main.transform;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        
        StopAllCoroutines();
        if (sr != null) StartCoroutine(HitEffect());
        StartCoroutine(ShakeCamera());

        if (currentHealth <= 0) Die();
    }

    IEnumerator HitEffect()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    IEnumerator ShakeCamera()
    {
        if (camTransform == null) yield break;
        
        originalCamPos = camTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeIntensity;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeIntensity;

            camTransform.localPosition = new Vector3(originalCamPos.x + x, originalCamPos.y + y, originalCamPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        camTransform.localPosition = originalCamPos;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
        onDeathCallback?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            TakeDamage(1);
        }
    }
}