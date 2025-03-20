using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;

    [SerializeField]
    private int iterations = 10;

    [SerializeField]
    public int walkLength = 10;

    [SerializeField]
    public bool startRandomlyEachIteration = true;

    public void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomwalk();
        foreach(var position in floorPositions)
        {
            Debug.Log(position);
        }
    }

    protected HashSet<Vector2Int> RunRandomwalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPosition = new HashSet<Vector2Int>();
        for(int i = 0; i < iterations; i++)
        {
            var path = ProceduralGenerationAlgorithm.SimpleRandomWalk(currentPosition, walkLength);
            floorPosition.UnionWith(path);
            if(startRandomlyEachIteration)
            {
                currentPosition = floorPosition.ElementAt(UnityEngine.Random.Range(0, floorPosition.Count));
            }
        }
        return floorPosition;
    }
}
