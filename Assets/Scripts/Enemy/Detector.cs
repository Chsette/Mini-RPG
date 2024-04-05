using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] private float detectorSize;
    [SerializeField] private Vector3 detectorPositionOffset;
    [SerializeField] private LayerMask layerToDetect;

    public bool isInDetectArea()
    {
        bool isInArea = GetCollidersInDetectArea().Length > 0;     
        return isInArea;
    }

    public Collider[] GetCollidersInDetectArea()
    {
        return Physics.OverlapSphere(transform.position + detectorPositionOffset, 
            detectorSize, layerToDetect);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + detectorPositionOffset, detectorSize);
    }
}
