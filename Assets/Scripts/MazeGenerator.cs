using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    //create 2 variables..one of the maze map and the other for the size of the map.
    [SerializeField] MazeMap nodePrefab;
    [SerializeField] Vector2Int mazeSize;


    //mainb method of the maze generatod...we will have vibratnt colours so we can see it better.
    private void Start(){
        StartCoroutine(GenerateMaze(mazeSize));
    }

    IEnumerator GenerateMaze(Vector2Int size){
         //list of all the nodes created.
         List<MazeMap> nodes = new List<MazeMap>(); 

         //creating nodes.
         // 2 for loops one for the x axis and one for the y axis.
         for(int x = 0; x < size.x; x++){
             for(int y = 0; y < size.y; y++){
                 //center the code at (0, 0) instead of random locations
                 Vector3 nodePos = new Vector3(x - (size.x /2f), 0, y - (size.y / 2f));
                 // instantiate the new node...set it as parent
                 MazeMap newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                 nodes.Add(newNode);

                 yield return null;
             }
         }
    }
}