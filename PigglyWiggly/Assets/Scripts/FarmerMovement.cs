using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmerMovement : MonoBehaviour {

    public float speed = 5f;

    Vector3 targetPosition;
    List<Activities> taskList;
    GameObjectAdmin objectAdmin;

    enum Activities { Cleaning, Feeding, Idle, MoveToPitchfork, MoveToPig, MoveToFood };
    Activities currentActivity;

	void Start () {
        objectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
        targetPosition = this.transform.position;
        currentActivity = Activities.Idle;
        taskList = new List<Activities>();
	}

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            taskList.Clear();
            taskList.Add(Activities.MoveToPitchfork);
            taskList.Add(Activities.MoveToPig);
            taskList.Add(Activities.Cleaning);

            currentActivity = taskList[0];
            DoTask();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            taskList.Clear();
            taskList.Add(Activities.MoveToPitchfork);
            taskList.Add(Activities.MoveToPig);
            taskList.Add(Activities.Cleaning);

            currentActivity = taskList[0];
            DoTask();
        }
    }

    void FixedUpdate()
    {
        Debug.Log(currentActivity);
        if (currentActivity != Activities.Idle && this.GetComponent<NavMeshAgent>().remainingDistance <= 0.5)
        {
            taskList.Remove(taskList[0]);
            currentActivity = Activities.Idle;

            this.GetComponent<NavMeshAgent>().Stop();
            this.GetComponent<NavMeshAgent>().updatePosition = false;
            this.GetComponent<NavMeshAgent>().updateRotation = false;
            Debug.Log("Bumm");

            if (taskList.Count <= 0)
            {
                currentActivity = Activities.Idle;
            }
            else
            {
                DoTask();
            }
        }
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
        else if(currentActivity == Activities.MoveToPig)
        {
            MoveTo(objectAdmin.pigs[this.GetComponent<SelectPig>().currentPig].transform.position);
        }
        else if (currentActivity == Activities.MoveToPitchfork)
        {
            MoveTo(objectAdmin.pitchfork.transform.position);
        }
        else if (currentActivity == Activities.MoveToFood)
        {
            MoveTo(objectAdmin.food.transform.position);
        }
    }

    void Feed()
    {
        Debug.Log("Feed");
    }

    void Clean()
    {
        Debug.Log("Clean");
    }

    void MoveTo(Vector3 position)
    {
        targetPosition = position;
        this.GetComponent<NavMeshAgent>().updatePosition = true;
        this.GetComponent<NavMeshAgent>().updateRotation = true;
        this.GetComponent<NavMeshAgent>().SetDestination(targetPosition);
    }
}
