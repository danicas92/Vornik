using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

namespace SimpleVR
{
    public class PoseProvider : BasePoseProvider
    {
        public override bool TryGetPoseFromProvider(out Pose output)
        {
            output = new Pose(transform.position, transform.rotation);
            return gameObject.activeInHierarchy;
        }
    }
}