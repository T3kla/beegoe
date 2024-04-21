using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{

    [SerializeField] GameObject grassPrefab;

    [SerializeField] int ringCount;
    [SerializeField] int sectionCount;
    [SerializeField] float distPerRing;
    float anglePerSection;

    [SerializeField] bool skipFirstRing;

    private void Start()
    {
        anglePerSection = 360 / ringCount;
        populateGrass();
    }

    private void populateGrass()
    {
        for (int ring = skipFirstRing? 1 : 0 ; ring < ringCount; ring++)
        {
            if (ring % 2 != 0)
            {
                //Double the sections?
            }
            for (int section = 0; section < sectionCount; section++)
            {
                var randDistance = Random.Range(distPerRing * ring, distPerRing * (ring + 1));
                var randAngle = Random.Range(anglePerSection * section, anglePerSection * (section + 1));

                var position = Quaternion.Euler(0, 0, randAngle) * Vector3.right * randDistance;

                placeAtPos(position);
            }
        }
    }

    private void placeAtPos(Vector3 position)
    {
        Instantiate(grassPrefab, position, Quaternion.identity, transform);
    }

}
