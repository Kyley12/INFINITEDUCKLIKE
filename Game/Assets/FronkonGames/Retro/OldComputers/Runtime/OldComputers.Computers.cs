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

namespace FronkonGames.Retro.OldComputers
{
  /// <summary> Old computers. </summary>
  public enum Computers
  {
    /// <summary> Apple II (1977). </summary>
    AppleII,
    
    /// <summary> Color Graphics Adapter (CGA) graphics card (1981). </summary>
    CGA,

    /// <summary> BBC Micro (or 'Beeb') (1981). </summary>
    BBCMicro,
    
    /// <summary> Commodore 64 (1982). </summary>
    Commodore64,

    /// <summary> MSX (1983). </summary>
    MSX,

    /// <summary> Mattel Aquarius (1983). </summary>
    MattelAquarius, 
    
    /// <summary> Amstrad CPC (1984). </summary>
    AmstradCPC,
    
    /// <summary> Atari ST (1985). </summary>
    AtariST,
    
    /// <summary> Enhanced Graphics Adapter (EGA) graphics card (1987). </summary>
    EGA,
    
    /// <summary> Macintosh II (1987). </summary>
    MacII,
    
    /// <summary> Game Boy (1989). </summary>
    GameBoy,

    /// <summary> Custom palette. </summary>
    Custom 
  }

  /// <summary> CGA modes. </summary>
  public enum CGAModes
  {
    /// <summary> NTSC colors. </summary>
    NTSC,
    
    /// <summary> Microsoft Windows default 20 color palette. </summary>
    Microsoft256,
  }

  /// <summary> Commodore 64 modes. </summary>
  public enum Commodore64Modes
  {
    /// <summary> Default colors. </summary>
    Default,
    
    /// <summary> NTSC colors. </summary>
    NTSC,
  }

  /// <summary> MSX modes. </summary>
  public enum MSXModes
  {
    /// <summary> Default colors. </summary>
    Default,
    
    /// <summary> MSX Mode 6. </summary>
    Mode6,
  }
  
  /// <summary> EGA modes. </summary>
  public enum EGAModes
  {
    Mode4Palette1Low,
    Mode4Palette1High,    
    Mode4Palette2Low,    
    Mode4Palette2High,    
    Mode5Palette3Low,    
    Mode5Palette3High,    
  }

  /// <summary> Game Boy modes. </summary>
  public enum GameBoyModes
  {
    /// <summary> Default colors. </summary>
    One,
    
    /// <summary> Alternative colors. </summary>
    Two,
  }
}
