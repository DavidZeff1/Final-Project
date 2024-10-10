using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlot : MonoBehaviour
{
    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentWeapon = Instantiate(weaponPrefab, transform);
        currentWeapon.transform.localPosition = new Vector2(0, -0.08f);
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = new Vector2(0.25f, 0.25f);
        currentWeapon.GetComponent<ShootHandler>().EnableScript();
    }
}
