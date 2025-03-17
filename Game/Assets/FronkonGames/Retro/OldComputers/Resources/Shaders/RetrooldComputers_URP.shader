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
Shader "Hidden/Fronkon Games/Retro/Old Computers URP"
{
  Properties
  {
    _MainTex("Main Texture", 2D) = "white" {}
  }

  SubShader
  {
    Tags
    {
      "RenderType" = "Opaque"
      "RenderPipeline" = "UniversalPipeline"
    }
    LOD 100
    ZTest Always ZWrite Off Cull Off

    Pass
    {
      Name "Fronkon Games Retro Old Computers"

      HLSLPROGRAM
      #include "Retro.hlsl"

      #pragma vertex RetroVert
      #pragma fragment frag
      #pragma fragmentoption ARB_precision_hint_fastest
      #pragma exclude_renderers d3d9 d3d11_9x ps3 flash
      #pragma multi_compile _ _USE_DRAW_PROCEDURAL
      #pragma multi_compile ___ APPLE_II CGA EGA MSX MATTEL_AQUARIUS AMSTRAD_CPC ATARI_ST COMMODORE_64 BBC_MICRO MAC_II GAME_BOY

      int _PixelSize;
      float _Dithering;
      float _RangeMin;
      float _RangeMax;
      int _Mode;
      float3 _CustomPalette[16];
      int _CustomPaletteColors;

      static const float Bayer2x2[] = { -0.5, 0.16666666, 0.5, -0.16666666 };

      float4 frag(const VertexOutput input) : SV_Target 
      {
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
        const float2 uv = UnityStereoTransformScreenSpaceTex(input.texcoord.xy);

        const float4 color = SAMPLE_MAIN(uv);
        float4 pixel = color;

        const float2 newRes = float2(_ScreenParams.x / (1 + _PixelSize), _ScreenParams.y);
        const float2 uv2 = float2((floor(uv.x * newRes.x) + 0.25) / newRes.x, (floor(uv.y * newRes.y) + 0.5) / newRes.y);
        pixel.rgb = SAMPLE_MAIN(uv2).rgb;

        int colorCount = 16;
        float3 palette[16];
#if APPLE_II
        palette[0]  = float3(0, 0, 0) / 255.0;
        palette[1]  = float3(137, 61, 81) / 255.0;
        palette[2]  = float3(78, 74, 134) / 255.0;
        palette[3]  = float3(239, 96, 235) / 255.0;
        palette[4]  = float3(0, 104, 84) / 255.0;
        palette[5]  = float3(145, 145, 145) / 255.0;
        palette[6]  = float3(0, 167, 237) / 255.0;
        palette[7]  = float3(199, 194, 246) / 255.0;
        palette[8]  = float3(82, 92, 31) / 255.0;
        palette[9]  = float3(244, 125, 51) / 255.0;
        palette[10] = float3(145, 145, 145) / 255.0;
        palette[11] = float3(251, 184, 200) / 255.0;
        palette[12] = float3(0, 199, 63) / 255.0;
        palette[13] = float3(203, 209, 157) / 255.0;
        palette[14] = float3(144, 219, 202) / 255.0;
        palette[15] = float3(255, 255, 255) / 255.0;          
#elif CGA
        if (_Mode == 0)
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(0, 14, 163) / 255.0;
          palette[2]  = float3(0, 119, 23) / 255.0;
          palette[3]  = float3(0, 156, 118) / 255.0;
          palette[4]  = float3(111, 7, 31) / 255.0;
          palette[5]  = float3(130, 34, 168) / 255.0;
          palette[6]  = float3(117, 143, 26) / 255.0;
          palette[7]  = float3(162, 162, 162) / 255.0;
          palette[8]  = float3(73, 73, 73) / 255.0;
          palette[9]  = float3(109, 92, 253) / 255.0;
          palette[10] = float3(94, 210, 75) / 255.0;
          palette[11] = float3(105, 250, 209) / 255.0;
          palette[12] = float3(204, 80, 116) / 255.0;
          palette[13] = float3(224, 117, 254) / 255.0;
          palette[14] = float3(210, 237, 79) / 255.0;
          palette[15] = float3(255, 255, 255) / 255.0;          
        }
        else
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(0, 0, 170) / 255.0;
          palette[2]  = float3(0, 170, 0) / 255.0;
          palette[3]  = float3(0, 170, 170) / 255.0;
          palette[4]  = float3(170, 0, 0) / 255.0;
          palette[5]  = float3(170, 0, 170) / 255.0;
          palette[6]  = float3(170, 170, 0) / 255.0;
          palette[7]  = float3(170, 170, 170) / 255.0;
          palette[8]  = float3(85, 85, 85) / 255.0;
          palette[9]  = float3(85, 85, 255) / 255.0;
          palette[10] = float3(85, 255, 85) / 255.0;
          palette[11] = float3(85, 255, 255) / 255.0;
          palette[12] = float3(255, 85, 85) / 255.0;
          palette[13] = float3(255, 85, 255) / 255.0;
          palette[14] = float3(255, 255, 85) / 255.0;
          palette[15] = float3(55, 255, 255) / 255.0;
        }
#elif BBC_MICRO
        palette[0]  = float3(0, 0, 0) / 255.0;
        palette[1]  = float3(255, 15, 22) / 255.0;
        palette[2]  = float3(0, 254, 62) / 255.0;
        palette[3]  = float3(0, 30, 250) / 255.0;
        palette[4]  = float3(0, 255, 254) / 255.0;
        palette[5]  = float3(255, 27, 249) / 255.0;
        palette[6]  = float3(255, 254, 64) / 255.0;
        palette[7]  = float3(255, 255, 255) / 255.0;
        colorCount = 8;
#elif COMMODORE_64
        if (_Mode == 0)
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(255, 255, 255) / 255.0;
          palette[2]  = float3(166, 77, 69) / 255.0;
          palette[3]  = float3(88, 192, 199) / 255.0;
          palette[4]  = float3(164, 88, 161) / 255.0;
          palette[5]  = float3(82, 171, 100) / 255.0;
          palette[6]  = float3(77, 71, 152) / 255.0;
          palette[7]  = float3(203, 212, 141) / 255.0;
          palette[8]  = float3(168, 104, 64) / 255.0;
          palette[9]  = float3(113, 84, 28) / 255.0;
          palette[10] = float3(210, 125, 119) / 255.0;
          palette[11] = float3(99, 99, 99) / 255.0;
          palette[12] = float3(138, 138, 138) / 255.0;
          palette[13] = float3(144, 226, 157) / 255.0;
          palette[14] = float3(134, 126, 202) / 255.0;
          palette[15] = float3(174, 173, 174) / 255.0;
        }
        else
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(255, 255, 255) / 255.0;
          palette[2]  = float3(255, 52, 24) / 255.0;
          palette[3]  = float3(0, 222, 253) / 255.0;
          palette[4]  = float3(170, 78, 199) / 255.0;
          palette[5]  = float3(95, 185, 89) / 255.0;
          palette[6]  = float3(0, 77, 204) / 255.0;
          palette[7]  = float3(255, 236, 84) / 255.0;
          palette[8]  = float3(255, 90, 30) / 255.0;
          palette[9]  = float3(201, 65, 22) / 255.0;
          palette[10] = float3(255, 113, 77) / 255.0;
          palette[11] = float3(96, 96, 96) / 255.0;
          palette[12] = float3(135, 152, 109) / 255.0;
          palette[13] = float3(170, 255, 157) / 255.0;
          palette[14] = float3(35, 136, 250) / 255.0;
          palette[15] = float3(195, 184, 213) / 255.0;
        }
