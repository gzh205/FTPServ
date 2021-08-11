﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace FTPServ.CmdIO
{
    public class TextConsoleReader : TextReader
    {
        public static int bufferedlength;
        public string readstr = "";
        private TextBox _textBox { set; get; }
        public TextConsoleReader(System.Windows.Forms.TextBox txt)
        {
            this._textBox = txt;
            bufferedlength = 0;
            Console.SetIn(this);
        }
        public override string ReadLine()
        {
            readstr = _textBox.Text.Substring(bufferedlength, _textBox.Text.Length - bufferedlength);
            bufferedlength = _textBox.Text.Length;
            return readstr;
        }
    }
}