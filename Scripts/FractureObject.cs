using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractureObject : MonoBehaviour
{
    public GameObject originalObject;
    public GameObject fracturedObject;

    public float explosionMinForce = 5;
    public float explosionMaxForce = 100;
    public float explosionForceRadius = 10;
    public float fragScaleFactor = 1;

    private GameObject fractObj;

    public void Explode()
    {
        if (originalObject != null)
        {
            originalObject.SetActive(false);

            if (fracturedObject != null)
            {
                fractObj = Instantiate(fracturedObject) as GameObject;

                foreach (Transform t in fractObj.transform)
                {

                    var rb = t.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(Random.Range(explosionMinForce, explosionMaxForce), originalObject.transform.position, explosionForceRadius);

                    StartCoroutine(Shrink(t, 2));

                }

                Destroy(fractObj, 5);
                                
            }

        }

        IEnumerator Shrink (Transform t, float delay)
        {
            yield return new WaitForSeconds(delay);

            Vector3 newScale = t.localScale;

            while (newScale.x >= 0)
            {
                newScale -= new Vector3(fragScaleFactor, fragScaleFactor, fragScaleFactor);

                t.localScale = newScale;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }

}
