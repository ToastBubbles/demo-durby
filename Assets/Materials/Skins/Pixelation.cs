using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Pixelation : ScriptableRendererFeature
{
    class CustomRenderPass : ScriptableRenderPass
    {
        public RenderTargetIdentifier source;
        private Material mat;
        private RenderTargetHandle tempRenderHandler;

        public CustomRenderPass(Material mat)
        {
            this.mat = mat;
            tempRenderHandler.Init("_TemoraryColorTexture");
        }
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer commandBuffer = CommandBufferPool.Get();

            commandBuffer.GetTemporaryRT(tempRenderHandler.id, renderingData.cameraData.cameraTargetDescriptor);
            Blit(commandBuffer, source, tempRenderHandler.Identifier(), mat);
            Blit(commandBuffer, tempRenderHandler.Identifier(), source);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }
    }

    [System.Serializable]
    public class Settings
    {
        public Material mat = null;
    }
    public Settings settings = new Settings();


    CustomRenderPass m_ScriptablePass;


    public override void Create()
    {
        m_ScriptablePass = new CustomRenderPass(settings.mat);


        m_ScriptablePass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_ScriptablePass.source = renderer.cameraColorTarget;
        renderer.EnqueuePass(m_ScriptablePass);
    }
}


