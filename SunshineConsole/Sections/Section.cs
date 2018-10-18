using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SunshineConsole.Sections.Interfaces;

namespace SunshineConsole.Sections
{
    public class Section : ISection
    {

        //The pins represents coordinates need to move the section's geometric origin when used by the SectionManager
        private int pinX;
        private int pinY;

        private Symbol[][] sectionSymbols;

        public Section(int pinY, int pinX, int height, int width, int layer = 0)
        {
            Height = height;
           
            Width = width;
            
            Layer = layer;
            this.PinX = pinX;
            this.PinY = pinY;
            Corners = new Corners(this);
            SectionSymbols = InitializeSectionSymbols(Height, Width);

        }

        public ICorners Corners { get; private set; }
 
        public int Height { get; }
        public int Width { get; }
        public int Layer { get; }

        public Symbol[][] SectionSymbols
        {
            get { return sectionSymbols; }
            private set { sectionSymbols = value; }
        }

        public int PinX { get => pinX; set => pinX = value; }
        public int PinY { get => pinY; set => pinY = value; }

        private Symbol[][] InitializeSectionSymbols(int consoleHeight, int consoleWidth)
        {
            var matrix = new Symbol[consoleHeight][];
            for (int y = 0; y < consoleHeight; y++)
            {
                matrix[y] = new Symbol[consoleWidth];
                for (int x = 0; x < consoleWidth; x++)
                {
                    matrix[y][x] = Symbol.DefaultBaseSymbol.GetCopy();
                }
            }

            return matrix;
        }

        public virtual void SetTextLine(Symbol[] text, int atRow, int atCol)
        {
            if (text.Length >= this.SectionSymbols[0].Length)
                throw new ArgumentException("Text's length is longer than window's width");

            for (int col = 0; col < text.Length; col++)
            {
                this.SectionSymbols[atRow][atCol + col] = text[col];
            }
        }

        public virtual void SetSymbol(Symbol sym, int y, int x)
        {
            CheckWriteConstraint(y, x);
            SectionSymbols[y][x] = sym;
        }

        public virtual void Update(Symbol[][] newMatrix)
        {
            if (newMatrix.Length != SectionSymbols.Length || newMatrix[0].Length != SectionSymbols[0].Length)
            {
                throw new ArgumentException($"Can't update section because, does not match the window size.");
            }

            SectionSymbols = newMatrix;
        }
        public virtual void Clear()
        {
            for (int row = 0; row <= this.Height - 1; row++)
            {
                this.ClearRow(row);
            }
        }

        public virtual void ClearRow(int row)
        {
            this.SectionSymbols[row] =
                this.SectionSymbols[row].Select(symb =>
                    {
                        return Symbol.DefaultBaseSymbol.GetCopy();
                    })
                    .ToArray();
        }

        public virtual void CheckWriteConstraint(int cordY, int cordX)
        {
            if (cordY<0 ||cordY > Width-1)
            {
                throw new ArgumentException("Y coordinate has passed the constraint.");
            }
            if (cordX <0 || cordX > Width - 1)
            {
                throw new ArgumentException("X coordinate has passed the constraint.");
            }
        }
    }

}
