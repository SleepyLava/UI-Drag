using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Drag : MonoBehaviour
{
    public bool inUse;

    bool startDrag;
    GameObject slotPos;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (startDrag)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void StartDragUI()
    {
        startDrag = true;
    }

    public void StopDragUI()
    {
        startDrag = false;

        if (inUse)
        {
            if (slotPos.transform.childCount > 0) 
            {
                slotPos.transform.GetChild(0).GetComponent<UI_Drag>().inUse = false;
                slotPos.transform.GetChild(0).GetComponent<UI_Drag>().StopDragUI();
            }

            transform.SetParent(slotPos.transform);
            transform.position = slotPos.transform.position;
        }
        else
        {
            transform.SetParent(GameObject.Find("Canvas").transform, false);
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot"))
        {
            inUse = true;
            slotPos = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot"))
        {
            inUse = false;
        }
        /*if (collision.CompareTag("Area"))
        {
            startDrag = false;
            transform.position = startPos;
        }*/
    }
}
