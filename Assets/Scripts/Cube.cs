using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _baseColor = Color.white;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private ColorChanger _colorChanger;

    private bool _hasFirstCollision = false;
    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public bool HasFirstCollision => _hasFirstCollision;

    public event Action<Cube> FirstPlatformCollision;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _collisionHandler.HitPlatform += HandlePlatformCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.HitPlatform -= HandlePlatformCollision;
    }

    public void HandlePlatformCollision()
    {
        if (_hasFirstCollision)
            return;

        _hasFirstCollision = true;

        _colorChanger.ChangeColor(this);
        FirstPlatformCollision?.Invoke(this);
    }

    public void ResetStats()
    {
        _hasFirstCollision = false;
        this._renderer.material.color = _baseColor;
        this._rigidbody.velocity = Vector3.zero;
        this._rigidbody.angularVelocity = Vector3.zero;
    }
}