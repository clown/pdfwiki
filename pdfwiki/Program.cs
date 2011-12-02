/* ------------------------------------------------------------------------- */
/*
 *  Program.cs
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

namespace PDFWiki {
    class Program {
        static void Main(string[] args) {
            foreach (var src in args) {
                try {
                    var parser = new PukiwikiParser();
                    using (var reader = new StreamReader(src, Encoding.UTF8)) {
                        parser.Run(reader);
                    }
                    var converter = new Converter(parser);
                    converter.Run(Path.ChangeExtension(src, ".pdf"));
                }
                catch (Exception err) {
                    Console.WriteLine(err.Message);
                }
            }
        }
    }
}
