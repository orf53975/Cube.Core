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
using Cube.Collections;
using Cube.Generics;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// OrderedDictionaryTest
    ///
    /// <summary>
    /// OrderedDictionary のテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class OrderedDictionaryTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Access_WithKey
        ///
        /// <summary>
        /// キーで値にアクセスするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Access_WithKey()
        {
            var src = Create();
            Assert.That(src.IsReadOnly, Is.False);
            Assert.That(src.Count,      Is.EqualTo(5));
            Assert.That(src["Linus"],   Is.EqualTo("Torvalds"));

            src["Richard"] = "Rossum";
            Assert.That(src["Richard"], Is.EqualTo("Rossum"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Access_WithKey_Throws
        ///
        /// <summary>
        /// 無効なキーでアクセスしようとした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Access_WithKey_Throws()
        {
            var src = Create();
            Assert.That(() => src["John"],       Throws.TypeOf<KeyNotFoundException>());
            Assert.That(() => src["John"] = "a", Throws.TypeOf<KeyNotFoundException>());
            Assert.That(() => src[null],         Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => src[null] = "a",   Throws.TypeOf<ArgumentNullException>());
        }


        /* ----------------------------------------------------------------- */
        ///
        /// Access_WithIndex
        ///
        /// <summary>
        /// インデックスで値にアクセスするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Access_WithIndex()
        {
            var src = Create();
            Assert.That(src.IsReadOnly, Is.False);
            Assert.That(src.Count,      Is.EqualTo(5));
            Assert.That(src[0],         Is.EqualTo("Ritchie"));
            Assert.That(src[1],         Is.EqualTo("Kernighan"));
            Assert.That(src[2],         Is.EqualTo("Thompson"));
            Assert.That(src[3],         Is.EqualTo("Torvalds"));
            Assert.That(src[4],         Is.EqualTo("Stallman"));

            src[2] = "Gutmans";
            Assert.That(src[2],         Is.EqualTo("Gutmans"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Access_WithIndex_Throws
        ///
        /// <summary>
        /// 範囲外の要素にアクセスしようとした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(5)]
        [TestCase(-1)]
        public void Access_WithIndex_Throws(int index)
        {
            var src = Create();
            Assert.That(() => src[index],      Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => src[index] = "", Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Add_Throws
        ///
        /// <summary>
        /// 無効な値を追加しようとした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Add_Throws() => Assert.That(
            () => Create().Add(null, null),
            Throws.TypeOf<ArgumentNullException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Remove
        ///
        /// <summary>
        /// KeyValuePair(string, string) を指定して Remove を実行した時の
        /// 挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("Linus", "Torvalds", ExpectedResult = true)]
        [TestCase("Linus", "Stallman", ExpectedResult = false)]
        [TestCase("",      "Torvalds", ExpectedResult = false)]
        [TestCase(null,   null,        ExpectedResult = false)]
        public bool Remove(string key, string value) =>
            Create().Remove(KeyValuePair.Create(key, value));

        /* ----------------------------------------------------------------- */
        ///
        /// CopyFrom
        ///
        /// <summary>
        /// コピーコンストラクタの挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void CopyFrom()
        {
            var src  = Create();
            var dest = new OrderedDictionary<string, string>(src);
            Assert.That(dest.Count, Is.EqualTo(src.Count));

            src["Brian"] = "Kay";
            Assert.That(src["Brian"],  Is.EqualTo("Kay"));
            Assert.That(dest["Brian"], Is.EqualTo("Kernighan"));

            Assert.That(new OrderedDictionary<string, string>(null).Count, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyTo
        ///
        /// <summary>
        /// 要素をコピーするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void CopyTo()
        {
            var src  = Create();
            var dest = new KeyValuePair<string, string>[5];

            src.CopyTo(dest, 0);
            Assert.That(dest[0].Key, Is.EqualTo("Dennis"));
            Assert.That(dest[1].Key, Is.EqualTo("Brian"));
            Assert.That(dest[2].Key, Is.EqualTo("Kenneth"));
            Assert.That(dest[3].Key, Is.EqualTo("Linus"));
            Assert.That(dest[4].Key, Is.EqualTo("Richard"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CopyTo_Throws
        ///
        /// <summary>
        /// CopyTo に無効な引数が指定された時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void CopyTo_Throws()
        {
            var src  = Create();
            var dest = new KeyValuePair<string, string>[7];

            Assert.That(() => src.CopyTo(null, 0),  Throws.TypeOf<ArgumentNullException>());
            Assert.That(() => src.CopyTo(dest, -1), Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => src.CopyTo(dest, 7),  Throws.TypeOf<ArgumentOutOfRangeException>());
            Assert.That(() => src.CopyTo(dest, 3),  Throws.TypeOf<ArgumentException>());
        }

        /* ----------------------------------------------------------------- */
        ///
        /// KeysAndValues
        ///
        /// <summary>
        /// Keys および Values の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void KeysAndValues()
        {
            var src    = Create();
            var keys   = src.Keys;
            var values = src.Values;

            Assert.That(() => keys.Add("a"),   Throws.TypeOf<NotSupportedException>());
            Assert.That(() => values.Add("a"), Throws.TypeOf<NotSupportedException>());

            src.Add("Bjarne", "Stroustrup");
            src.Add(KeyValuePair.Create("Anders", "Hejlsberg"));
            Assert.That(src.Count,      Is.EqualTo(7));
            Assert.That(keys.Count,     Is.EqualTo(5));
            Assert.That(values.Count,   Is.EqualTo(5));

            src.Remove("Richard");
            src.Remove("Dennis");
            src.Remove("Kenneth");
            src.Remove("Dummy");
            Assert.That(src.Count,      Is.EqualTo(4));
            Assert.That(keys.Count,     Is.EqualTo(5));
            Assert.That(values.Count,   Is.EqualTo(5));

            src.Clear();
            Assert.That(src.Count,      Is.EqualTo(0));
            Assert.That(keys.Count,     Is.EqualTo(5));
            Assert.That(values.Count,   Is.EqualTo(5));
            Assert.That(keys.First(),   Is.EqualTo("Dennis"));
            Assert.That(values.First(), Is.EqualTo("Ritchie"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TryGetValue
        ///
        /// <summary>
        /// TryGetValue の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TryGetValue()
        {
            var src = Create();

            Assert.That(src.TryGetValue("Linus", out string s0), Is.True);
            Assert.That(s0, Is.EqualTo("Torvalds"));

            Assert.That(src.TryGetValue("Bjarne", out string s1), Is.False);
            Assert.That(s1, Is.Null);

            Assert.That(src.TryGetValue("", out string s2), Is.False);
            Assert.That(s2, Is.Null);

            Assert.That(src.TryGetValue(null, out string s3), Is.False);
            Assert.That(s3, Is.Null);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEnumerator
        ///
        /// <summary>
        /// IEnumerable.GetEnumerator の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetEnumerator()
        {
            var src = ((IEnumerable)Create()).GetEnumerator();
            Assert.That(src.MoveNext(), Is.True);
            Assert.That(
                src.Current.TryCast<KeyValuePair<string, string>>().Key,
                Is.EqualTo("Dennis")
            );
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        ///
        /// <summary>
        /// OrderedDictionary(string, string) オブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private OrderedDictionary<string, string> Create() =>
            new OrderedDictionary<string, string>
            {
                { "Dennis",  "Ritchie"   },
                { "Brian",   "Kernighan" },
                { "Kenneth", "Thompson"  },
                { "Linus",   "Torvalds"  },
                { "Richard", "Stallman"  },
            };

        #endregion
    }
}
