using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speed = 10;

    [SerializeField] private AudioSource _audioSource;

    private Camera _mainCamera;
    private bool _isActive;

    public Action OnGameOver;

    private Vector3 resetPosition;

    public void Awake()
    {
        _mainCamera = Camera.main;
        resetPosition = transform.position;
    }

    public void Update()
    {
        if (_isActive)
        {
            if (_mainCamera != null)
            {
                var t = transform.eulerAngles;
                transform.LookAt(_mainCamera.transform);
                transform.eulerAngles = new Vector3(t.x, transform.eulerAngles.y, t.z);
            }

            //moving

            if (Vector3.Distance(transform.position, _targetTransform.position) > 2)
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else
            {
                OnGameOver?.Invoke();
            }
        }
    }

    public void StartGame()
    {
        transform.position = resetPosition;
        _isActive = true;
        _audioSource.Play();
    }

    public void Reset()
    {
        _isActive = false;
        transform.position = resetPosition;
    }
}