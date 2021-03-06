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
using System;

namespace Cube.Generics
{
    /* --------------------------------------------------------------------- */
    ///
    /// StringExtension
    ///
    /// <summary>
    /// Provides extended methods for the string class.
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
        /// Gets the value indicating whether the specified value has
        /// one or more character.
        /// </summary>
        ///
        /// <param name="src">Source string.</param>
        ///
        /// <returns>true for one mor charecter.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasValue(this string src) => !string.IsNullOrEmpty(src);

        /* ----------------------------------------------------------------- */
        ///
        /// Unify
        ///
        /// <summary>
        /// Converts a null or empty string to the empty one.
        /// </summary>
        ///
        /// <param name="src">Source string.</param>
        ///
        /// <returns>Converted string.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string Unify(this string src) => src ?? string.Empty;

        /* ----------------------------------------------------------------- */
        ///
        /// Quote
        ///
        /// <summary>
        /// Quotes the specified string.
        /// </summary>
        ///
        /// <param name="src">Source string.</param>
        ///
        /// <returns>Quoted string.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string Quote(this string src) => $"\"{src}\"";

        /* ----------------------------------------------------------------- */
        ///
        /// GetName
        ///
        /// <summary>
        /// Gets the directory name corresponding to the specified value.
        /// </summary>
        ///
        /// <param name="src">Source string.</param>
        ///
        /// <returns>Quoted string.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static string GetName(this Environment.SpecialFolder src) =>
            Environment.GetFolderPath(src);

        #endregion
    }
}
