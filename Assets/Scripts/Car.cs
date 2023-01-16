using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _speedGainPerSecond = 2f;
    [SerializeField] private float _turnSpeed = 200f;

    private bool _isPressed = false;

    private int _steerValue;



    // Update
    private void Update()
    {
        if (_isPressed != true)
        {
            _speed += _speedGainPerSecond * Time.deltaTime;

            transform.Rotate(0f, _steerValue * _turnSpeed * Time.deltaTime, 0f);

            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
        else
        {
            _speed -= 5 * Time.deltaTime;
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        }
        

    }

    // OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }

    }

    // Steer
    public void Steer(int value)
    {
        _steerValue = value;        

    }

    public void SlowDown(bool value)
    {
        _isPressed = value;        

    }
   
}
