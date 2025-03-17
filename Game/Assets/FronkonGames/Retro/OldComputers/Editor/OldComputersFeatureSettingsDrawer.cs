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
using UnityEditor;
using static FronkonGames.Retro.OldComputers.Inspector;

namespace FronkonGames.Retro.OldComputers.Editor
{
  /// <summary> Retro Old Computers inspector. </summary>
  [CustomPropertyDrawer(typeof(OldComputers.Settings))]
  public class VintageFiltersFeatureSettingsDrawer : Drawer
  {
    private OldComputers.Settings settings;

    protected override void ResetValues() => settings?.ResetDefaultValues();

    protected override void InspectorGUI()
    {
      settings ??= GetSettings<OldComputers.Settings>();

      /////////////////////////////////////////////////
      // Common.
      /////////////////////////////////////////////////
      settings.intensity = Slider("Intensity", "Controls the intensity of the effect [0, 1]. Default 1.", settings.intensity, 0.0f, 1.0f, 1.0f);

      /////////////////////////////////////////////////
      // Old Computers.
      /////////////////////////////////////////////////
      Separator();

      settings.computer = (Computers)EnumPopup("Computer", "Current computer to emulate. Default Amstrad CPC.", settings.computer, Computers.AmstradCPC);
      IndentLevel++;
      switch (settings.computer)
      {
        case Computers.CGA:
          settings.cgaMode = (CGAModes)EnumPopup("Modes", "CGA modes. Default NTSC", settings.cgaMode, CGAModes.NTSC);
          break;
        case Computers.Commodore64:
          settings.commodore64Mode = (Commodore64Modes)EnumPopup("Modes", "Commodore 64 modes.", settings.commodore64Mode, Commodore64Modes.Default);
          break;
        case Computers.MSX:
          settings.msxMode = (MSXModes)EnumPopup("Modes", "MSX modes.", settings.msxMode, MSXModes.Default);
          break;
        case Computers.EGA:
          settings.egaMode = (EGAModes)EnumPopup("Modes", "EGA modes. Default EGA_Mode4_Palette_1_Low", settings.egaMode, EGAModes.Mode4Palette1Low);
          break;
        case Computers.GameBoy:
          settings.gameboyMode = (GameBoyModes)EnumPopup("Modes", "Game Boy modes. Default One", settings.gameboyMode, GameBoyModes.One);
          break;
        case Computers.Custom:
          settings.customColorsCount = Slider("Colors", "Number of colors of the custom palette [2, 16]. Default 16.", settings.customColorsCount, 2, 16, 16);

          for (int i = 0; i < settings.customColorsCount; ++i)
            settings.customPalette[i] = ColorField($"Color #{i + 1}", string.Empty, settings.customPalette[i], OldComputers.Settings.DefaultCustomPalette[i]);
          break;
      }
      IndentLevel--;

      settings.pixelation = Slider("Pixelation", "Pixelation strength [0, 10]. Default 4.", settings.pixelation, 0, 10, 4);
      settings.dithering = Slider("Dithering", "Dithering strength [0, 1]. Default 0.1.", settings.dithering, 0.0f, 1.0f, 0.1f);

      float min = settings.rangeMin;
      float max = settings.rangeMax;
      MinMaxSlider("Remap colors", "", ref min, ref max, 0.0f, 1.0f, 0.0f, 1.0f);
      settings.rangeMin = min;
      settings.rangeMax = max;

      /////////////////////////////////////////////////
      // Color.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Color") == true)
      {
        IndentLevel++;

        settings.brightness = Slider("Brightness", "Brightness [-1.0, 1.0]. Default 0.", settings.brightness, -1.0f, 1.0f, 0.0f);
        settings.contrast = Slider("Contrast", "Contrast [0.0, 10.0]. Default 1.", settings.contrast, 0.0f, 10.0f, 1.0f);
        settings.gamma = Slider("Gamma", "Gamma [0.1, 10.0]. Default 1.", settings.gamma, 0.01f, 10.0f, 1.0f);
        settings.hue = Slider("Hue", "The color wheel [0.0, 1.0]. Default 0.", settings.hue, 0.0f, 1.0f, 0.0f);
        settings.saturation = Slider("Saturation", "Intensity of a colors [0.0, 2.0]. Default 1.", settings.saturation, 0.0f, 2.0f, 1.0f);

        IndentLevel--;
      }

      /////////////////////////////////////////////////
      // Advanced.
      /////////////////////////////////////////////////
      Separator();

      if (Foldout("Advanced") == true)
      {
        IndentLevel++;

#if !UNITY_6000_0_OR_NEWER
        settings.filterMode = (FilterMode)EnumPopup("Filter mode", "Filter mode. Default Bilinear.", settings.filterMode, FilterMode.Bilinear);
#endif
        settings.affectSceneView = Toggle("Affect the Scene View?", "Does it affect the Scene View?", settings.affectSceneView);
        settings.whenToInsert = (UnityEngine.Rendering.Universal.RenderPassEvent)EnumPopup("RenderPass event",
          "Render pass injection. Default BeforeRenderingPostProcessing.",
          settings.whenToInsert,
          UnityEngine.Rendering.Universal.RenderPassEvent.BeforeRenderingPostProcessing);
#if !UNITY_6000_0_OR_NEWER
        settings.enableProfiling = Toggle("Enable profiling", "Enable render pass profiling", settings.enableProfiling);
#endif

        IndentLevel--;
      }
    }
  }
}
