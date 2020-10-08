
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowadownLength = 2f;

    private void Update()
    {
        Time.timeScale += (1f / slowadownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void BulletTime()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

}
