//Script to spawn the pickables in the spawnpoints at the start of the game
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private GameObject[] spawnPoints;
    private int chosenPrefab;
    [SerializeField]
    [Tooltip ("Lista de todos los pickables que pueden spawnear")]
    private List<GameObject> pickablePrefab;

    private void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");

        GameManager.instance.pickableNumber = spawnPoints.Length;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            chosenPrefab = Random.Range(0, pickablePrefab.Count);
            Destroy(spawnPoints[i].transform.GetChild(0).gameObject);
            Instantiate(pickablePrefab[chosenPrefab], spawnPoints[i].transform.position, Quaternion.identity, spawnPoints[i].transform);
        }
    }
}
