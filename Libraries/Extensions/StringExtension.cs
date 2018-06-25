﻿/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
namespace Cube.Generics
{
    /* --------------------------------------------------------------------- */
    ///
    /// StringExtension
    ///
    /// <summary>
    /// 文字列の拡張用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class StringExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// HasValue
        ///
        /// <summary>
        /// 1 文字以上の文字を保持しているかどうかを判別します。
        /// </summary>
        ///
        /// <param name="src">文字列</param>
        ///
        /// <returns>1 文字以上の文字を保持しているかどうか</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasValue(this string src) => !string.IsNullOrEmpty(src);

        /* ----------------------------------------------------------------- */
        ///
        /// Quote
        ///
        /// <summary>
        /// 文字列を引用符で囲みます。
        /// </summary>
        ///
        /// <param name="src">文字列</param>
        ///
        /// <returns>変換後の文字列</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string Quote(this string src) => $"\"{src}\"";

        #endregion
    }
}
