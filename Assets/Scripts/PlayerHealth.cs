using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;
    int hp;

    void Start()
    {
        hp = maxHP;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Debug.Log("HP: " + hp);

        if(hp <= 0)
            Destroy(gameObject);
    }
}