using UnityEngine;

public class MortarBarrel : MonoBehaviour
{
    public float height = 5f;
    public float duration = 1.2f;
    public GameObject spillPrefab;
    public int explodeDamage = 20; 
    private GameObject myShadow;
    
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;

    public void Setup(Vector3 target, GameObject shadow)
    {
        startPos = transform.position;
        targetPos = target;
        myShadow = shadow;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = timer / duration;

        if (progress <= 1f)
        {
            Vector3 currentPos = Vector2.Lerp(startPos, targetPos, progress);
            currentPos.y += Mathf.Sin(progress * Mathf.PI) * height;
            transform.position = currentPos;
            transform.Rotate(0, 0, 500 * Time.deltaTime);
        }
        else
        {
            Explode();
        }
    }

    void Explode()
    {
        if (myShadow != null) Destroy(myShadow);
        
        if (spillPrefab != null)
        {
            Instantiate(spillPrefab, targetPos, Quaternion.identity);
        }

        // Logika Damage Ledakan
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1.2f);
        if (hit != null && hit.CompareTag("Player"))
        {
           
            hit.GetComponent<PlayerMovement>().ReceiveDamage(explodeDamage);
        }

        Destroy(gameObject);
    }
}