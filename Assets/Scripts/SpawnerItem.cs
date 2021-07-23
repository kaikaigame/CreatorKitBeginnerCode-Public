using UnityEngine;
using CreatorKitCode;
using System;

public class SpawnerItem : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public int radius = 5;
    public int count;

    Vector3 direction;
    Vector3 spawnPosition;
    LootAngle myLootAngle = new LootAngle(45);

    void Start()
    {
        //int angle = 15;
        //int radius = 5;
        //Vector3 spawnPosition = transform.position;
        //Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
        //spawnPosition = transform.position + direction * radius;
        //Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);

        //SpawnPotion(0);
        //SpawnPotion(45);
        //SpawnPotion(90);
        //SpawnPotion(135);

        for (int i = 0; i < count; i++)
        {
            SpawnPotion(myLootAngle.NextAngle());
        }
    }

    //生成药水
    void SpawnPotion(int angle)
    {
        direction = Quaternion.Euler(0, angle, 0) * Vector3.right;
        spawnPosition = transform.position + direction * radius;
        Instantiate(ObjectToSpawn, spawnPosition, Quaternion.identity);
    }
}

[Serializable]
public class LootAngle
{
    int angle;
    int step;

    //每次角度加45
    public LootAngle(int increment)
    {
        step = increment;
        angle = 0;
    }

    public int NextAngle()
    {
        int currentAngle = angle;
        angle = Helpers.WrapAngle(angle + step);
        return currentAngle;
    }
}

