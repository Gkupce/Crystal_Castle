using System.Collections;
using UnityEngine;

public class Homing : MonoBehaviour {

    Transform target;
    public LayerMask targetLayer;
    public float rotSpeed = 0f;
    public float maxAngleDif = 35f;
    float lastDist;

    private void OnEnable()
    {
        target = null;
        StartCoroutine(GetTarget());
        lastDist = float.MaxValue;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        enabled = false;
    }

    void Update () {
        if (target)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,GetRot(),rotSpeed * Time.deltaTime);
			gameObject.GetComponent<Projectile>().CalculateSpeed();
			float d = Vector3.Distance(transform.position,target.position);
            if (d > lastDist)
            {
                target = null;
                StopAllCoroutines();
                StartCoroutine(GetTarget());
            }
            else
            {
                lastDist = d;
            }
        }
	}

    IEnumerator GetTarget()
    {
        float minAngleDif = maxAngleDif;
        while(target == null)
        {
            foreach(Collider2D c in Physics2D.OverlapCircleAll(transform.position, 8f, targetLayer))
            {
                if(c == null)
                {
                    continue;
                }
                else
                {
                    float ang = Vector3.Angle(c.transform.position - transform.position, transform.up);
                    if (ang < minAngleDif)
                    {
                        minAngleDif = ang;
                        target = c.transform;
                        lastDist = Vector3.Distance(transform.position,target.position);
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    Quaternion GetRot()
    {
        Vector3 diff = target.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
