using System.Collections;
using Meta.XR.EnvironmentDepth;
using UnityEngine;

// https://github.com/oculus-samples/Unity-DepthAPI/issues/72#issuecomment-2779503628
public class EnvironmentDepthFix : MonoBehaviour
{
    [SerializeField] private OVRManager _ovrManager;
    [SerializeField] private EnvironmentDepthManager _environmentDepthManager;
    
    private void Start()
    {
        Invoke(nameof(EnableEnvironmentalDepth), 2f);
    }

    private void EnableEnvironmentalDepth()
    {
        StartCoroutine(EnableEnvironmentalDepthCoroutine());
    }

    public IEnumerator EnableEnvironmentalDepthCoroutine()
    {
        _ovrManager.isInsightPassthroughEnabled = true;

        if (!EnvironmentDepthManager.IsSupported)
            yield break;

         // enable depth with soft occlusions
        _environmentDepthManager.enabled = true;
        _environmentDepthManager.OcclusionShadersMode = OcclusionShadersMode.SoftOcclusion;
        _environmentDepthManager.RemoveHands = true;

        // wait until the depth texture becomes available
        while (!_environmentDepthManager.IsDepthAvailable)
            yield return null;
    }
}
