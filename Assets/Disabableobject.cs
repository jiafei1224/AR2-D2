using UnityEngine;
using System.Collections.Generic;

public class Disabableobject : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        // Check if the target object is currently active in the scene
        if (targetObject.activeInHierarchy)
        {
            // Get all the child GameObjects of the target object
            GameObject[] childObjects = GetAllChildObjects(targetObject);

            // Loop through all the child objects and disable their Mesh Renderer components
            foreach (GameObject childObject in childObjects)
            {
                MeshRenderer meshRenderer = childObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }
        }
    }

    // Helper method to recursively get all child objects of a parent object
    GameObject[] GetAllChildObjects(GameObject parent)
    {
        List<GameObject> childList = new List<GameObject>();
        foreach (Transform child in parent.transform)
        {
            childList.Add(child.gameObject);
            childList.AddRange(GetAllChildObjects(child.gameObject));
        }
        return childList.ToArray();
    }
}
