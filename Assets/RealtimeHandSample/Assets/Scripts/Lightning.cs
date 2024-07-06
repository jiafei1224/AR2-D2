
using UnityEngine;
using RTHand;

public class Lightning : MonoBehaviour
{
    [SerializeField]
    public LineRenderer lineRenderer;
  
    [SerializeField]
    public RealtimeHandManager handManager;

    [SerializeField]
    public Vector2 screenPos;

    public float jointDistance;

    public Vector3 temp;
    public Vector3 range; 
    public Quaternion rotation;
    public Vector3 indexFinger;
    public Vector3 ThumbFinger;
    // public PandaControlIKKeyboard pandascript;

  
    protected void Start()
    {

        // pandascript = GameObject.FindObjectOfType<PandaControlIKKeyboard>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;

        handManager.HandUpdated += OnHandUpdated;
    }

    protected void OnDestroy()
    {
        if (handManager != null)
        {
            handManager.HandUpdated -= OnHandUpdated;
        }
    }

    public void OnHandUpdated(RealtimeHand _realtimeHand)
    {
        lineRenderer.enabled = _realtimeHand.IsVisible;

        if (_realtimeHand.IsVisible)
        {
            var thumbWorldPos = _realtimeHand.Joints[JointName.thumbTip].worldPos;
            var indexWorldPos = _realtimeHand.Joints[JointName.indexTip].worldPos;
            var Index2Worldpos = _realtimeHand.Joints[JointName.indexDIP].worldPos;
            // pandascript.updatecoord(indexWorldPos);
            // pandascript.updatescore(thumbWorldPos-indexWorldPos);
            jointDistance = _realtimeHand.Joints[JointName.indexTip].distance;
            screenPos = _realtimeHand.Joints[JointName.indexTip].screenPos;
            temp = indexWorldPos;
            range = thumbWorldPos-indexWorldPos;
            rotation = Quaternion.LookRotation(temp);
            indexFinger = indexWorldPos;
            ThumbFinger = Index2Worldpos;
            lineRenderer.SetPosition(0, thumbWorldPos);
            lineRenderer.SetPosition(1, indexWorldPos);
        }
    }

    public float GetDistance() {
        return jointDistance;
    }

    public Vector2 GetScreenCoords() {
        return screenPos;
    }

    public Vector3 Getvariable(){
        return temp;
    }

    public Vector3 Range(){
        return range;
    }

    public Quaternion Rotation(){
        return rotation;
    }

    public Vector3 Indexfinger(){
        return indexFinger;
    }

    public Vector3 ThumbFingers(){
        return ThumbFinger;
    }

}
