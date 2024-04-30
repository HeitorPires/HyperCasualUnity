using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    public Vector2 pastMousePosition;
    public float velocity;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Move(Input.mousePosition.x - pastMousePosition.x);
        }
        pastMousePosition = Input.mousePosition;
    }

    private void Move(float speed)
    {
        transform.position += Vector3.right * speed * Time.deltaTime * velocity;
    }

}
