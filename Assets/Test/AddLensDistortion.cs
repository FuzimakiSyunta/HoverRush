using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AddLensDistortion : MonoBehaviour
{
    private PostProcessVolume volume;
    private LensDistortion lensDistortion;

    void Start()
    {
        lensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        lensDistortion.enabled.Override(true);
        lensDistortion.intensity.Override(50f);  // ˜c‚Ý‚Ì‹­“x

        volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, lensDistortion);
    }

    void OnDestroy()
    {
        RuntimeUtilities.DestroyVolume(volume, true, true);
    }
}
