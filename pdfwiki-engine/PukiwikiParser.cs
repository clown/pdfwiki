/* ------------------------------------------------------------------------- */
/*
 *  PukiwikiParser.cs
 *
 *  Copyright (c) 2011, clown. All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *    - Redistributions of source code must retain the above copyright
 *      notice, this list of conditions and the following disclaimer.
 *    - Redistributions in binary form must reproduce the above copyright
 *      notice, this list of conditions and the following disclaimer in the
 *      documentation and/or other materials provided with the distribution.
 *    - No names of its contributors may be used to endorse or promote
 *      products derived from this software without specific prior written
 *      permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 *  A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 *  OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 *  TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
 *  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 *  LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
 *  NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Text;
using System.IO;

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    ///
    /// PukiwikiParser
    ///
    /// <summary>
    /// Pukiwiki で使用されている Wiki 記法をパースするクラス．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class PukiwikiParser : Parser {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public PukiwikiParser() : base() { }

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public override void Run(TextReader src) {
            var buffer = new StringBuilder();
            for (var line = src.ReadLine(); line != null; line = src.ReadLine()) {
                if (line.Length == 0 || this.HasSymbol(line)) {
                    if (buffer.Length > 0) {
                        this._elements.Add(new Element(ElementType.Paragraph, 0, buffer.ToString()));
                        buffer.Remove(0, buffer.Length);
                    }

                    if (line.Length == 0) continue;
                    else if (line[0] == '*') this.ParseBasicElement(line, ElementType.Section);
                    else if (line[0] == '-') this.ParseBasicElement(line, ElementType.List);
                    else if (line[0] == '+') this.ParseBasicElement(line, ElementType.NumericList);
                    else if (line[0] == '#') this.ParseImage(line);
                }
                else {
                    if (line[line.Length - 1] == '~') line = line.Substring(0, line.Length - 1) + "\r\n";
                    buffer.Append(line);
                }
            }

            if (buffer.Length > 0) {
                this._elements.Add(new Element(ElementType.Paragraph, 0, buffer.ToString()));
                buffer.Remove(0, buffer.Length);
            }
        }

        /* ----------------------------------------------------------------- */
        /// 内部処理のためのメソッド群
        /* ----------------------------------------------------------------- */
        #region Private methods

        /* ----------------------------------------------------------------- */
        ///
        /// HasSymbol
        ///
        /// <summary>
        /// 行頭に意味のあるシンボルが指定されているかどうかを判定する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool HasSymbol(string src) {
            if (src == null || src.Length == 0) return false;
            if (src[0] == '*' || src[0] == '-' || src[0] == '+' || src[0] == '#') return true;
            return false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ParseBasicElement
        ///
        /// <summary>
        /// 行頭にのみシンボルが存在するような要素をパースする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ParseBasicElement(string src, ElementType type) {
            var element = new Element();
            element.Type = type;

            int pos = 0;
            while (src[pos] == src[0]) ++pos;
            element.Depth = pos;

            while (src[pos] == ' ') ++pos;
            element.Value = src.Substring(pos);

            this._elements.Add(element);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ParseImage
        ///
        /// <summary>
        /// 画像のパスを抽出する．パスは，#ref(example.jpg) と言う
        /// フォーマットで記述されている．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void ParseImage(string src) {
            var regex = new System.Text.RegularExpressions.Regex(@"#ref\((?<path>.*?)\)\s*");
            var match = regex.Match(src);
            if (match.Success) this._elements.Add(new Element(ElementType.Image, 0, match.Groups["path"].Value));
        }

        #endregion
    }
}
