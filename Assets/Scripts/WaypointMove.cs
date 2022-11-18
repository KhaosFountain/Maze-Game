using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour
{
    //Stores reference to the way poit system this object will use
    [SerializeField] private WayPoints waypoints;
    [SerializeField] private float speed = 5f;

//current waypoint we are moving to
    private Transform currentWaypoint;

    void Start(){
        // Set initial position to th first waypoint
        currentWaypoint = waypoints.getNextWaypoint(currentWaypoint);
        transform.position = currentWaypoint.position;
        // use this code to create waypoints to move the cube back and forward.
    }

    void update(){

    }
    // rest of the video
    //https://www.youtube.com/watch?v=6tMwi-hBxnE
    // im around 1:30 in the video.
}
