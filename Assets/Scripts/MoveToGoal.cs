using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using TMPro;
using Unity.Barracuda;

public class MoveToGoal : Agent
{
    public float Movespeed;
    private Vector3 orig;
    private GameObject Target = null;

    private int stepsTaken;
    private float prevDistanceToGoal;

    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float rayDistance = 10f;

    private float minX = -22f;
    private float maxX = 22f;
    private float minZ = -22f;
    private float maxZ = 22f;

    public RayPerceptionSensorComponent3D rayPerceptionSensor;
    public float cellSize = 1.0f;

    private Queue<Vector2Int> recentVisitedStates = new Queue<Vector2Int>();
    public int maxRecentVisitedStates = 5;


    public override void Initialize()
    {
        orig = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Target = this.transform.parent.transform.Find("Goal").gameObject;
    }

    public override void OnEpisodeBegin()
    {
        stepsTaken = 0;
        this.transform.localPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        Target.transform.localPosition = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
        prevDistanceToGoal = Vector3.Distance(transform.localPosition, Target.transform.localPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(Target.transform.localPosition);

    }


    public override void OnActionReceived(ActionBuffers vectorAction)
    {
        float moveX = vectorAction.ContinuousActions[0];
        float moveZ = vectorAction.ContinuousActions[1];
        Target = this.transform.parent.transform.Find("Goal").gameObject;

        float currentDistanceToGoal = Vector3.Distance(transform.localPosition, Target.transform.localPosition);

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;
        Vector3 newPosition = transform.position + moveDirection * Time.deltaTime * Movespeed;
        AddReward(-0.001f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, rayDistance, wallMask))
        {
            if (hit.distance <= 0.5f)
            {
                AddReward(+0.1f);
                return;
            }
        }

        transform.position = newPosition;

        Vector2Int currentState = GetDiscretePosition(transform.localPosition);
        if (recentVisitedStates.Contains(currentState))
        {
            AddReward(-0.2f);
        }

        // Update the recent visited states queue
        recentVisitedStates.Enqueue(currentState);
        if (recentVisitedStates.Count > maxRecentVisitedStates)
        {
            recentVisitedStates.Dequeue();
        }

        float newDistanceToGoal = Vector3.Distance(transform.localPosition, Target.transform.localPosition);
        float distanceReward = (prevDistanceToGoal - newDistanceToGoal) * 0.1f;
        AddReward(distanceReward);

        prevDistanceToGoal = newDistanceToGoal;
        stepsTaken++;
    }

    private Vector2Int GetDiscretePosition(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x / cellSize);
        int z = Mathf.FloorToInt(position.z / cellSize);
        return new Vector2Int(x, z);
    }



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Goal"))
        {
            float reward = 5f ;
            SetReward(reward);
            Debug.Log("Hit Goal");
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            SetReward(-6f);
            Debug.Log("Hit Wall");
            floorMeshRenderer.material = loseMaterial;
        }
    }
}
