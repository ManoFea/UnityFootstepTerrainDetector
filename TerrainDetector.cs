//  https://www.youtube.com/watch?v=vqc9f7HU-Vc&ab_channel=WorldofZero
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainDetector : MonoBehaviour

{
    public GameObject lasthit;
    public Vector3 collision = Vector3.zero;

    void Update()
        {
            var ray = new Ray(this.transform.position, this.transform.up * -1);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                {
                      lasthit = hit.transform.gameObject;
                      collision = hit.point;
                }
        }

    public GameObject PlayerTerrain()
        {
          return lasthit;
        }

    void OnDrawGizmos()
        {
          Gizmos.color = Color.red;
          Gizmos.DrawWireSphere(collision, 0.2f);
        }
}
