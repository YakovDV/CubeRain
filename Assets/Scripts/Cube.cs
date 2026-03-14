using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _baseColor = Color.white;

    public Rigidbody Rigidbody => GetComponent<Rigidbody>();
    public bool HasFirstCollision => _hasFirstCollision;

    private bool _hasFirstCollision = false;

    public event Action<Cube, Collision> FirstCollisionHappened;

    private void OnCollisionEnter(Collision collision)
    {
        FirstCollisionHappened?.Invoke(this, collision);
    }

    public void ChangeCollisionStatus()
    {
        _hasFirstCollision = true;
    }

    public void ResetStats()
    {
        _hasFirstCollision = false;
        this.GetComponent<Renderer>().material.color = _baseColor;
        this.Rigidbody.velocity = Vector3.zero;
        this.Rigidbody.angularVelocity = Vector3.zero;
    }
}