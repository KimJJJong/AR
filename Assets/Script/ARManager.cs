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

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // 터치시 오브젝트 생성 조건
        {
            Instantiate(objectToPlace, transform.position, Quaternion.identity);
        }
    }

    void UpdateCursor() 
    {
        Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        ar_raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes); // 평면을 탐지

        if (hits.Count > 0)
        {
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
        }
    }
}
