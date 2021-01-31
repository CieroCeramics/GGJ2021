using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoulSpawnController : MonoBehaviour
{
    [SerializeField] private SoulBehavior soulPrefab;
    
    [SerializeField]
    private Vector3 minPosition;
    [SerializeField]
    private Vector3 maxPosition;
    
    [SerializeField]
    private int totalRed;
    [SerializeField]
    private int totalGreen;
    [SerializeField]
    private int totalBlue;

    private new Transform transform;
    // Start is called before the first frame update
    private void Start()
    {
        transform = gameObject.transform;
        
        SpawnSouls();
    }

    private void SpawnSouls()
    {
        var toSpawn = new[]
        {
            totalRed,
            totalGreen,
            totalBlue
        };

        for (int i = 0; i < 3; i++)
        {
            var spawnCount = toSpawn[i];
            var type = (SoulBehavior.TYPE) i;

            for (int ii = 0; ii < spawnCount; ii++)
            {
                var position = GetRandomPosition();
                var soulTemp = Instantiate(soulPrefab, position, Quaternion.identity, transform);

                soulTemp.gameObject.name = $"Soul_{type}_[{ii}]";
                
                soulTemp.Init(type);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        var x = Random.Range(minPosition.x, maxPosition.x);
        var z = Random.Range(minPosition.z, maxPosition.z);
        var newPosition = new Vector3(x, 2f, z);

        return !NavMesh.SamplePosition(newPosition, out var navMeshHit, 5.0f, NavMesh.AllAreas)
            ? GetRandomPosition()
            : navMeshHit.position;
    }
    
#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        var TL = new Vector3(minPosition.x, 2f, maxPosition.z);
        var TR = new Vector3(maxPosition.x, 2f, maxPosition.z);
        var BR = new Vector3(maxPosition.x, 2f, minPosition.z);
        var BL = new Vector3(minPosition.x, 2f, minPosition.z);

        
        Gizmos.DrawLine(TL, TR);
        Gizmos.DrawLine(TR, BR);
        Gizmos.DrawLine(BR, BL);
        Gizmos.DrawLine(BL, TL);
    }

#endif

}
