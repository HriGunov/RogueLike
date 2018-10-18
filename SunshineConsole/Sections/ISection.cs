using SunshineConsole.Sections.Interfaces;

namespace SunshineConsole.Sections
{
    public interface ISection
    {
        ICorners Corners { get; }
        int Height { get; }
        int Layer { get; }
        int PinX { get; set; }
        int PinY { get; set; }
        Symbol[][] SectionSymbols { get; }
        int Width { get; }

        void Clear();
        void ClearRow(int row);
        void SetSymbol(Symbol sym, int y, int x);
        void SetTextLine(Symbol[] text, int atRow, int atCol);
        void Update(Symbol[][] newMatrix);
    }
}