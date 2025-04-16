using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DelayedEvents : MonoBehaviour
{
    // Struct to hold UnityEvent and delay time
    [System.Serializable]
    public struct DelayedEvent
    {
        public UnityEvent unityEvent;
        public float delayTime;
    }

    // Array of DelayedEvent
    public DelayedEvent[] delayedEvents;

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine for each delayed event
        foreach (var delayedEvent in delayedEvents)
        {
            StartCoroutine(InvokeEventAfterDelay(delayedEvent));
        }
    }

    // Coroutine to handle the delay for each event
    private IEnumerator InvokeEventAfterDelay(DelayedEvent delayedEvent)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delayedEvent.delayTime);
        
        // Invoke the UnityEvent
        delayedEvent.unityEvent.Invoke();
    }
}
