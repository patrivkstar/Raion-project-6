using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    public enum BossState { Chase, Attack, Ultimate }
    public BossState currentState;

    public GameObject barrelPrefab;
    public GameObject shadowPrefab;
    public Transform shootPoint;
    
    public float moveSpeed = 2f;
    public float stopDistance = 5f;
    public float attackInterval = 3f;
    public int attacksBeforeUlti = 3;

    private int attackCount = 0;
    private float nextActionTime;
    private Transform player;
    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        currentState = BossState.Chase;
    }

    void Update()
    {
        if (player == null) return;

        // Flip Boss
        if (player.position.x > transform.position.x) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        if (currentState == BossState.Chase)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            if (dist > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (Time.time >= nextActionTime)
            {
                if (attackCount >= attacksBeforeUlti) StartCoroutine(PerformUltimate());
                else LaunchSingleAttack();
            }
        }
    }

    void LaunchSingleAttack()
    {
        attackCount++;
        nextActionTime = Time.time + attackInterval;
        if (anim != null) anim.SetTrigger("isAttacking");

        SpawnBarrel(player.position);
    }

    IEnumerator PerformUltimate()
    {
        currentState = BossState.Ultimate;
        attackCount = 0; 

        for (int i = 0; i < 6; i++)
        {
            if (anim != null) anim.SetTrigger("isAttacking");
            Vector3 target = player.position + (Vector3)Random.insideUnitCircle * 1.5f;
            SpawnBarrel(target);
            yield return new WaitForSeconds(0.4f);
        }

        nextActionTime = Time.time + attackInterval;
        currentState = BossState.Chase;
    }

    void SpawnBarrel(Vector3 target)
    {
        GameObject shadow = Instantiate(shadowPrefab, target, Quaternion.identity);
        GameObject barrel = Instantiate(barrelPrefab, shootPoint.position, Quaternion.identity);
        barrel.GetComponent<MortarBarrel>().Setup(target, shadow);
    }
}