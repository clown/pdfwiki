/* ------------------------------------------------------------------------- */
/*
 *  Style.cs
 *
 *  Copyright (c) 2011, clown.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU Affero General Public License as
 *  published by the Free Software Foundation, either version 3 of the
 *  License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Affero General Public License for more details.
 *
 *  You should have received a copy of the GNU Affero General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Text;
using iText = iTextSharp.text;
using iTextPDF = iTextSharp.text.pdf;

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    ///
    /// StringStyle
    /// 
    /// <summary>
    /// 文字列で記述する要素 (Section, Paragraph, List, ...) に共通する
    /// スタイル要素を定義したクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class StringStyle {
        /* ----------------------------------------------------------------- */
        /// Font
        /* ----------------------------------------------------------------- */
        public iText.Font Font {
            get { return _font; }
            set { _font = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Spacing
        /* ----------------------------------------------------------------- */
        public float Spacing {
            get { return _space; }
            set { _space = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Indent
        /* ----------------------------------------------------------------- */
        public float Indent {
            get { return _indent; }
            set { _indent = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private iText.Font _font = null;
        private float _space = 0.0F;
        private float _indent = 0.0F;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// SectionStyle
    /// 
    /// <summary>
    /// セクションのスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class SectionStyle : StringStyle {
        /* ----------------------------------------------------------------- */
        /// TriggerNewPage
        /* ----------------------------------------------------------------- */
        public bool TriggerNewPage {
            get { return _newpage; }
            set { _newpage = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private bool _newpage = false;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ParagraphStyle
    /// 
    /// <summary>
    /// 本文のスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ParagraphStyle : StringStyle {
        /* ----------------------------------------------------------------- */
        /// FirstLineIndent
        /* ----------------------------------------------------------------- */
        public float FirstLineIndent {
            get { return _firstline; }
            set { _firstline = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private float _firstline = 0.0F;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ListStyle
    /// 
    /// <summary>
    /// リストのスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ListStyle : StringStyle {
        /* ----------------------------------------------------------------- */
        /// Symbol
        /* ----------------------------------------------------------------- */
        public char Symbol {
            get { return _symbol; }
            set { _symbol = value; }
        }

        /* ----------------------------------------------------------------- */
        /// SymbolIndent
        /* ----------------------------------------------------------------- */
        public float SymbolIndent {
            get { return _symbol_indent; }
            set { _symbol_indent = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private char  _symbol = '-';
        private float _symbol_indent = 0.0F;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// NumericListStyle
    /// 
    /// <summary>
    /// 数字付リストのスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class NumericListStyle : StringStyle {
        /* ----------------------------------------------------------------- */
        /// SymbolIndent
        /* ----------------------------------------------------------------- */
        public float SymbolIndent {
            get { return _symbol_indent; }
            set { _symbol_indent = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private float _symbol_indent = 0.0F;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ImageStyle
    /// 
    /// <summary>
    /// 画像のスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ImageStyle {
        /* ----------------------------------------------------------------- */
        /// Alignment
        /* ----------------------------------------------------------------- */
        public int Alignment {
            get { return _align; }
            set { _align = value; }
        }

        /* ----------------------------------------------------------------- */
        /// DPI
        /* ----------------------------------------------------------------- */
        public int DPI {
            get { return _dpi; }
            set { _dpi = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private int _align = 0;
        private int _dpi = 300;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Style
    /// 
    /// <summary>
    /// 生成する PDF の各種要素のスタイルを定義するクラス．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class Style {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Style() {
            this.LoadDefaultStyle();
        }

        /* ----------------------------------------------------------------- */
        /// Chapter
        /* ----------------------------------------------------------------- */
        public SectionStyle Chapter {
            get { return _chapter; }
            set { _chapter = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Section
        /* ----------------------------------------------------------------- */
        public SectionStyle Section {
            get { return _section; }
            set { _section = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Paragraph
        /* ----------------------------------------------------------------- */
        public ParagraphStyle Paragraph {
            get { return _paragraph; }
            set { _paragraph = value; }
        }

        /* ----------------------------------------------------------------- */
        /// List
        /* ----------------------------------------------------------------- */
        public ListStyle List {
            get { return _list; }
            set { _list = value; }
        }

        /* ----------------------------------------------------------------- */
        /// NumericList
        /* ----------------------------------------------------------------- */
        public NumericListStyle NumericList {
            get { return _numeric; }
            set { _numeric = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Image
        /* ----------------------------------------------------------------- */
        public ImageStyle Image {
            get { return _image; }
            set { _image = value; }
        }

        /* ----------------------------------------------------------------- */
        /// 内部処理のためのメソッド群
        /* ----------------------------------------------------------------- */
        #region Private methods

        /* ----------------------------------------------------------------- */
        ///
        /// LoadDefaultStyle
        ///
        /// <summary>
        /// ユーザが特に何も指定しない場合のデフォルトのスタイル．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void LoadDefaultStyle() {
            var path = GetFontsFolderPath() + @"\msmincho.ttc,1"; // MS-P明朝
            var basefont = iTextPDF.BaseFont.CreateFont(path, iTextPDF.BaseFont.IDENTITY_H, true);

            // 本文のスタイル
            _paragraph.Font = new iText.Font(basefont, 10);
            _paragraph.Spacing = _paragraph.Font.Size;
            _paragraph.Indent = 0.0F;
            _paragraph.FirstLineIndent = _paragraph.Font.Size;

            // チャプターのスタイル
            _chapter.Font = new iText.Font(basefont, 16, iText.Font.BOLD);
            _chapter.Spacing = _paragraph.Spacing;
            _chapter.Indent = 0.0F;
            _chapter.TriggerNewPage = false;

            // セクションのスタイル
            _section.Font = new iText.Font(basefont, 12, iText.Font.BOLD);
            _section.Spacing = _paragraph.Spacing;
            _section.Indent = 0.0F;
            _section.TriggerNewPage = false;

            // リストのスタイル
            _list.Font = _paragraph.Font;
            _list.Spacing = _paragraph.Spacing;
            _list.Indent = _list.Font.Size;
            _list.SymbolIndent = _list.Font.Size * 1.5F;
            _list.Symbol = '・';

            // 数字付リストのスタイル
            _numeric.Font = _paragraph.Font;
            _numeric.Spacing = _paragraph.Spacing;
            _numeric.Indent = _list.Font.Size;
            _numeric.SymbolIndent = _list.Font.Size * 1.5F;

            // 画像のスタイル
            _image.Alignment = 0;
            _image.DPI = 300;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetFontsFolderPath
        ///
        /// <summary>
        /// フォントが格納されているフォルダのパスを取得する．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private string GetFontsFolderPath() {
            StringBuilder buffer = new StringBuilder();
            SHGetFolderPath(IntPtr.Zero, CSIDL_FONTS, IntPtr.Zero, SHGFP_TYPE_CURRENT, buffer);
            return buffer.ToString();
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 使用している Win32 APIs
        /* ----------------------------------------------------------------- */
        #region Win32 APIs

        [System.Runtime.InteropServices.DllImport("shell32.dll")]
        public static extern Int32 SHGetFolderPath(IntPtr hwndOwner, Int32 nFolder, IntPtr hToken, UInt32 dwFlags, StringBuilder pszPath);

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private SectionStyle _chapter = new SectionStyle();
        private SectionStyle _section = new SectionStyle();
        private ParagraphStyle _paragraph = new ParagraphStyle();
        private ListStyle _list = new ListStyle();
        private NumericListStyle _numeric = new NumericListStyle();
        private ImageStyle _image = new ImageStyle();
        #endregion

        /* ----------------------------------------------------------------- */
        /// 定数定義
        /* ----------------------------------------------------------------- */
        #region Constant variables
        private const Int32 CSIDL_FONTS = 0x0014;
        private const UInt32 SHGFP_TYPE_CURRENT = 0x0000;
        #endregion
    }
}
