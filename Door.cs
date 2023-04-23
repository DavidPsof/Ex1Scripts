using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Переменная для хранения состояния двери (открыта или закрыта)
    public bool isOpen = false;
    private bool _isAction = false;
    public bool locked = false;

    // Угол поворота двери при открытии
    public float openAngle = 90f;

    // Время, за которое дверь открывается/закрывается
    public float duration = 1f;

    // Переменная для хранения времени, прошедшего с начала открытия/закрытия двери
    private float _timer = 0f;

    private void Start()
    {
        GameEvents.current.OnDoorStateChange += ChangeDoorState;
    }

    public void ChangeDoorState(int instanceId)
    {
        if (gameObject.GetInstanceID() != instanceId)
        {
            return;
        }

        if (locked)
        {
            return;
        }

        if (_isAction)
        {
            _timer = 0;
        }

        _isAction = true;
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    // Функция для открытия двери
    public void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;
    }

    // Функция для закрытия двери
    public void CloseDoor()
    {
        if (!isOpen) return;

        isOpen = false;
    }

    // TODO: дыра в производительности: нужно использовать анимации а не рассчитывать кодом
    void Update()
    {
        if (!_isAction)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (isOpen)
        {
            transform.rotation =
                Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, openAngle, 0f), _timer / duration);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0, 0f), _timer / duration);
        }

        if (_timer >= duration)
        {
            _timer = 0f;
            _isAction = false;
        }
    }
}