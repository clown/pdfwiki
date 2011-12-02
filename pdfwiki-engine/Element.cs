/* ------------------------------------------------------------------------- */
/*
 *  Element.cs
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

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    ///
    /// ElementType
    ///
    /// PDFWiki で対応しているデータの種類一覧．
    ///
    /* --------------------------------------------------------------------- */
    public enum ElementType : int {
        Unknown,
        Paragraph,
        Section,
        List,
        NumericList,
        Image,
    }

    /* --------------------------------------------------------------------- */
    /// Element
    /* --------------------------------------------------------------------- */
    public class Element {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Element() { }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Element(ElementType type, int depth, string value) {
            _type  = type;
            _depth = depth;
            _value = value;
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        /// Type
        /* ----------------------------------------------------------------- */
        public ElementType Type {
            get { return _type; }
            set { _type = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Depth
        /* ----------------------------------------------------------------- */
        public int Depth {
            get { return _depth; }
            set { _depth = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Value
        /* ----------------------------------------------------------------- */
        public string Value {
            get { return _value; }
            set { _value = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 各種変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private ElementType _type = ElementType.Unknown;
        private int _depth = 1;
        private string _value = "";
        #endregion
    }
}
