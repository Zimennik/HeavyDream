using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyController : MonoBehaviour
{
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _speed = 10;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _pootisSource;
    [SerializeField] private AudioClip _startAudio;
    [SerializeField] private AudioClip _gameOverAudio;


    private Camera _mainCamera;
    private bool _isActive;

    public Action OnGameOver;

    private Vector3 resetPosition = new Vector3(0, 0, -50);

    public void Awake()
    {
        _mainCamera = Camera.main;
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

            var pos = new Vector3(_targetTransform.position.x,0,_targetTransform.position.z);
            if (Vector3.Distance(transform.position, pos) > 3)
            {
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            }
            else
            {
                OnGameOver?.Invoke();
                _audioSource.PlayOneShot(_gameOverAudio);
                _pootisSource.Stop();
                _isActive = false;
            }
        }
    }

    public void StartGame()
    {
        transform.position = resetPosition;
        _isActive = true;
        _audioSource.PlayOneShot(_startAudio);
        _pootisSource.Play();
    }

    public void StopGame()
    {
        _isActive = false;
        _pootisSource.Stop();
    }

    public void Reset()
    {
        _isActive = false;
        _pootisSource.Stop();
        transform.position = resetPosition;
    }

}