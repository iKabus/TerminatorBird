using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action Jumping;
    public event Action Shooting;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shooting?.Invoke();
        }
    }
}