#elif EGA
        if (_Mode == 0)
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(170, 170, 170) / 255.0;
          palette[2]  = float3(170, 0, 170) / 255.0;
          palette[3]  = float3(0, 170, 170) / 255.0;
        }
        else if (_Mode == 1)
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(170, 170, 170) / 255.0;
          palette[2]  = float3(255, 85, 255) / 255.0;
          palette[3]  = float3(85, 255, 255) / 255.0;
        }
        else if (_Mode == 2)
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(0, 170, 0) / 255.0;
          palette[2]  = float3(170, 0, 0) / 255.0;
          palette[3]  = float3(170, 170, 0) / 255.0;
        }
        else if (_Mode == 3)
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(85, 255, 85) / 255.0;
          palette[2]  = float3(255, 85, 85) / 255.0;
          palette[3]  = float3(255, 255, 85) / 255.0;
        }
        else if (_Mode == 4)
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(0, 170, 170) / 255.0;
          palette[2]  = float3(170, 0, 0) / 255.0;
          palette[3]  = float3(255, 255, 255) / 255.0;
        }
        else
        {
          palette[0] = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(85, 255, 255) / 255.0;
          palette[2]  = float3(255, 85, 85) / 255.0;
          palette[3]  = float3(2255, 255, 255) / 255.0;
        }
        colorCount = 4;
