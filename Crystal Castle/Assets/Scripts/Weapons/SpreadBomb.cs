using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBomb : MonoBehaviour {

    public GameObject projectile;
    public string projectileName;

    public Sprite[] sprites;

    public void Explode(int gems,GemManager.GemType gemType)
    {
        int projectiles = gems * 2;
        for (int i = 0; i < projectiles; i++)
        {
            GameObject b = Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, i * (360f / projectiles)));
            //TODO: usar el nuevo pooling
            b.SetActive(true);

            switch (gemType) {
                case GemManager.GemType.Homing:
                    Homing h = b.GetComponent<Homing>();
                    h.rotSpeed = 7f;
                    h.maxAngleDif = 90f;
                    h.enabled = true;
                    b.GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
                    break;
                case GemManager.GemType.Bouncing:
                    b.GetComponent<PlayerProjectile>().bounces = gems;
                    b.GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];

                    break;
            }
        }
    }
}
