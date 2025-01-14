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
    [SerializeField] int centerMargin = 8;
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
                        (distPerRing / layerPerRing * layer) + (distPerRing * ring) + centerMargin,
                        (distPerRing / layerPerRing * layer + 1) + (distPerRing * ring) + centerMargin);
                    var randAngle = Random.Range(anglePerSection * section, anglePerSection * (section + 1));

                    var position = Quaternion.Euler(0, 0, randAngle) * Vector3.right * randDistance;
                    position = new Vector2(position.x * 1.4f, position.y);

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
        Instantiate(grassPrefab, position, Quaternion.identity, aliveRings[ring].transform);
    }

    private void InstantiateDeadGrass(Vector3 position, int ring)
    {
        Instantiate(deadGrassPrefab, position, Quaternion.identity, deadRings[ring].transform);
    }

}
