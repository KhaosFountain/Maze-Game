using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


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
public class MoveToGoal : Agent{

    [SerializeField] private Transform targetTransform;

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.position);
        sensor.AddObservation(targetTransform.position);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        float moveSpeed = 8f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut){
       ActionSegment<float> continiousActions = actionsOut.ContinuousActions;
       continiousActions[0] = Input.GetAxisRaw("Horizontal");
       continiousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other){

        if(other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(+5f);
            transform.position = new Vector3(0, 2, 0);
        }
        else if (other.TryGetComponent<Wall>(out Wall wall)){
            SetReward(-10f);
            transform.position = new Vector3(0, 2, 0);
        }
    }


}
