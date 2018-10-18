using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace SunshineConsole.Sections
{
    class BorderedSection :  Section
    {
        [Flags]
        public enum BorderSides
        {
           None=0, Top = 2,Bottom = 4,Rigth =8,Left = 16, All = 31
        }

        public BorderSides Sides { get; set; }
        public Symbol BorderSymbol { get; set; }
        public BorderedSection(Section section, Symbol borderSymbol, BorderSides sides = BorderSides.All) :
            this(section.PinY, section.PinX, section.Height , section.Width, section.Layer, borderSymbol, sides)
        {
            
        }
        public BorderedSection(int pinY, int pinX, int height, int width, int layer , Symbol borderSymbol, BorderSides sides = BorderSides.All) : base(pinY, pinX, height, width, layer)
        {
            BorderSymbol = borderSymbol;
            Sides = sides;
            WriteBorder(borderSymbol, sides);
        }

        public override void Update(Symbol[][] newMatrix)
        {

            base.Update(newMatrix);
            WriteBorder(BorderSymbol,Sides);
        }

        
        public override void Clear()
        {
            for (int row = 1; row <= this.Height - 2; row++)
            {
                this.ClearRow(row);
            }
        }

        public override void CheckWriteConstraint(int cordY, int cordX)
        {
            if (cordY <= 0 || cordY >= Width - 2)
            {
                throw new ArgumentException("Y coordinate has passed the constraint.");
            }
            if (cordX <= 0 || cordX >= Width - 2)
            {
                throw new ArgumentException("X coordinate has passed the constraint.");
            }
        }

        
        public void WriteBorder(Symbol borderSymbol,BorderSides sides)
        {
            if ((sides & BorderSides.Rigth) == BorderSides.Rigth)
            {
                WriteRightBorder(borderSymbol);
            }
            if ((sides & BorderSides.Left) == BorderSides.Left)
            {
                WriteLeftBorder(borderSymbol);
            }

            if ((sides & BorderSides.Top) == BorderSides.Top)
            {
                WriteTopBorder(borderSymbol);
            }
            if ((sides & BorderSides.Bottom) == BorderSides.Bottom)
            {
                WriteBottomBorder(borderSymbol);
            }
        }

        private void WriteRightBorder(Symbol borderSymbol)
        {
            for (int y = 0; y < Height; y++)
            {
                SectionSymbols[y][Width - 1] = borderSymbol;
            }
        }
        private void WriteLeftBorder(Symbol borderSymbol)
        {
            for (int y = 0; y < Height; y++)
            {
                SectionSymbols[y][0] = borderSymbol;
            }
        }
        private void WriteTopBorder(Symbol borderSymbol)
        {
            for (int x = 0; x < Width; x++)
            {
                SectionSymbols[0][x] = borderSymbol;
            }
        }
        private void WriteBottomBorder(Symbol borderSymbol)
        {
            for (int x = 0; x < Width; x++)
            {
                SectionSymbols[Height-1][x] = borderSymbol;
            }
        }
    }
}
