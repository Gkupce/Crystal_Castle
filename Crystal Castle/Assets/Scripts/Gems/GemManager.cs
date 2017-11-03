using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

    public enum GemType
    {
        None,
        Speed,
        Homing,
		Bouncing,
		Poison
    }

    GemType[] types = new GemType[2] { GemType.None, GemType.None };
    int[] amounts = new int[2] { 0, 0 };

    public GemType[] Types
    {
        get
        {
            return types;
        }
    }

    public int[] Amounts
    {
        get
        {
            return amounts;
        }
    }

    public bool PickUpGem(GemType gemType)
    {

        for (int i = 0; i < types.Length; i++)
        {
            if (types[i] == gemType || types[i] == GemType.None)
            {
                if (types[i] == gemType)
                {
                    amounts[i]++;
                }
                if (types[i] == GemType.None)
                {
                    types[i] = gemType;
                    amounts[i] = 1;
                }
                GemEffect(gemType);
                GemUIManager.instance.AddGem(i, gemType, amounts[i]);
                return true;
            }
        }

        return false;
    }


    void GemEffect(GemType gemType)
    {
        switch (gemType)
        {
            case GemType.Speed:
                transform.GetComponent<AutomaticProjectileWeapon>().ReduceCooldown(0.05f);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Crystal")
        {
            if (PickUpGem(collision.GetComponent<Gem>().gemType))
            {
                collision.GetComponent<Gem>().GrabAnim();
            }
        }
    }
}
