using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField] GameObject grassPrefab;
    [SerializeField] GameObject deadGrassPrefab;

    [SerializeField] int ringCount = 4;
    [SerializeField] int sectionCount = 12;
    [SerializeField] float distPerRing = 7;
    [SerializeField] int layerPerRing = 4;
    float anglePerSection;
    int layers;

    [SerializeField] bool skipFirstRing;

    [SerializeField] GameObject[] aliveRings;
    [SerializeField] GameObject[] deadRings;

    private void Start()
    {
        anglePerSection = 360 / ringCount;
        PopulateGrass();
    }

    private void PopulateGrass()
    {
        for (int ring = skipFirstRing ? 1 : 0; ring < ringCount; ring++)
        {
            for (int layer =  1; layer < layerPerRing + 1; layer++)
            {
                for (int section = 0; section < sectionCount; section++)
                {
                    var randDistance = Random.Range(
                        distPerRing / layerPerRing * layer * ring,
                        distPerRing / layerPerRing * (layer + 1) * ring);
                    var randAngle = Random.Range(anglePerSection * section, anglePerSection * (section + 1));

                    var position = Quaternion.Euler(0, 0, randAngle) * Vector3.right * randDistance;

                    PlaceAtPos(position, ring);
                }
            }
        }
    }

    private void PlaceAtPos(Vector3 position, int ring)
    {
        InstantiateGrass(position, ring);
        InstantiateDeadGrass(position, ring);
    }

    private void InstantiateGrass(Vector3 position, int ring)
    {
        Instantiate(grassPrefab, position, Quaternion.identity, aliveRings[ring-1].transform);
    }

    private void InstantiateDeadGrass(Vector3 position, int ring)
    {
        Instantiate(deadGrassPrefab, position, Quaternion.identity, deadRings[ring-1].transform);
    }

}
