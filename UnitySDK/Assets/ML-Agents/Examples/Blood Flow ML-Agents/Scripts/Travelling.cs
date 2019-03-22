using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Travelling : MonoBehaviour {

    Camera camera;
    NavMeshAgent agent;
    private void Start()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
        agent = GetComponent<NavMeshAgent> ();
    }
    void Update () {
		if (Input.GetMouseButtonDown(0))
        {
            MoveToPoint();
        }
	}




    public void MoveToPoint()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Place"))
        {
            agent.SetDestination(hit.point);
        }
    }

    public void MoveTo(Vector3 location)
    {
        agent.SetDestination(location);
    }

}