#elif MSX
        if (_Mode == 0)
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(0, 0, 0) / 255.0;
          palette[2]  = float3(35, 182, 83) / 255.0;
          palette[3]  = float3(103, 206, 129) / 255.0;
          palette[4]  = float3(79, 88, 218) / 255.0;
          palette[5]  = float3(121, 119, 236) / 255.0;
          palette[6]  = float3(191, 94, 83) / 255.0;
          palette[7]  = float3(68, 218, 237) / 255.0;
          palette[8]  = float3(227, 100, 91) / 255.0;
          palette[9]  = float3(255, 135, 125) / 255.0;
          palette[10] = float3(206, 193, 102) / 255.0;
          palette[11] = float3(224, 207, 138) / 255.0;
          palette[12] = float3(41, 160, 74) / 255.0;
          palette[13] = float3(186, 102, 177) / 255.0;
          palette[14] = float3(203, 203, 203) / 255.0;
          palette[15] = float3(255, 255, 255) / 255.0;
        }
        else
        {
          palette[0]  = float3(0, 0, 0) / 255.0;
          palette[1]  = float3(255, 38, 23) / 255.0;
          palette[2]  = float3(110, 108, 75) / 255.0;
          palette[3]  = float3(255, 216, 149) / 255.0;
          colorCount = 4;
        }
#elif MATTEL_AQUARIUS
        palette[0] = float3(0,0,0);
        palette[1] = float3(1,1,1);
        palette[2] = float3(0.494117647058824,0.0980392156862745,0.164705882352941);
        palette[3] = float3(0.764705882352941,0,0.105882352941176);
        palette[4] = float3(0.725490196078431,0.694117647058824,0.337254901960784);
        palette[5] = float3(0.784313725490196,0.725490196078431,0.0274509803921569);
        palette[6] = float3(0.231372549019608,0.592156862745098,0.180392156862745);
        palette[7] = float3(0.0274509803921569,0.749019607843137,0);
        palette[8] = float3(0.250980392156863,0.650980392156863,0.584313725490196);
        palette[9] = float3(0,0.776470588235294,0.643137254901961);
        palette[10] = float3(0.749019607843137,0.749019607843137,0.749019607843137);
        palette[11] = float3(0.513725490196078,0.152941176470588,0.564705882352941);
        palette[12] = float3(0.717647058823529,0,0.819607843137255);
        palette[13] = float3(0.0196078431372549,0.0509803921568627,0.407843137254902);
        colorCount = 14;
#elif AMSTRAD_CPC
        palette[0] = float3(0,0,0);
        palette[1] = float3(1,1,1);
        palette[2] = float3(0,0,0.498039215686275);
        palette[3] = float3(0.498039215686275,0,0);
        palette[4] = float3(0.498039215686275,0,0.498039215686275);
        palette[5] = float3(0,0.498039215686275,0);
        palette[6] = float3(1,0,0);
        palette[7] = float3(0,0.498039215686275,0.498039215686275);
        palette[8] = float3(0.498039215686275,0.498039215686275,0);
        palette[9] = float3(0.498039215686275,0.498039215686275,0.498039215686275);
        palette[10] = float3(0.498039215686275,0.498039215686275,1);
        palette[11] = float3(1,0.498039215686275,0);
        palette[12] = float3(1,0.498039215686275,0.498039215686275);
        palette[13] = float3(0.498039215686275,1,0.498039215686275);
        palette[14] = float3(0.498039215686275,1,1);
        palette[15] = float3(1,1,0.498039215686275);
#elif ATARI_ST
        palette[0] = float3(0,0,0);
        palette[1] = float3(1,0.886274509803922,0.882352941176471);
        palette[2] = float3(0.376470588235294,0.0392156862745098,0.0117647058823529);
        palette[3] = float3(0.811764705882353,0.133333333333333,0.0549019607843137);
        palette[4] = float3(0.16078431372549,0.345098039215686,0.0352941176470588);
        palette[5] = float3(0.937254901960784,0.16078431372549,0.0705882352941176);
        palette[6] = float3(0.356862745098039,0.349019607843137,0.0431372549019608);
        palette[7] = float3(0.352941176470588,0.352941176470588,0.352941176470588);
        palette[8] = float3(0.803921568627451,0.372549019607843,0.207843137254902);
        palette[9] = float3(0.494117647058824,0.509803921568627,0.756862745098039);
        palette[10] = float3(0.305882352941176,0.623529411764706,0.0980392156862745);
        palette[11] = float3(0.792156862745098,0.509803921568627,0.364705882352941);
        palette[12] = float3(1,0.392156862745098,0.215686274509804);
        palette[13] = float3(1,0.525490196078431,0.368627450980392);
        palette[14] = float3(0.631372549019608,0.63921568627451,0.76078431372549);
        palette[15] = float3(1,0.768627450980392,0.517647058823529);
