using UnityEngine;

public class PointInTime
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;
    public Vector3 angularVelocity;

    public PointInTime(Vector3 _position, Quaternion _rotation, Vector3 _velocity, Vector3 _angularVeolcity)
    {
        position = _position;
        rotation = _rotation;
        velocity = _velocity;
        angularVelocity = _angularVeolcity;

    }
}
