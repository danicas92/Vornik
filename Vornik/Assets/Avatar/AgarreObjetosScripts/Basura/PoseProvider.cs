using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

namespace SimpleVR
{
    public class PoseProvider : BasePoseProvider
    {
        Pose output;
        
        public override bool TryGetPoseFromProvider(out Pose output)
        {
            output = new Pose(transform.position, transform.rotation);
            return gameObject.activeInHierarchy;
        }

        public Pose GetPose()
        {
            output = new Pose(transform.position, transform.rotation);
            return output;
        }
    }
}