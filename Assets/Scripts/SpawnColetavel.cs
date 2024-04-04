using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnColetavel : MonoBehaviour
{
    public float intervaloSpawn = 0.5f;

    public Transform prefabColetavel;
    public Transform areaSpawn;

    void Start()
    {
        InvokeRepeating("Spawnar", 0, intervaloSpawn);
    }


    void Spawnar()
    {
        float areaX = areaSpawn.localScale.x / 2;
        float areaZ = areaSpawn.localScale.z / 2;
        Vector3 localSpawn = new Vector3(Random.Range(-areaX, areaX), prefabColetavel.position.y, Random.Range(-areaZ, areaZ));

        Transform instancia = Instantiate(prefabColetavel, transform);
        instancia.position = localSpawn;
    }
}
