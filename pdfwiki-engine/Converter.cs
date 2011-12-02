/* ------------------------------------------------------------------------- */
/*
 *  Converter.cs
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
using System.IO;
using iText = iTextSharp.text;
using iTextPDF = iTextSharp.text.pdf;

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    /// Converter
    /* --------------------------------------------------------------------- */
    public class Converter {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter(Parser parser) {
            _parser = parser;
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Converter(Parser parser, Style policy) {
            _parser = parser;
            _policy = policy;
        }

        /* ----------------------------------------------------------------- */
        /// Run
        /* ----------------------------------------------------------------- */
        public void Run(string path) {
            iTextPDF.PdfWriter.GetInstance(_doc, new System.IO.FileStream(path, System.IO.FileMode.Create));
            _doc.Open();

            while (_index < _parser.Elements.Count) {
                var element = _parser.Elements[_index];
                if (element.Type == ElementType.Section && element.Depth == 1) this.AddElements(null);
                else {
                    var chapter = new iText.Chapter(_chapter++);
                    this.AddElements(chapter);
                    _doc.Add(chapter);
                }
            }

            _doc.Close();
        }

        /* ----------------------------------------------------------------- */
        /// Clear
        /* ----------------------------------------------------------------- */
        public void Clear() {
            _doc = new iText.Document();
            _index = 0;
            _chapter = 1;
        }

        /* ----------------------------------------------------------------- */
        /// プロパティ定義
        /* ----------------------------------------------------------------- */
        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Policy
        /// 
        /// <summary>
        /// 生成する PDF のスタイル・ポリシーを取得，または設定する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public Style Policy {
            get { return _policy; }
            set { _policy = value; }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 内部処理のためのメソッド群
        /* ----------------------------------------------------------------- */
        #region Private methods

        /* ----------------------------------------------------------------- */
        /// AddElements
        /* ----------------------------------------------------------------- */
        private void AddElements(iText.Section parent) {
            while (_index < _parser.Elements.Count) {
                var element = _parser.Elements[_index];
                switch (element.Type) {
                    case ElementType.Section:
                        if (parent != null && element.Depth <= parent.Depth) return;
                        this.AddSection(parent, element);
                        break;
                    case ElementType.Paragraph:
                        this.AddParagraph(parent, element);
                        break;
                    case ElementType.List:
                        this.AddList(parent, element);
                        break;
                    case ElementType.NumericList:
                        this.AddNumericList(parent, element);
                        break;
                    case ElementType.Image:
                        this.AddImage(parent, element);
                        break;
                    default: // 未対応のデータ種類
                        ++_index;
                        break;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// AddSection
        /* ----------------------------------------------------------------- */
        private void AddSection(iText.Section parent, Element element) {
            var style = (element.Depth == 1) ? _policy.Chapter : _policy.Section;
            var title = new iText.Paragraph(element.Value, style.Font);
            title.SpacingAfter = style.Spacing;
            iText.Section section = (parent == null) ? new iText.Chapter(title, _chapter++) : parent.AddSection(title);
            section.IndentationLeft = style.Indent;
            section.TriggerNewPage = style.TriggerNewPage;
            ++_index;
            this.AddElements(section);
            if (parent == null) _doc.Add(section);
        }

        /* ----------------------------------------------------------------- */
        /// AddParagraph
        /* ----------------------------------------------------------------- */
        private void AddParagraph(iText.ITextElementArray parent, Element element) {
            var paragraph = new iText.Paragraph(element.Value,_policy.Paragraph.Font);
            paragraph.IndentationLeft = _policy.Paragraph.Indent;
            paragraph.FirstLineIndent = _policy.Paragraph.FirstLineIndent;
            paragraph.SpacingAfter = _policy.Paragraph.Spacing;
            parent.Add(paragraph);
            ++_index;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddList
        ///
        /// <summary>
        /// NOTE: 行間は，各種要素 (Paragraph, Section, List, NumericList)
        /// の末尾に空白を取る事で調整する．そのため，リストの最後の項目に
        /// 空白を挿入している．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void AddList(iText.ITextElementArray parent, Element element) {
            var list = new iText.List(false, _policy.List.SymbolIndent);
            list.IndentationLeft = _policy.List.Indent;
            list.Symbol = new iText.Chunk(_policy.List.Symbol);

            while (_index < _parser.Elements.Count && _parser.Elements[_index].Type == element.Type) {
                var item = new iText.Paragraph(_parser.Elements[_index].Value, _policy.List.Font);
                ++_index;
                if (_index >= _parser.Elements.Count || _parser.Elements[_index].Type != element.Type) {
                    item.SpacingAfter = _policy.List.Spacing;
                }
                list.Add(new iText.ListItem(item));
            }

            parent.Add(list);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddNumericList
        /// 
        /// <summary>
        /// NOTE: 行間は，各種要素 (Paragraph, Section, List, NumericList)
        /// の末尾に空白を取る事で調整する．そのため，リストの最後の項目に
        /// 空白を挿入している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void AddNumericList(iText.ITextElementArray parent, Element element) {
            var list = new iText.List(true, _policy.NumericList.SymbolIndent);
            list.IndentationLeft = _policy.NumericList.Indent;
            while (_index < _parser.Elements.Count && _parser.Elements[_index].Type == element.Type) {
                var item = new iText.Paragraph(_parser.Elements[_index].Value, _policy.NumericList.Font);
                ++_index;
                if (_index >= _parser.Elements.Count || _parser.Elements[_index].Type != element.Type) {
                    item.SpacingAfter = _policy.NumericList.Spacing;
                }
                list.Add(new iText.ListItem(item));
            }

            parent.Add(list);
        }

        /* ----------------------------------------------------------------- */
        /// AddImage
        /* ----------------------------------------------------------------- */
        private void AddImage(iText.ITextElementArray parent, Element element) {
            var path = Path.IsPathRooted(element.Value) ? element.Value : Path.GetFullPath(element.Value);
            ++_index;
            if (!File.Exists(path)) return;

            var image = iText.Image.GetInstance(path);
            image.Alignment = _policy.Image.Alignment;
            image.SetDpi(_policy.Image.DPI, _policy.Image.DPI);
            parent.Add(image);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        /// 変数定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private iText.Document _doc = new iText.Document();
        private Parser _parser = null;
        private Style _policy = new Style();
        private int _index = 0;
        private int _chapter = 1;
        #endregion
    }
}
