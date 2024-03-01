using UnityEngine;

public class Broom : MonoBehaviour
{
    private float pointsPerCobwebf = 10f;
    private int pointsPerCobweb;

    
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the "Cobweb" tag
        if (other.CompareTag("Cobweb"))
        {
            // Destroy the cobweb GameObject
            Destroy(other.gameObject);
            pointsPerCobweb = Mathf.CeilToInt(pointsPerCobwebf);
            GameManager.instance.AddScore(pointsPerCobweb);
            GameManager.instance.webGotBunkedInBack();
            pointsPerCobwebf = pointsPerCobwebf + 9 / 8 * pointsPerCobweb;
        }
    }
}