#elif MAC_II
        palette[0]  = float3(255, 255, 255) / 255.0;
        palette[1]  = float3(255, 255, 0) / 255.0;
        palette[2]  = float3(255, 102, 0) / 255.0;
        palette[3]  = float3(221, 0, 0) / 255.0;
        palette[4]  = float3(255, 0, 153) / 255.0;
        palette[5]  = float3(51, 0, 153) / 255.0;
        palette[6]  = float3(0, 0, 204) / 255.0;
        palette[7]  = float3(0, 153, 255) / 255.0;
        palette[8]  = float3(0, 170, 0) / 255.0;
        palette[9]  = float3(0, 102, 0) / 255.0;
        palette[10] = float3(102, 51, 0) / 255.0;
        palette[11] = float3(153, 102, 51) / 255.0;
        palette[12] = float3(187, 187, 187) / 255.0;
        palette[13] = float3(136, 136, 136) / 255.0;
        palette[14] = float3(68, 68, 68) / 255.0;
        palette[15] = float3(0, 0, 0) / 255.0;
#elif GAME_BOY
        if (_Mode == 0)
        {
          palette[0]  = float3(155, 188, 15) / 255.0;
          palette[1]  = float3(139, 172, 15) / 255.0;
          palette[2]  = float3(48, 98, 48) / 255.0;
          palette[3]  = float3(15, 56, 15) / 255.0;
        }
        else
        {
          palette[0]  = float3(50, 59, 41) / 255.0;
          palette[1]  = float3(109, 127, 87) / 255.0;
          palette[2]  = float3(98, 114, 79) / 255.0;
          palette[3]  = float3(156, 173, 136) / 255.0;
        }
          
        colorCount = 4;
#else
        palette[0]  = _CustomPalette[0];
        palette[1]  = _CustomPalette[1];
        palette[2]  = _CustomPalette[2];
        palette[3]  = _CustomPalette[3];
        palette[4]  = _CustomPalette[4];
        palette[5]  = _CustomPalette[5];
        palette[6]  = _CustomPalette[6];
        palette[7]  = _CustomPalette[7];
        palette[8]  = _CustomPalette[8];
        palette[9]  = _CustomPalette[9];
        palette[10] = _CustomPalette[10];
        palette[11] = _CustomPalette[11];
        palette[12] = _CustomPalette[12];
        palette[13] = _CustomPalette[13];
        palette[14] = _CustomPalette[14];
        palette[15] = _CustomPalette[15];
        colorCount = _CustomPaletteColors;
#endif

        // Dither.
        const float grid_position = frac(dot(uv, _ScreenParams.xy * 0.5) + 0.25);
        float dither_shift = 0.25 * (1.0 / (pow(2, 2.0) - 1.0));
        pixel.rgb += lerp(2.0 * dither_shift.xxx, -2.0 * dither_shift.xxx, grid_position);

        // Bayer 2x2.
        const uint2 pss = (uint2)(uv2 * _ScreenSize.xy);
        const float dither = Bayer2x2[(pss.y & 1) * 2 + (pss.x & 1)];
        pixel.rgb += dither * _Dithering;

        // Color matching.
        float3 remap = pixel.rgb;
        const float range = _RangeMax - _RangeMin;
        remap = remap * range + _RangeMin.xxx;

        float3 diff = remap - palette[0];
        float dist = dot(diff, diff);
        float closest_dist = dist;
        float3 closest_color = palette[0];

        for (int i = 1 ; i < colorCount; ++i)
        {
          diff = remap - palette[i];
          dist = dot(diff, diff);

          UNITY_BRANCH
          if (dist < closest_dist)
          {
            closest_dist = dist;
            closest_color = palette[i];
          }
        }

        pixel.rgb = closest_color;

        // Color adjust.
        pixel.rgb = ColorAdjust(pixel.rgb, _Contrast, _Brightness, _Hue, _Gamma, _Saturation);
        
        return lerp(color, pixel, _Intensity);
      }

      ENDHLSL
    }
  }
  
  FallBack "Diffuse"
}
