using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public void ChangeColor(Cube cube)
    {
        if (cube.TryGetComponent<Renderer>(out Renderer renderer))
            renderer.material.color = Random.ColorHSV();
    }
}
