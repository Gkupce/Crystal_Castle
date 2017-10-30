using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

    public enum GemType
    {
        None,
        Speed,
        Homing,
		Bouncing
    }

    GemType[] types = new GemType[2] { GemType.None, GemType.None };
    int[] amounts = new int[2] { 0, 0 };

    public GameObject[] bombs;

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

    public void DropGem(int i)
    {
        if (amounts[i] < 1)
        {
            return;
        }
        GameObject explosion = null;
        switch (types[i])
        {
            default: //en caso de que este tipo de gema no tenga una bomba especial o no este implementada, uso la default
                explosion = Instantiate(bombs[0], transform.position, Quaternion.identity);
                explosion.transform.localScale = Vector3.one * Mathf.Clamp(amounts[i] * 0.5f,1f,4f);
                explosion.GetComponent<PlayerProjectile>().damage = amounts[i] * 2;
                break;
        }
        types[i] = GemType.None;
        amounts[i] = 0;
        GemUIManager.instance.AddGem(i,GemType.None,0);
        if (explosion != null)
        {
            Destroy(explosion,0.5f);
        }
    }
}
