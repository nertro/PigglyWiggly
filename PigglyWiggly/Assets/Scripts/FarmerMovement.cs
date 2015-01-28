using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmerMovement : MonoBehaviour {

    public float speed = 5f;

    List<GameObject> pigs;
    Vector3 targetPosition;
    List<Activities> taskList;

    enum Activities { Cleaning, Feeding, Idle, MoveTo };
    Activities currentActivity;

	void Start () {
        pigs = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>().pigs;
        targetPosition = this.transform.position;
        currentActivity = Activities.Idle;
        taskList = new List<Activities>();
	}

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            taskList.Clear();
            taskList.Add(Activities.MoveTo(
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentActivity = Activities.Feeding;
        }
    }

    void FixedUpdate()
    {
        Debug.Log(currentActivity);
        if (currentActivity != Activities.Idle && this.GetComponent<NavMeshAgent>().remainingDistance <= 0.5)
        {
            this.GetComponent<NavMeshAgent>().Stop();
            this.GetComponent<NavMeshAgent>().updatePosition = false;
            this.GetComponent<NavMeshAgent>().updateRotation = false;
            Debug.Log("Bumm");

            DoTask();
        }
    }

    void MoveToP(Vector3 position)
    {
        targetPosition = position;
        //targetPosition = pigs[this.GetComponent<SelectPig>().currentPig].transform.position;
        //rigidbody.velocity = (this.targetPosition - this.transform.position).normalized * speed;
        this.GetComponent<NavMeshAgent>().updatePosition = true;
        this.GetComponent<NavMeshAgent>().updateRotation = true;
        this.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
    }

    void DoTask()
    {
        if (currentActivity == Activities.Feeding)
        {
            Feed();
        }
        else if (currentActivity == Activities.Cleaning)
        {
            Clean();
        }
    }

    void Feed()
    {
        Debug.Log("Feed");

        currentActivity = Activities.Idle;
    }

    void Clean()
    {
        Debug.Log("Clean");

        currentActivity = Activities.Idle;
    }
}
