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
using System.Collections.Generic;

namespace Cube.Differences
{
    /* --------------------------------------------------------------------- */
    ///
    /// Condition
    ///
    /// <summary>
    /// 差分の状態を表す列挙型です。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public enum Condition
    {
        /// <summary>差分無し</summary>
        None,
        /// <summary>追加（新しいシーケンスにのみ存在する）</summary>
        Inserted,
        /// <summary>削除（古いシーケンスにのみ存在する）</summary>
        Deleted,
        /// <summary>変更</summary>
        Changed,
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Result
    ///
    /// <summary>
    /// 比較結果を保持するためのクラスです。
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class Result<T>
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Result
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// 
        /// <param name="condition">差分の状態</param>
        /// <param name="older">変更前シーケンスの対象部分</param>
        /// <param name="newer">変更後シーケンスの対象部分</param>
        ///
        /* ----------------------------------------------------------------- */
        public Result(Condition condition, IEnumerable<T> older, IEnumerable<T> newer)
        {
            Condition = condition;
            Older     = older;
            Newer     = newer;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Condition
        /// 
        /// <summary>
        /// 差分の状態を示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Condition Condition { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Older
        /// 
        /// <summary>
        /// 変更前シーケンスの対象部分を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<T> Older { get; }

        /* ----------------------------------------------------------------- */
        ///
        /// Newer
        /// 
        /// <summary>
        /// 変更後シーケンスの対象部分を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public IEnumerable<T> Newer { get; }

        #endregion
    }
}
