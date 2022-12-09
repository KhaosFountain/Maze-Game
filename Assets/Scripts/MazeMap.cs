using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}

public class MazeMap : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    public void RemoveWall(int wallToRemove){
        walls[wallToRemove].gameObject.SetActive(false);
    }

    public void SetState(NodeState state)
    {
        switch (state)
        {
            case NodeState.Available:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = new Color(255, 230, 0);
                break;
            case NodeState.Completed:
                floor.material.color = new Color(255, 255, 255);
                break;
        }
    }
}