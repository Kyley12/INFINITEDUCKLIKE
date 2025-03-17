////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>. All rights reserved.
//
// THIS FILE CAN NOT BE HOSTED IN PUBLIC REPOSITORIES.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
#if UNITY_6000_0_OR_NEWER
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
#endif

namespace FronkonGames.Retro.OldComputers
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Render Pass. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class OldComputers
  {
    [DisallowMultipleRendererFeature]
    private sealed class RenderPass : ScriptableRenderPass
    {
      // Internal use only.
      internal Material material { get; set; }

      private readonly Settings settings;

#if UNITY_6000_0_OR_NEWER
#else
      private RenderTargetIdentifier colorBuffer;
      private RenderTextureDescriptor renderTextureDescriptor;

      private readonly int renderTextureHandle0 = Shader.PropertyToID($"{Constants.Asset.AssemblyName}.RTH0");

      private const string CommandBufferName = Constants.Asset.AssemblyName;

      private ProfilingScope profilingScope;
      private readonly ProfilingSampler profilingSamples = new(Constants.Asset.AssemblyName);
#endif

      private static class ShaderIDs
      {
        public static readonly int Intensity = Shader.PropertyToID("_Intensity");

        public static readonly int Pixelation = Shader.PropertyToID("_PixelSize");
        public static readonly int Dithering = Shader.PropertyToID("_Dithering");
        public static readonly int RangeMin = Shader.PropertyToID("_RangeMin");
        public static readonly int RangeMax = Shader.PropertyToID("_RangeMax");
        public static readonly int Mode = Shader.PropertyToID("_Mode");
        public static readonly int CustomPalette = Shader.PropertyToID("_CustomPalette");
        public static readonly int CustomPaletteColors = Shader.PropertyToID("_CustomPaletteColors");
        
        public static readonly int Brightness = Shader.PropertyToID("_Brightness");
        public static readonly int Contrast = Shader.PropertyToID("_Contrast");
        public static readonly int Gamma = Shader.PropertyToID("_Gamma");
        public static readonly int Hue = Shader.PropertyToID("_Hue");
        public static readonly int Saturation = Shader.PropertyToID("_Saturation");      
      }

      private static class Keywords
      {
        public const string APPLE_II = "APPLE_II";
        public const string CGA = "CGA";
        public const string BBC_MICRO = "BBC_MICRO";
        public const string COMMODORE_64 = "COMMODORE_64";
        public const string MSX = "MSX";
        public const string MATTEL_AQUARIUS = "MATTEL_AQUARIUS";
        public const string AMSTRAD_CPC = "AMSTRAD_CPC";
        public const string ATARI_ST = "ATARI_ST";
        public const string EGA = "EGA";
        public const string MAC_II = "MAC_II";
        public const string GAME_BOY = "GAME_BOY";
      }

      /// <summary> Render pass constructor. </summary>
      public RenderPass(Settings settings) : base()
      {
        this.settings = settings;
#if UNITY_6000_0_OR_NEWER
        profilingSampler = new ProfilingSampler(Constants.Asset.AssemblyName);
#endif
      }

      /// <summary> Destroy the render pass. </summary>
      ~RenderPass() => material = null;

      private void UpdateMaterial()
      {
        material.shaderKeywords = null;
        material.SetFloat(ShaderIDs.Intensity, settings.intensity);

        switch (settings.computer)
        {
          case Computers.AppleII:
            material.EnableKeyword(Keywords.APPLE_II);
            break;
          case Computers.CGA:
            material.EnableKeyword(Keywords.CGA);
            material.SetInt(ShaderIDs.Mode, (int)settings.cgaMode);
            break;
          case Computers.BBCMicro:
            material.EnableKeyword(Keywords.BBC_MICRO);
            break;
          case Computers.Commodore64:
            material.EnableKeyword(Keywords.COMMODORE_64);
            material.SetInt(ShaderIDs.Mode, (int)settings.commodore64Mode);
            break;
          case Computers.EGA:
            material.EnableKeyword(Keywords.EGA);
            material.SetInt(ShaderIDs.Mode, (int)settings.egaMode);
            break;
          case Computers.MSX:
            material.EnableKeyword(Keywords.MSX);
            material.SetInt(ShaderIDs.Mode, (int)settings.msxMode);
            break;
          case Computers.MattelAquarius:
            material.EnableKeyword(Keywords.MATTEL_AQUARIUS);
            break;
          case Computers.AmstradCPC:
            material.EnableKeyword(Keywords.AMSTRAD_CPC);
            break;
          case Computers.AtariST:
            material.EnableKeyword(Keywords.ATARI_ST);
            break;
          case Computers.MacII:
            material.EnableKeyword(Keywords.MAC_II);
            break;
          case Computers.GameBoy:
            material.EnableKeyword(Keywords.GAME_BOY);
            material.SetInt(ShaderIDs.Mode, (int)settings.gameboyMode);
            break;
          case Computers.Custom:
            material.SetColorArray(ShaderIDs.CustomPalette, settings.customPalette);
            material.SetInt(ShaderIDs.CustomPaletteColors, settings.customColorsCount);
            break;
        }

        material.SetInt(ShaderIDs.Pixelation, settings.pixelation * 2);
        material.SetFloat(ShaderIDs.Dithering, settings.dithering * 0.1f);
        material.SetFloat(ShaderIDs.RangeMin, Mathf.Min(settings.rangeMin, settings.rangeMax));
        material.SetFloat(ShaderIDs.RangeMax, Mathf.Max(settings.rangeMax, settings.rangeMin));

        material.SetFloat(ShaderIDs.Brightness, settings.brightness);
        material.SetFloat(ShaderIDs.Contrast, settings.contrast);
        material.SetFloat(ShaderIDs.Gamma, 1.0f / settings.gamma);
        material.SetFloat(ShaderIDs.Hue, settings.hue);
        material.SetFloat(ShaderIDs.Saturation, settings.saturation);
      }

#if UNITY_6000_0_OR_NEWER
      /// <inheritdoc/>
      public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
      {
        if (material == null || settings.intensity == 0.0f)
          return;

        UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
        if (resourceData.isActiveTargetBackBuffer == true)
          return;

        UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
        if (cameraData.camera.cameraType == CameraType.SceneView && settings.affectSceneView == false)
          return;

        TextureHandle source = resourceData.activeColorTexture;
        TextureHandle destination = renderGraph.CreateTexture(source.GetDescriptor(renderGraph));

        UpdateMaterial();

        RenderGraphUtils.BlitMaterialParameters pass = new(source, destination, material, 0);
        renderGraph.AddBlitPass(pass, $"{Constants.Asset.AssemblyName}.Pass");

        resourceData.cameraColor = destination;
      }
#elif UNITY_2022_3_OR_NEWER
      /// <inheritdoc/>
      public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
      {
        renderTextureDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        renderTextureDescriptor.depthBufferBits = 0;

        colorBuffer = renderingData.cameraData.renderer.cameraColorTargetHandle;
        cmd.GetTemporaryRT(renderTextureHandle0, renderTextureDescriptor, settings.filterMode);
      }

      /// <inheritdoc/>
      public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
      {
        if (material == null ||
            renderingData.postProcessingEnabled == false ||
            settings.intensity <= 0.0f ||
            settings.affectSceneView == false && renderingData.cameraData.isSceneViewCamera == true)
          return;

        CommandBuffer cmd = CommandBufferPool.Get(CommandBufferName);

        if (settings.enableProfiling == true)
          profilingScope = new ProfilingScope(cmd, profilingSamples);

        UpdateMaterial();

        cmd.Blit(colorBuffer, renderTextureHandle0, material);
        cmd.Blit(renderTextureHandle0, colorBuffer);

        cmd.ReleaseTemporaryRT(renderTextureHandle0);

        if (settings.enableProfiling == true)
          profilingScope.Dispose();

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
      }

      public override void OnCameraCleanup(CommandBuffer cmd) => cmd.ReleaseTemporaryRT(renderTextureHandle0);
#else
      #error Unsupported Unity version. Please update to a newer version of Unity.
#endif
    }
  }
}
