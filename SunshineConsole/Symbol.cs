using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;

namespace SunshineConsole
{
    public class Symbol
    {
        public static readonly Symbol DefaultBaseSymbol = new Symbol(' ', Color4.White, Color4.Black);

        public Symbol(char character) : this(character, Color4.White)
        {
            
        }
        public Symbol(char character, Color4 fontColor ) : this(character,fontColor, Color4.Black)
        {
            
        }
        public Symbol(char character, Color4 fontColor, Color4 backgroundColor)
        {
            Character = character;
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        public char Character { get; set; }
        public Color4 FontColor { get; set; }
        public Color4 BackgroundColor { get; set; }

        public Symbol GetCopy()
        {
            return  new Symbol(Character,FontColor,BackgroundColor);
        }
        
    }
    public  static class StringExtension
    {
        public static Symbol[] ToSymbolArray(this string str)
        {
            Symbol[] placeholder = new Symbol[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                //Should be changed, high coupling checks if char is containted in the extended ascii table 
                if (str[i] < 256)
                {
                    placeholder[i] = new Symbol(str[i]);
                }
            }

            return placeholder;
        }
    }
}
