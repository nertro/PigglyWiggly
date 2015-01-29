using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FarmerMovement : MonoBehaviour {

    public float speed = 5f;

    Vector3 targetPosition;
    List<Activities> taskList;
    GameObjectAdmin objectAdmin;
    bool activityStarted;

    enum Activities { Cleaning, Feeding, Idle, MoveToPitchfork, MoveToPig, MoveToFood };
    Activities currentActivity;

	void Start () {
        objectAdmin = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameObjectAdmin>();
        targetPosition = this.transform.position;
        currentActivity = Activities.Idle;
        taskList = new List<Activities>();
        activityStarted = false;
	}

    void Update()
    {

        if (Input.GetAxis("CleanPig") > 0)
        {
            taskList.Clear();
            taskList.Add(Activities.MoveToPitchfork);
            taskList.Add(Activities.MoveToPig);
            taskList.Add(Activities.Cleaning);

            currentActivity = taskList[0];
            DoTask();
        }

        if (Input.GetAxis("FeedPig") > 0)
        {
            taskList.Clear();
            taskList.Add(Activities.MoveToFood);
            taskList.Add(Activities.MoveToPig);
            taskList.Add(Activities.Feeding);

            currentActivity = taskList[0];
            DoTask();
        }
    }

    void FixedUpdate()
    {
        if (currentActivity != Activities.Idle && Vector3.Distance(this.transform.position, this.targetPosition) <= 2)
        {
            Debug.Log(currentActivity);
            taskList.Remove(taskList[0]);

            this.GetComponent<NavMeshAgent>().Stop();
            this.GetComponent<NavMeshAgent>().updatePosition = false;
            this.GetComponent<NavMeshAgent>().updateRotation = false;
            Debug.Log("Bumm");

            if (taskList.Count <= 0)
            {
                Debug.Log("stop");
                activityStarted = false;
                currentActivity = Activities.Idle;
            }
            else
            {
                currentActivity = taskList[0];
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

        if (!activityStarted)
        {
            activityStarted = true;
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
