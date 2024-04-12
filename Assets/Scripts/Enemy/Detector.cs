using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float detectorSize;
    [SerializeField] private Vector3 detectorPositionOffset;
    [SerializeField] private LayerMask layerToDetect;

    public bool isInDetectArea()
    {
        bool isInArea = GetCollidersInDetectAreaSphere().Length > 0;     
        return isInArea;
    }

    public Collider[] GetCollidersInDetectAreaSphere()
    {
        return Physics.OverlapSphere(transform.position + detectorPositionOffset, 
            detectorSize, layerToDetect);
    }

    public Collider[] GetCollidersInDetectAreaBox(Vector3 centerPosition, Vector3 boxSize)
    {
        return Physics.OverlapBox(centerPosition, boxSize, Quaternion.identity, layerToDetect);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position + detectorPositionOffset, detectorSize);
    }
}
