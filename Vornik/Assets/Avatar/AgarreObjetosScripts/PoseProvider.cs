using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

namespace SimpleVR
{
    
    public class PoseProvider : BasePoseProvider
    {
        public Transform pivot;

        public override bool TryGetPoseFromProvider(out Pose output)
        {
            if (pivot != null)
            {
                output = new Pose(pivot.position, pivot.rotation);
                return gameObject.activeInHierarchy;
            }
            else
            {
                output = new Pose(transform.position, transform.rotation);
                return gameObject.activeInHierarchy;
            }
        }
    }
}