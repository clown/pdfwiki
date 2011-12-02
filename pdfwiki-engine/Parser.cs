/* ------------------------------------------------------------------------- */
/*
 *  Parser.cs
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
using Container = System.Collections.Generic.List<PDFWiki.Element>;

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    ///
    /// Parser
    ///
    /// <summary>
    /// 各種パーサの基底クラス．Wiki 記法に合わせて Run() メソッドを
    /// 実装する事．
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public abstract class Parser {
        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public abstract void Run(TextReader src);

        /* ----------------------------------------------------------------- */
        /// Elements
        /* ----------------------------------------------------------------- */
        public Container Elements {
            get { return _elements; }
            set { _elements = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 各種変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        protected Container _elements = new Container();
        #endregion
    }
}
