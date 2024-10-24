using R3;
using UnityEngine;

public interface IPlayerInput
{
    Vector2 Move { get; }
    Vector2 Look { get; }
    Subject<Unit> Attack { get; }
}