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
namespace Cube.Settings
{
    /* --------------------------------------------------------------------- */
    ///
    /// SettingsType
    /// 
    /// <summary>
    /// Settings クラスで読み込み、および保存可能なデータ形式一覧を
    /// 表した列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum SettingsType
    {
        /// <summary>XML</summary>
        Xml,
        /// <summary>JSON</summary>
        Json,
        /// <summary>不明</summary>
        Unknown = -1
    }
}
