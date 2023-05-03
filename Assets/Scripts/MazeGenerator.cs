using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    //create 2 variables..one of the maze map and the other for the size of the map.
    [SerializeField] MazeMap nodePrefab;
    [SerializeField] Vector2Int mazeSize;

    [SerializeField] float nodeSize;

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject target;
    [SerializeField] public GameObject flag;
    public GameObject gameCamera;

    private float minX = -45f;
    private float maxX = 25f;
    private float minZ = -44f;
    private float maxZ = 24f;

    //list of all the nodes created.
    private List<MazeMap> nodes = new List<MazeMap>();
    public List<MazeMap> CompNodes = new List<MazeMap>();




    //mainb method of the maze generatod...we will have vibratnt colours so we can see it better.

    private void Start(){
        StartCoroutine(GenerateMaze(mazeSize));
    }

    [System.Obsolete]
    IEnumerator GenerateMaze(Vector2Int size){
 


         //creating nodes.
         // 2 for loops one for the x axis and one for the y axis.
         for(int x = 0; x < size.x; x++){
             for(int y = 0; y < size.y; y++){
                 //center the code at (0, 0) instead of random locations
                 Vector3 nodePos = new Vector3(x - (size.x/2f), 0, y - (size.y/2f)) * nodeSize;
                 // instantiate the new node...set it as parent
                 MazeMap newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                 nodes.Add(newNode);

                 yield return null;
             }
         }

         //Selecting starting path to generate the maze
         List<MazeMap> currentPath = new List<MazeMap>();
         List<MazeMap> completedNodes = new List<MazeMap>();

        
        Random.seed = System.DateTime.Now.Millisecond;

         currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
         currentPath[0].SetState(NodeState.Current);

         //create loop to generate the next node and load the map
         while(completedNodes.Count < nodes.Count){
             //check nodes
             List<int> possNextNodes = new List<int>();
             List<int> possDirections = new List<int>();

             //we are getting the poistion so the node can back tack to find a un-used node and continue generating the map
             int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count -1]);
             int currentNodeX = currentNodeIndex / size.y;
             int currentNodeY = currentNodeIndex % size.y;

            

             if(currentNodeX < size.x - 1){
                 if(!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y])){
                     // telling the current node what direction it can move in

                     possDirections.Add(1);
                     possNextNodes.Add(currentNodeIndex + size.y);
                 }
             }
             // same as the if statement before but we are checking if the current ndoe is near the left edge wall
             if(currentNodeX > 0){
                   
                 if(!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y])){
                     possDirections.Add(2);
                     possNextNodes.Add(currentNodeIndex - size.y);
                 }
             }

            // checking the node above the current node
             if(currentNodeY < size.y -1 ){
                 if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1])){
                     possDirections.Add(3);
                     possNextNodes.Add(currentNodeIndex + 1);
                 }
             }

             // checking the node below the current node
             if(currentNodeY >  0 ){
                 if(!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1])){
                     possDirections.Add(4);
                     possNextNodes.Add(currentNodeIndex - 1);
                 }
             }

             //code to choose the next node
             if(possDirections.Count > 0){
                 int choseDir = Random.Range(0, possDirections.Count);
                 MazeMap choseNode = nodes[possNextNodes[choseDir]];

                 switch(possDirections[choseDir]){
                     case 1: 
                     choseNode.RemoveWall(1);
                     currentPath[currentPath.Count-1].RemoveWall(0);
                     break;
                     case 2:
                     choseNode.RemoveWall(0);
                     currentPath[currentPath.Count-1].RemoveWall(1);
                     break;
                     case 3:
                     choseNode.RemoveWall(3);
                     currentPath[currentPath.Count-1].RemoveWall(2);
                     break;
                     case 4:
                     choseNode.RemoveWall(2);
                     currentPath[currentPath.Count-1].RemoveWall(3);
                     break;
                 }


                 currentPath.Add(choseNode);
                 choseNode.SetState(NodeState.Current);
             }
             else{
                 completedNodes.Add(currentPath[currentPath.Count-1]);
                CompNodes.Add(currentPath[currentPath.Count-1]);

                 currentPath[currentPath.Count-1].SetState(NodeState.Completed);
                 currentPath.RemoveAt(currentPath.Count-1);
             }

                // use this for demo purposes only to show the random map generation with colour
               //yield return new WaitForSeconds(2f);

         }
         player.transform.localPosition = new Vector2(1, 1);
         player.transform.localRotation = Quaternion.Euler(0, 0, 0);
        flag.transform.localPosition = new Vector3(Random.Range(maxX, minX), 1, Random.Range(maxZ, minZ));
    }
}