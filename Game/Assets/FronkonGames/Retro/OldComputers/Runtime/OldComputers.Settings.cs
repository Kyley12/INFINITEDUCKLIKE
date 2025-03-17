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
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FronkonGames.Retro.OldComputers
{
  ///------------------------------------------------------------------------------------------------------------------
  /// <summary> Settings. </summary>
  /// <remarks> Only available for Universal Render Pipeline. </remarks>
  ///------------------------------------------------------------------------------------------------------------------
  public sealed partial class OldComputers
  {
    /// <summary>
    /// Settings.
    /// </summary>
    [Serializable]
    public sealed class Settings
    {
      public Settings() => ResetDefaultValues();

#region Common settings.
      /// <summary> Controls the intensity of the effect [0, 1]. Default 1. </summary>
      /// <remarks> An effect with Intensity equal to 0 will not be executed. </remarks>
      public float intensity = 1.0f;
#endregion

#region Old Computers settings.
      /// <summary> Current computer to emulate. Default Amstrad CPC. </summary>
      public Computers computer = Computers.AmstradCPC;

      /// <summary> CGA modes. Default NTSC. </summary>
      public CGAModes cgaMode = CGAModes.NTSC;

      /// <summary> Commodore 64 modes. </summary>
      public Commodore64Modes commodore64Mode = Commodore64Modes.Default;

      /// <summary> MSX modes. </summary>
      public MSXModes msxMode = MSXModes.Default;
      
      /// <summary> EGA modes. Default Mode4Palette1Low. </summary>
      public EGAModes egaMode = EGAModes.Mode4Palette1Low;

      /// <summary> Game Boy modes. Default One. </summary>
      public GameBoyModes gameboyMode = GameBoyModes.One;
      
      /// <summary> Pixelation strength [0, 10]. Default 4. </summary>
      public int pixelation = 4;

      /// <summary> Dithering strength [0, 1]. Default 0.1. </summary>
      public float dithering = 0.1f;

      /// <summary> Minimum value of the range for color remapping [0, 1]. Default 0. </summary>
      /// <remarks> Must always be less than rangeMax. </remarks>
      public float rangeMin = 0.0f;

      /// <summary> Maximum value of the range for color remapping [0, 1]. Default 1. </summary>
      /// <remarks> Must always be greater than rangeMin </remarks>
      public float rangeMax = 1.0f;
      
      /// <summary> Number of colors of the custom palette [2, 16]. Default 16. </summary>
      public int customColorsCount = 16;
      
      /// <summary> Custom palette. </summary>
      public Color[] customPalette = new Color[16];
#endregion

#region Color settings.
      /// <summary> Brightness [-1.0, 1.0]. Default 0. </summary>
      public float brightness = 0.0f;

      /// <summary> Contrast [0.0, 10.0]. Default 1. </summary>
      public float contrast = 1.0f;

      /// <summary>Gamma [0.1, 10.0]. Default 1. </summary>      
      public float gamma = 1.0f;

      /// <summary> The color wheel [0.0, 1.0]. Default 0. </summary>
      public float hue = 0.0f;

      /// <summary> Intensity of a colors [0.0, 2.0]. Default 1. </summary>      
      public float saturation = 1.0f;
      #endregion

      #region Advanced settings.
      /// <summary> Does it affect the Scene View? </summary>
      public bool affectSceneView = false;

#if !UNITY_6000_0_OR_NEWER
      /// <summary> Enable render pass profiling. </summary>
      public bool enableProfiling = false;

      /// <summary> Filter mode. Default Bilinear. </summary>
      public FilterMode filterMode = FilterMode.Bilinear;
#endif

      /// <summary> Render pass injection. Default BeforeRenderingPostProcessing. </summary>
      public RenderPassEvent whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
      #endregion

      public static readonly Color[] DefaultCustomPalette = new Color[16]
      {
        new(0.0f, 0.0f, 0.0f),
        new(1.0f, 1.0f, 1.0f),
        new(136.0f / 255.0f, 0.0f, 0.0f),
        new(70.0f / 255.0f, 255.0f / 255.0f, 238.0f / 255.0f),
        new(204.0f / 255.0f, 68.0f / 255.0f, 204.0f / 255.0f),
        new(0.0f / 255.0f, 204.0f / 255.0f, 85.0f / 255.0f),
        new(0.0f / 255.0f, 0.0f / 255.0f, 170.0f / 255.0f),
        new(238.0f / 255.0f, 238.0f / 255.0f, 119.0f / 255.0f),
        new(221.0f / 255.0f, 136.0f / 255.0f, 85.0f / 255.0f),
        new(102.0f / 255.0f, 68.0f / 255.0f, 0.0f / 255.0f),
        new(255.0f / 255.0f, 119.0f / 255.0f, 119.0f / 255.0f),
        new(51.0f / 255.0f,  51.0f / 255.0f,  51.0f / 255.0f),
        new(119.0f / 255.0f, 119.0f / 255.0f, 119.0f / 255.0f),
        new(170.0f / 255.0f, 255.0f / 255.0f, 102.0f / 255.0f),
        new(0.0f / 255.0f, 136.0f / 255.0f, 255.0f / 255.0f),
        new(187.0f / 255.0f, 187.0f / 255.0f, 187.0f / 255.0f)
      };

      /// <summary> Reset to default values. </summary>
      public void ResetDefaultValues()
      {
        intensity = 1.0f;

        pixelation = 4;
        dithering = 0.1f;
        rangeMin = 0.0f;
        rangeMax = 1.0f;
        DefaultCustomPalette.CopyTo(customPalette, 0);
        
        brightness = 0.0f;
        contrast = 1.0f;
        gamma = 1.0f;
        hue = 0.0f;
        saturation = 1.0f;

        affectSceneView = false;
#if !UNITY_6000_0_OR_NEWER
        enableProfiling = false;
        filterMode = FilterMode.Bilinear;
#endif
        whenToInsert = RenderPassEvent.BeforeRenderingPostProcessing;
      }
    }    
  }
}
