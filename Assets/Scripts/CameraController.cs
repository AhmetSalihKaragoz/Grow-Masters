using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] Transform cameraEndPosition;


    private bool isLevelEnd = false;
    private Vector3 _cameraOffset;
    private float size_X;


    private void Start()
    {
        _camera = Camera.main;
        _cameraOffset = _camera.transform.position - transform.position;

    }
    private void LateUpdate()
    {
        
        CameraFollow();
    }
    private void CameraFollow()
    {
        if (!isLevelEnd)
        {
            size_X = transform.localScale.x / 3;
            var camOfSet = _cameraOffset * size_X;
            camOfSet = new Vector3(0, Mathf.Clamp(camOfSet.y, 8, 16), Mathf.Clamp(camOfSet.z, -20, -5));
            Vector3 pos = new Vector3(0, camOfSet.y, (transform.position.z + camOfSet.z));
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, pos, Time.deltaTime * 4);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FinishLine"))
        {
            isLevelEnd = true;
            _camera.transform.DOMove(cameraEndPosition.transform.position, 2.5f);
            _camera.transform.DORotateQuaternion(cameraEndPosition.transform.rotation, 2.5f);
        }
    }

    
}
