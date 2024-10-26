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
        currentWeapon.transform.localPosition = new Vector2(0.1f, -0.08f);
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = new Vector2(0.25f, 0.25f);

        if(currentWeapon.GetComponent<ShootHandler>() != null)
        {
            currentWeapon.GetComponent<ShootHandler>().EnableScript();
        }

        if (currentWeapon.GetComponent<ShotGunShootHandler>() != null)
        {
            currentWeapon.GetComponent<ShotGunShootHandler>().EnableScript();
        }

        
        currentWeapon.transform.GetChild(0).gameObject.SetActive(false);
    }
}
