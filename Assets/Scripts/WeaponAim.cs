using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    public SpriteRenderer weaponSprite; 

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (angle > 90 || angle < -90) weaponSprite.flipY = true;
        else weaponSprite.flipY = false;
    }
}