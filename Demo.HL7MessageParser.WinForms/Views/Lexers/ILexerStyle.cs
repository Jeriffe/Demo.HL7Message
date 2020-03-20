using ScintillaNET;

namespace Demo.HL7MessageParser.WinForms.Lexers
{
    public interface ILexerStyle
    {
        void LexerStyle(Scintilla scintilla);
    }

    public enum StyleType
    {
        Xml,
        Json,
        CSharp,
    }
}