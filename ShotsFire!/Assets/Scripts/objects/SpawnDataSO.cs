using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Data", menuName = "Scriptable Object/SpawnDataSO")]
public class SpawnDataSO : ScriptableObject
{
    public int spawn;
    [Space(20)]
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
}
