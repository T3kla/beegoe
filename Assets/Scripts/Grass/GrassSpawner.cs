using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{

    [SerializeField] GameObject grassPrefab;

    [SerializeField] int ringCount;
    [SerializeField] int sectionCount;
    [SerializeField] float distPerRing;
    [SerializeField] int layerPerRing;
    float anglePerSection;
    int layers;

    [SerializeField] bool skipFirstRing;

    private void Start()
    {
        anglePerSection = 360 / ringCount;
        layers = ringCount * layerPerRing;
        populateGrass();
    }

    private void populateGrass()
    {
        for (int layer = skipFirstRing? layerPerRing : 0 ; layer < layers; layer++)
        {
            if (layer % 2 != 0)
            {
                //Double the sections?
            }
            for (int section = 0; section < sectionCount; section++)
            {
                var randDistance = Random.Range(distPerRing / layerPerRing * layer, distPerRing / layerPerRing * (layer + 1));
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
