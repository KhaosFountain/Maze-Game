using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState{
    Available,
    Current,
    Completed
}
public class MazeMap : MonoBehaviour
{
    //List of all the game objects...walls etc
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    public void setState(NodeState state){
        switch(state){
             case NodeState.Available:
             floor.material.color = Color.red;
             break;
             case NodeState.Current:
             floor.material.color = Color.blue;
             break;
             case NodeState.Completed:
             floor.material.color = Color.green;
             break;
            }
    }

}
