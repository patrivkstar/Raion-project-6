using UnityEngine;

public class HitboxAim : MonoBehaviour
{
    [HideInInspector] public bool isLocked = false;

    void Update()
    {
        
        if (isLocked) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (Vector2)mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (transform.root.localScale.x > 0) 
        {
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, angle + 180f);
        }
    }
}