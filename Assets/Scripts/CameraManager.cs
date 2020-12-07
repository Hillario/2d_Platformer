using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
   
    private Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        //get component
        targetPosition = target.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(targetPosition.position.x, targetPosition.position.y, -1);
    }
}
