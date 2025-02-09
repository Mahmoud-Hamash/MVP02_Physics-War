using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Debug.Log(
                "colisiono con letra");
            collision.gameObject.GetComponent<LetterInteraction>().OnSelectEntered();
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
