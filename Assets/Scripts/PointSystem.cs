using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public string targetTag = "Bibelou";  // Specify the tag of the target GameObject
    public int pointsPerInterval = 5;    // Points to be awarded per interval
    public float intervalDuration = 5f;    // Interval duration in seconds

    private float timer = 0f;

    private void Update()
    {
        // Check if the player is looking at the GameObject with the specified tag
        if (IsPlayerLookingAtObjectWithTag())
        {
            // Update the timer
            timer += Time.deltaTime;

            // Check if the interval has passed
            if (timer >= intervalDuration)
            {
                // Add points and reset the timer
                AddPoints();
                timer = 0f;
            }
        }
        else
        {
            // Reset the timer if the player is not looking at the target GameObject
            timer = 0f;
        }
    }

    private bool IsPlayerLookingAtObjectWithTag()
    {
        // Raycast from the camera's position forward
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Check if the ray hits a GameObject with the specified tag
        if (Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.CompareTag(targetTag))
        {
            return true;
        }

        return false;
    }

    private void AddPoints()
    {
        // Add points to the GameManager (you may need to modify this based on your GameManager script)
        GameManager.instance.AddScore(pointsPerInterval);

        // You can add additional logic here if needed
    }
}
