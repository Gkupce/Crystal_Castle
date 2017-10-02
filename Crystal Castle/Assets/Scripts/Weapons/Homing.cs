using System.Collections;
using UnityEngine;

public class Homing : MonoBehaviour {

    Transform target;
    public LayerMask targetLayer;
    float rotSpeed = 0f;
    

    private void OnEnable()
    {
        target = null;
        rotSpeed = 0;
        StartCoroutine(GetTarget());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update () {
        if (target)
        {
            rotSpeed += 10 * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,GetRot(),rotSpeed * Time.deltaTime);
        }
	}

    IEnumerator GetTarget()
    {
        while(target == null)
        {
            foreach(Collider2D c in Physics2D.OverlapCircleAll(transform.position, 10f, targetLayer))
            {
                if(c == null)
                {
                    continue;
                }
                if (target == null)
                {
                    target = c.transform;
                }
                else
                {
                    if(Vector3.Distance(target.position,transform.position) > Vector3.Distance(c.transform.position, transform.position))
                    {
                        target = c.transform;
                    }
                }
            }
            yield return new WaitForSeconds(1f);
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
