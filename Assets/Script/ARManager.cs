using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARManager : MonoBehaviour
{
    public GameObject objectToPlace; 
    public ARRaycastManager ar_raycastManager; 

    void Update()
    {
        UpdateCursor();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // ��ġ�� ������Ʈ ���� ����
        {
            Instantiate(objectToPlace, transform.position, Quaternion.identity);
        }
    }

    void UpdateCursor() 
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ar_raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes); // ����� Ž��

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
