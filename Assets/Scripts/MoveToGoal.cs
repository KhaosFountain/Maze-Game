using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.Barracuda;


/*agent learns through reinforcement learning which involves some steps
   1. Observatoin
       - gathers data from its enviornment
   2. Decision
       - after it gathers information it makes a decision based on that data.
   3. Action
       - based on the decision it takes a action
   4. Reward
       - if the action is correct the agent gets a reward...if the action is incorrect then no reward.
  
*/



public class MoveToGoal : Agent
{
    public float Movespeed = 10;
    private Vector3 orig;
    private GameObject Target = null;

    private float degreePerSecond = 4;
    private bool rotate = true;

    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    public override void Initialize()
    { 
        orig = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Target = this.transform.parent.transform.Find("Goal").gameObject;
        
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(25f, -45f), 0, Random.Range(-44f, 24f));
        Target.transform.localPosition = new Vector3(Random.Range(25f, -45f), 0, Random.Range(-44f, 24f));

    }
    public override void OnActionReceived(ActionBuffers vectorAction)
    {

        float moveX = vectorAction.ContinuousActions[0];
        float moveZ = vectorAction.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * Movespeed;


    }
    
        void OnCollisionEnter(Collision col){
        if(col.gameObject.CompareTag("Goal") ){
            SetReward(+10f);
            Debug.Log("Hit Goal");
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        else if (col.gameObject.CompareTag("Wall")){
            SetReward(-50f);
            Debug.Log("Hit Wall");
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }

    }
}



