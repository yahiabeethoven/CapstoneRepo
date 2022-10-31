using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PipelineRenderer : MonoBehaviour
{
    public RenderPipelineAsset RenderPipelineAsset;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.renderPipeline = RenderPipelineAsset;
    }
}
