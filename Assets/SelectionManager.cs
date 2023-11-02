using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField] Camera cam;
    [SerializeField] NavMeshAgent selectedAgent;
    [SerializeField] List<NavMeshAgent> selectedObjects;

    [SerializeField] InputAction selectionAction, clearAction, groupSelectAction;
    
    // Start is called before the first frame update
    void Start()
    {
        selectedObjects = new List<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        bool found = false;

        if (selectionAction.WasPressedThisFrame())
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    if (!groupSelectAction.IsPressed())
                    {
                        ClearSelectedList();
                    }

                    selectedAgent = hit.collider.gameObject.GetComponentInParent<NavMeshAgent>();

                    if (!selectedObjects.Contains(selectedAgent))
                    {
                        selectedObjects.Add(selectedAgent);
                        selectedAgent.gameObject.GetComponent<SelectableObject>().SetSelected(true);
                    }
                }
                else
                {
                    for (int i = 0; i < selectedObjects.Count; i++)
                    {
                        selectedObjects[i].destination = hit.point;
                    }
                }
            }
        }
        if (clearAction.WasPressedThisFrame())
        {
            ClearSelectedList();
        }
    }

    void ClearSelectedList()
    {
        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].GetComponent<SelectableObject>().SetSelected(false);  
        }
        selectedObjects.Clear();
    }

    private void OnEnable()
    {
        selectionAction.Enable();
        clearAction.Enable();
        groupSelectAction.Enable();
    }

    private void OnDisable()
    {
        selectionAction.Disable();
        clearAction.Disable();
        groupSelectAction.Disable();
    }
}
