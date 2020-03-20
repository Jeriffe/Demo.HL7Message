using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;

namespace Demo.HL7MessageParser.WinForms.Lexers
{
    public class JsonLexerStyle : ILexerStyle
    {
        public void LexerStyle(Scintilla scintilla)
        {
            scintilla.Styles[Style.Json.Default].ForeColor = Color.Silver;
            scintilla.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintilla.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            scintilla.Styles[Style.Json.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            scintilla.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            scintilla.Lexer = ScintillaNET.Lexer.Json;
        }
    }
}
