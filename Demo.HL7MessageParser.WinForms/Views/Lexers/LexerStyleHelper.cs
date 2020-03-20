using Demo.HL7MessageParser.WinForms.Lexers;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.HL7MessageParser.WinForms
{
    public static class ScintillaHelper
    {
        /*
         * https://github.com/jacobslusser/ScintillaNET/wiki/User-Submitted-Recipes
         * https://github.com/jacobslusser/ScintillaNET/wiki/Automatic-Syntax-Highlighting
        */

        public static void FormatJsonStyle(this Scintilla scintilla)
        {
            scintilla.FormatStyle(StyleType.Json);
        }


        public static void FormatXmlStyle(this Scintilla scintilla)
        {
            scintilla.FormatStyle(StyleType.Xml);
        }


        public static void FormatStyle(this Scintilla scintilla, StyleType styleType)
        {
            ILexerStyle style = null;

            switch (styleType)
            {
                case StyleType.Xml:
                    style = new XmlLexerStyle();
                    break;
                case StyleType.Json:
                    style = new JsonLexerStyle();
                    break;
                case StyleType.CSharp:
                default:
                    throw new NotImplementedException(styleType.ToString());
            }

            style.LexerStyle(scintilla);
        }
    }
}
