using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGeneration
{

    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength){
        
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition;

        for (int i = 0; i < walkLength; i++){
            var newPosition = previousPosition + Direction2D.GetRandomCardinalDirection2();
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path;
    }
}

public static class Direction2D {

    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>{
        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,0) // LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>{
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,1) // LEFT-UP
    };

    public static List<Vector2Int> eightDireciontList = new List<Vector2Int>{
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0,-1), //DOWN
        new Vector2Int(-1,-1), //DOWN-LEFT
        new Vector2Int(-1,0), // LEFT
        new Vector2Int(-1,1) // LEFT-UP
    };

    public static Vector2Int GetRandomCardinalDirection2(){
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}