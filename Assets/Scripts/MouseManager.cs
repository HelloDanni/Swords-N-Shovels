using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{

    // What objects are clickable
    public LayerMask clickableLayer;
    public Texture2D pointer; // normal pointer
    public Texture2D target; // cursor for clickable objects
    public Texture2D doorway; // cursor for doorways
    public EventVector3 OnClickEnvironment;
    

    // Update is called once per frame
    void Update()
    {        
        RaycastHit hit;

        // Change cursor based on what object mouse is over, and move to that object

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;

            if (hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16, 16), CursorMode.Auto);
                door = true;
            }
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(target, new Vector2(16,16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(pointer, new Vector2(16, 16), CursorMode.Auto);
            }

            // Click to move to cursor

            if (Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorway = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(doorway.position); // Move to door
                }
                else if(item)
                {
                    Transform itemLocation = hit.collider.gameObject.transform;
                    OnClickEnvironment.Invoke(itemLocation.position); // Move to item
                }
                else
                {
                    OnClickEnvironment.Invoke(hit.point); // Move to pointer
                }
                
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
