using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon Weapon { get; private set; }
    private GameObject weaponObject;
    private List<GameObject> weapons = new List<GameObject>();
    public void RegisterWeapon(GameObject weapon)
    {
        if(!weapons.Contains(weapon))
        {
            BaseWeapon weaponInfo=weapon.GetComponent<BaseWeapon>();
            weapons.Add(weapon);
            weapon.SetActive(false);
        }
    }
    public void SetWeapon(GameObject weapon)
    {
        if (Weapon == null)
        {
            weaponObject = weapon;
            Weapon=weapon.GetComponent<BaseWeapon> ();
            weaponObject.SetActive(true);
            Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
            return;
        }

        for(int i=0;i<weapons.Count; i++)
        {
            if (weapons[i].Equals(Weapon))
            {
                weaponObject = weapon;
                weaponObject.SetActive(true);
                Weapon = weapon.GetComponent<BaseWeapon>();
                Tester.Instance.animator.runtimeAnimatorController = Weapon.WeaponAnimator;
                continue;
            }
            weapons[i].SetActive(false);
        }
        
    }
}
