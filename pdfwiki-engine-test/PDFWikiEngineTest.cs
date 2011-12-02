/* ------------------------------------------------------------------------- */
/*
 *  PDFWikiEngineTest.cs
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
using System.IO;
using System.Text;
using NUnit.Framework;

namespace PDFWiki {
    /* --------------------------------------------------------------------- */
    /// PDFWikiEngineTest
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class PDFWikiEngineTest {
        /* ----------------------------------------------------------------- */
        /// TestPukiwikiParser
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestPukiwkiParser() {
            var parser = new PukiwikiParser();
            var path = System.Environment.CurrentDirectory + @"\examples\example.txt";
            using (var reader = new StreamReader(path, Encoding.UTF8)) {
                parser.Run(reader);
            }

            var v = parser.Elements;
            int index = 0;

            // サンプルテキスト
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(1, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);

            // サブセクション
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(2, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);

            // サブ*2セクション
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(3, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);

            // サブ*3セクション
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(4, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);

            // サブ*4セクション
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(5, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);

            // リスト
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(1, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
            Assert.AreEqual(ElementType.Section, v[index++].Type);
            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
            Assert.AreEqual(ElementType.List, v[index++].Type);
            Assert.AreEqual(ElementType.List, v[index++].Type);
            Assert.AreEqual(ElementType.List, v[index++].Type);
            Assert.AreEqual(ElementType.List, v[index++].Type);
            Assert.AreEqual(ElementType.Section, v[index++].Type);
            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);
            Assert.AreEqual(ElementType.NumericList, v[index++].Type);

            // 画像
            Assert.AreEqual(ElementType.Section, v[index].Type);
            Assert.AreEqual(1, v[index].Depth);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
            Assert.AreEqual(ElementType.Image, v[index].Type);
            Assert.AreEqual("example.jpg", v[index].Value);
            ++index;

            Assert.AreEqual(ElementType.Paragraph, v[index++].Type);
        }
    }
}
