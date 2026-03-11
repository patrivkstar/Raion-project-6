using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Visual Components")]
    public GameObject attackHitbox;    
    public SpriteRenderer handRenderer; 
    public Animator handAnimator;      
    public Animator effectAnimator;    

    [Header("Aiming System")]
    public Transform aimPivot;       
    public MonoBehaviour aimScript;  

    [Header("90 BPM Settings")]
    [SerializeField] private float attackDuration = 0.2f; 
    [SerializeField] private float attackCooldown = 0.66f; 
    
    private float nextAttackTime = 0f;
    private bool isAttacking = false;

    void Start()
    {
        attackHitbox.SetActive(false);
        handRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime && !isAttacking)
        {
            StartCoroutine(PerformAttackRoutine());
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    IEnumerator PerformAttackRoutine()
    {
        isAttacking = true;

        LockAttackDirection();
        if (aimScript != null) aimScript.enabled = false;

        handRenderer.enabled = true;
        attackHitbox.SetActive(true);

        if (handAnimator != null) handAnimator.Play("Hand_Attack", -1, 0f);
        if (effectAnimator != null) effectAnimator.Play("Effect_Attack", -1, 0f);

        yield return new WaitForSeconds(attackDuration);

        attackHitbox.SetActive(false);
        handRenderer.enabled = false;

        if (aimScript != null) aimScript.enabled = true;
        
        isAttacking = false;
    }

    void LockAttackDirection()
    {
        if (aimPivot == null) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)mousePos - (Vector2)aimPivot.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (transform.localScale.x < 0) 
            aimPivot.rotation = Quaternion.Euler(0, 0, angle + 180f);
        else
            aimPivot.rotation = Quaternion.Euler(0, 0, angle);
    }
}