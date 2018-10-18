using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK.Graphics;
using SunshineConsole.Sections;

namespace SunshineConsole
{
    public class SectionManager
    {
        private Dictionary<string,Section> Sections = new Dictionary<string,Section>();
        private ConsoleWindow ConsoleToManage;
        public SectionManager(ConsoleWindow targetConsole)
        {
            ConsoleToManage = targetConsole;
            this.ConsoleWidth = targetConsole.Width;
            this.ConsoleHeight = targetConsole.Height;
            BaseLayer= InitializeBaseLayer(ConsoleWidth, ConsoleHeight);

        }

        public Symbol[][] BaseLayer { get; private set; }


        public int ConsoleWidth { get; }
        public int ConsoleHeight { get; }

        private Symbol[][] InitializeBaseLayer(int consoleHeight, int consoleWidth)
        {
            var matrix = new Symbol[consoleHeight][];
            for (int y = 0; y < consoleHeight; y++)
            {
                matrix[y]= new Symbol[consoleHeight];
                for (int x = 0; x < consoleWidth; x++)
                {
                    matrix[y][x] = Symbol.DefaultBaseSymbol.GetCopy();
                }
            }

            return matrix;
        }

        public void AddSection(string title,Section newSection)
        {
            Sections.Add(title,newSection);

            // check if intersect with windows on same layer
            List<Section> sameLayerSections = Sections.Values
                .Where(x => x.Layer == newSection.Layer)
                .ToList();

            foreach (Section curSection in sameLayerSections.Where(s  => !s.Equals(newSection)))
            {
                if (Intersect(newSection, curSection))
                    throw new ArgumentException($"The Section you are trying to add W:{newSection.Width} H:{newSection.Height} L:{newSection.Layer}"
                                                +Environment.NewLine
                                                +$"intersects with the W:{curSection.Width} H:{curSection.Height} L:{curSection.Layer} section");
            }
        }

        public Section GetSection(string stringKey)
        {
            if (Sections.ContainsKey(stringKey))
            {
                return Sections[stringKey];
            }
            throw new ArgumentException($"Could not find a section matching the passed key:  {stringKey}");
        }
        private bool Intersect(Section winA, Section winB)
        {
            bool noIntersect = winA.Corners.TopLeft().X > winB.Corners.BottomRight().X ||
                               winB.Corners.TopLeft().X > winA.Corners.BottomRight().X ||
                               winA.Corners.TopLeft().Y > winB.Corners.BottomRight().Y ||
                               winB.Corners.TopLeft().Y > winA.Corners.BottomRight().Y;

            return !noIntersect;
        }

        public void UpdateBaseLayer()
        {
            var collection = Sections.Values.ToArray().OrderBy(section => section.Layer);

            foreach (var section in collection)
            {
                InprintSection(section);
            }
        }

        private void InprintSection(Section section)
        {
            for (int y = 0; y < section.Height; y++)
            {
                for (int x = 0; x < section.Width; x++)
                {
                    this.BaseLayer[section.PinY + y][section.PinX + x] = section.SectionSymbols[y][x];
                }
            }
        }

        public void PrintToConsole( )
        {

            UpdateBaseLayer();
            for (int row = 0; row < ConsoleToManage.Width; row++)
            {
                for (int col = 0; col < ConsoleToManage.Height; col++)
                {
                    //nooo need to check if there is actual change in the cell we are writing, the console does that on its own
                    ConsoleToManage.Write(row, col, BaseLayer[row][col].Character, BaseLayer[row][col].FontColor,
                        BaseLayer[row][col].BackgroundColor);
                }
            }
        }

    }
}
