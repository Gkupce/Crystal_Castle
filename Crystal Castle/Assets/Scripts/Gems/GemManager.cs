using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

    public enum GemType
    {
        None = 0,
        Speed = 1,
        Homing = 2,
		Bouncing = 3,
		Poison = 4,
		Vampire = 5,
		Count
    }

	public const int MAX_GEMS = 7;

    GemType[] types = new GemType[2] { GemType.None, GemType.None };
	Color[] colors = {	
						Color.white,
						new Color (0.237f, 0.661f, 0.959f),			// Light-blue
						new Color (0.834f, 0.855f, 0.310f),			// Yellow
						new Color (0.310f, 0.955f, 0.310f),			// Green
						new Color (0.859f, 0.337f, 0.596f),			// Violet
						new Color (0.871f, 0.349f, 0.349f)			// Red
	};

	public Color[] currentColors = { Color.white, Color.white };

	int[] amounts = new int[2] { 0, 0 };

    public GameObject[] bombPrefs;

	private GemText gemText;

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
		int slot = -1;
        for (int i = 0; i < types.Length; i++)
        {
			if (types [i] == gemType)
				slot = i;  
        }

		if (slot == -1) {
			for (int i = 0; i < types.Length; i++)
			{
				if (types [i] == GemType.None)
					slot = i;  
			}
		}

		if (slot != -1)
		{
			if (types[slot] == gemType)
			{
				if (amounts [slot] < MAX_GEMS)
					amounts [slot]++;
				else
					return false;
			}

			if (types[slot] == GemType.None)
			{
				types[slot] = gemType;
				amounts[slot] = 1;
			}

			GemEffect(gemType);
			currentColors [slot] = colors[(int)gemType];

			GemUIManager.Instance.AddGem(slot, gemType, amounts[slot]);
			return true;
		}

        return false;
    }


    void GemEffect(GemType gemType)
    {
        switch (gemType)
        {
			case GemType.Speed:
				transform.GetComponent<AutomaticProjectileWeapon> ().ReduceCooldown (0.05f);
				break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Crystal")
        {
			Gem gem = collision.GetComponent<Gem> ();
			if (PickUpGem (gem.gemType)) {
				gem.GrabAnim ();
				if (gemText == null)
					gemText = GameObject.FindWithTag ("GemText").GetComponent<GemText> ();
				gemText.Show (gem);
			}	
			else
				gem.GetComponentInChildren<Animator> ().SetTrigger ("Reject");
        }
    }

    public void DropGem(int i)
    {
        if (amounts[i] < 1)
        {
            return;
        }

        if(types[i] == GemType.Speed)
        {
            transform.GetComponent<AutomaticProjectileWeapon>().RemoveCooldown();
        }

		GameObject bombs = null;

        switch (types[i])
        {
			case GemType.Speed:
				bombs = Instantiate(bombPrefs[(int)GemType.Speed], transform.position, Quaternion.identity);
				bombs.GetComponent<SpeedBomb>().Explode(amounts[i]);
				break;
            case GemType.Bouncing:
            case GemType.Homing:
				bombs = Instantiate(bombPrefs[(int)GemType.Homing], transform.position, Quaternion.identity);
                bombs.GetComponent<SpreadBomb>().Explode(amounts[i], types[i]);
                break;
            case GemType.Poison:
                bombs = Instantiate(bombPrefs[(int)GemType.Poison], transform.position, Quaternion.identity);
                bombs.transform.localScale = Vector3.one * Mathf.Clamp(amounts[i] * 0.5f, 1f, 4f);
                bombs.GetComponent<PlayerProjectile>().poisonDamage = amounts[i];
                break;
			case GemType.Vampire:
				bombs = Instantiate(bombPrefs[(int)GemType.Vampire], transform.position, Quaternion.identity);
				bombs.transform.localScale = Vector3.one * Mathf.Clamp(amounts[i] * 0.5f,1f,4f);
				bombs.GetComponent<PlayerProjectile>().vampireRecovery = amounts[i] * 5;
				break;
            default: //en caso de que este tipo de gema no tenga una bomba especial o no este implementada, uso la default
				bombs = Instantiate(bombPrefs[(int)GemType.None], transform.position, Quaternion.identity);
                bombs.transform.localScale = Vector3.one * Mathf.Clamp(amounts[i] * 0.5f,1f,4f);
                //bombs.GetComponent<PlayerProjectile>().damage = amounts[i] * 2;
                break;
        }

        types[i] = GemType.None;
		currentColors [i] = colors [(int)GemType.None];
        amounts[i] = 0;

        GemUIManager.Instance.AddGem(i,GemType.None,0);

        if (bombs != null)
        {
            Destroy(bombs, 0.5f);
        }
    }


	private int BombsQuantity (int i) {
		return amounts [i] / 4 + 1;
	}
}
