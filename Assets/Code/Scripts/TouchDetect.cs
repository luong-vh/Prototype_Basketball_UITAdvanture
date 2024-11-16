using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class TouchDetect : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float maximumTime = 1.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private float startTime;
    private float endTime;

    public UnityEvent SwipedEvent;
    public Vector2Variable power;
    private Camera mainCamera;

    private Touch theTouch;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if(theTouch.phase == TouchPhase.Began )
            {
                startPosition = ScreenToWorld(mainCamera,theTouch.position);
                startTime = Time.time;
            }
            else if(theTouch.phase == TouchPhase.Ended ) 
            { 
               endPosition = ScreenToWorld(mainCamera, theTouch.position);
                endTime = Time.time;
                DetectSwipe();
            }
        }
        
    }
    private void DetectSwipe()
    {
       if (Vector3.Distance(startPosition, endPosition) > minimumDistance && endTime - startTime <= maximumTime)
            {
            power.SetValue(endPosition - startPosition);
            SwipedEvent.Invoke();
           // Ball.Instance.Throw(endPosition - startPosition);


            }
    }
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
