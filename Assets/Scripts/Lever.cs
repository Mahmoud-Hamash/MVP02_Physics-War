using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelected()
    {
        Debug.Log("OMG SELECTED OMG");
        handle.transform.rotation = Quaternion.Euler(handle.transform.rotation.eulerAngles.x, 
            handle.transform.rotation.eulerAngles.y, 45f);

    }
}
