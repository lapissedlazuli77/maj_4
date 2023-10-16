using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currenttime = 0;
    float targetTime = 0.8f;

    public GameObject dragonfly;
    public GameObject birdstatue;
    public List<GameObject> spawnpoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        if (currenttime > targetTime)
        {
            currenttime = 0;
            targetTime = Random.Range(0.5f, 1.5f);
            int spawncount = 0;
            int tempcount = Random.Range(0, 5);
            if (tempcount > 0) { spawncount = tempcount - 1; } else { spawncount = 0; }
            Startspawn(spawncount);
        }
    }
    void Startspawn(int numspawn)
    {
        if (numspawn == 3)
        {
            for (int a = 0; a < numspawn; a++)
            {
                GameObject tospawn = spawnpoints[a];
                int chosenemy = Random.Range(0, 3);
                SpawnIn(tospawn, chosenemy);
            }
        }
        else
        {
            int temp1 = 0;
            int temp2 = 0;
            int choice = 0;

            for (int i = 0; i < numspawn; i++)
            {
                while (choice == temp1 || choice == temp2)
                {
                    int tempchoice = Random.Range(1, 6);
                    if (tempchoice == 1 || tempchoice == 2) { choice = 1; }
                    if (tempchoice == 3 || tempchoice == 4) { choice = 2; }
                    if (tempchoice == 5) { choice = 3; }
                }
                temp1 = choice;
                GameObject tospawn = spawnpoints[choice - 1];
                int chosenemy = Random.Range(0, 3);
                SpawnIn(tospawn, chosenemy);
                choice = 0;
            }

        }
    }
    void SpawnIn(GameObject spawner, int spawnedenem)
    {
        GameObject spawningenemy = new GameObject();
        if (spawnedenem <= 1) { spawningenemy = dragonfly; }
        else if (spawnedenem == 2) { spawningenemy = birdstatue; }
        Instantiate(spawningenemy, spawner.transform);
    }
}
