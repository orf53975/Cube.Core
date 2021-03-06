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
using Cube.Collections.Mixin;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// EnumerableTest
    ///
    /// <summary>
    /// Tests the extended methods for a sequence.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class EnumerableTest
    {
        #region Tests

        #region IEnumerable<int>

        /* ----------------------------------------------------------------- */
        ///
        /// OrderBy
        ///
        /// <summary>
        /// Executes the test of the OrderBy method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void OrderBy()
        {
            var src = new[] { 3, 1, 4, 1, 5, 9, 2, 6 }.OrderBy().ToList();
            Assert.That(src[0], Is.EqualTo(1));
            Assert.That(src[1], Is.EqualTo(1));
            Assert.That(src[2], Is.EqualTo(2));
            Assert.That(src[3], Is.EqualTo(3));
            Assert.That(src[4], Is.EqualTo(4));
            Assert.That(src[5], Is.EqualTo(5));
            Assert.That(src[6], Is.EqualTo(6));
            Assert.That(src[7], Is.EqualTo(9));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OrderByDescending
        ///
        /// <summary>
        /// Executes the test of the OrderByDescending method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void OrderByDescending()
        {
            var src = new[] { 3, 1, 4, 1, 5, 9, 2, 6 }.OrderByDescending().ToList();
            Assert.That(src[0], Is.EqualTo(9));
            Assert.That(src[1], Is.EqualTo(6));
            Assert.That(src[2], Is.EqualTo(5));
            Assert.That(src[3], Is.EqualTo(4));
            Assert.That(src[4], Is.EqualTo(3));
            Assert.That(src[5], Is.EqualTo(2));
            Assert.That(src[6], Is.EqualTo(1));
            Assert.That(src[7], Is.EqualTo(1));
        }


        /* ----------------------------------------------------------------- */
        ///
        /// Within
        ///
        /// <summary>
        /// Executes the test of the Within method.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Within() => Assert.That(
            new[] { 3, 1, 4, 1, 5, 9, 2, 6, 0, 0 }.Within(5),
            Is.EquivalentTo(new[] { 3, 1, 4, 1, 2, 0, 0 })
        );

        #endregion

        #region Get or Set

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault
        ///
        /// <summary>
        /// GetOrDefault の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetOrDefault()
        {
            var sum = 0;
            var src = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (var i in src.GetOrDefault()) sum += i;
            Assert.That(sum, Is.EqualTo(55));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetOrDefault_Null
        ///
        /// <summary>
        /// null を指定した場合の GetOrDefault の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void GetOrDefault_Null()
        {
            var sum = 0;
            var src = default(List<int>);
            foreach (var i in src.GetOrDefault()) sum += i;
            Assert.That(sum, Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AddOrSet
        ///
        /// <summary>
        /// Tests to add or set values to a dictionary collection.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void AddOrSet()
        {
            var src = new Dictionary<string, int>();
            var key = nameof(AddOrSet);
            for (var i = 0; i < 10; ++i) src.AddOrSet(key, i);
            Assert.That(src[key], Is.EqualTo(9));
        }

        #endregion

        #region FirstIndex

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndex
        ///
        /// <summary>
        /// Tests the FirstIndex method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndex() => Assert.That(
            Enumerable.Range(1, 50).Select(e => e * 2).FirstIndex(e => e < 20),
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndex_Empty
        ///
        /// <summary>
        /// Tests the FirstIndex method against the empty List(T) object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndex_Empty() => Assert.That(
            () => new List<int>().FirstIndex(e => e < 20),
            Throws.TypeOf<InvalidOperationException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndex_Default
        ///
        /// <summary>
        /// Tests the FirstIndex method against the default of List(T).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndex_Default() => Assert.That(
            () => default(List<int>).FirstIndex(e => e < 20),
            Throws.TypeOf<ArgumentNullException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndex_NeverMatch
        ///
        /// <summary>
        /// Tests the FirstIndex method with the never-matched predicate.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndex_NeverMatch() => Assert.That(
            () => Enumerable.Range(1, 10).FirstIndex(e => e > 100),
            Throws.TypeOf<InvalidOperationException>()
        );

        #endregion

        #region FirstIndexOf

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndexOf
        ///
        /// <summary>
        /// Tests the FirstIndexOf method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndexOf() => Assert.That(
            Enumerable.Range(1, 50).Select(e => e * 2).FirstIndexOf(e => e < 20),
            Is.EqualTo(0)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndexOf_Empty
        ///
        /// <summary>
        /// Tests the FirstIndexOf method against the empty List(T) object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndexOf_Empty() => Assert.That(
            new List<int>().FirstIndexOf(e => e < 20),
            Is.EqualTo(-1)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndexOf_Default
        ///
        /// <summary>
        /// Tests the FirstIndexOf method against the default of List(T).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndexOf_Default() => Assert.That(
            default(List<int>).FirstIndexOf(e => e < 20),
            Is.EqualTo(-1)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// FirstIndexOf_NeverMatch
        ///
        /// <summary>
        /// Tests the FirstIndexOf method with the never-matched predicate.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void FirstIndexOf_NeverMatch() => Assert.That(
            Enumerable.Range(1, 10).FirstIndexOf(e => e > 100),
            Is.EqualTo(-1)
        );

        #endregion

        #region LastIndex

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex
        ///
        /// <summary>
        /// Tests the LastIndex method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10, ExpectedResult = 9)]
        [TestCase( 1, ExpectedResult = 0)]
        public int LastIndex(int count) => Enumerable.Range(0, count).LastIndex();

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex
        ///
        /// <summary>
        /// Tests the LastIndex method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndex() => Assert.That(
            Enumerable.Range(1, 50).Select(e => e * 2).LastIndex(e => e < 20),
            Is.EqualTo(8)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex_Empty
        ///
        /// <summary>
        /// Tests the LastIndex method against the empty List(T) object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndex_Empty() => Assert.That(
            () => new List<int>().LastIndex(),
            Throws.TypeOf<InvalidOperationException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex_Default
        ///
        /// <summary>
        /// Tests the LastIndex method against the default of List(T).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndex_Default() => Assert.That(
            () => default(List<int>).LastIndex(),
            Throws.TypeOf<ArgumentNullException>()
        );

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndex_NeverMatch
        ///
        /// <summary>
        /// Tests the LastIndex method with the never-matched predicate.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndex_NeverMatch() => Assert.That(
            () => Enumerable.Range(1, 10).LastIndex(e => e > 100),
            Throws.TypeOf<InvalidOperationException>()
        );

        #endregion

        #region LastIndexOf

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf
        ///
        /// <summary>
        /// Tests the LastIndexOf method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10, ExpectedResult =  9)]
        [TestCase( 1, ExpectedResult =  0)]
        [TestCase( 0, ExpectedResult = -1)]
        public int LastIndexOf(int count) => Enumerable.Range(0, count).LastIndexOf();

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf
        ///
        /// <summary>
        /// Tests the LastIndexOf method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndexOf() => Assert.That(
            Enumerable.Range(1, 50).Select(e => e * 2).LastIndexOf(e => e < 20),
            Is.EqualTo(8)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf_Default
        ///
        /// <summary>
        /// Tests the LastIndexOf method against the default of List(T).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndexOf_Default() => Assert.That(
            default(List<int>).LastIndexOf(),
            Is.EqualTo(-1)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// LastIndexOf_NeverMatch
        ///
        /// <summary>
        /// Tests the LastIndexOf method with the never-matched predicate.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void LastIndexOf_NeverMatch() => Assert.That(
            Enumerable.Range(1, 10).LastIndexOf(e => e > 100),
            Is.EqualTo(-1)
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp
        ///
        /// <summary>
        /// Tests the Clamp method at the specified condition.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(10,  5, ExpectedResult = 5)]
        [TestCase(10, 20, ExpectedResult = 9)]
        [TestCase(10, -1, ExpectedResult = 0)]
        [TestCase( 0, 10, ExpectedResult = 0)]
        [TestCase( 0, -1, ExpectedResult = 0)]
        public int Clamp(int count, int index) => Enumerable.Range(0, count).Clamp(index);

        /* ----------------------------------------------------------------- */
        ///
        /// Clamp_Default
        ///
        /// <summary>
        /// Tests the Clamp method against the default of List(T).
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Clamp_Default() =>
            Assert.That(default(List<int>).Clamp(100), Is.EqualTo(0));

        #endregion

        #region Flatten

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten
        ///
        /// <summary>
        /// ツリー構造を平坦化するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Flatten()
        {
            var src = new[]
            {
                new Tree { Name = "1st" },
                new Tree
                {
                    Name     = "2nd",
                    Children = new[]
                    {
                        new Tree
                        {
                            Name     = "2nd-1st",
                            Children = new[] { new Tree { Name = "2nd-1st-1st" } },
                        },
                        new Tree { Name = "2nd-2nd" },
                        new Tree
                        {
                            Name     = "2nd-3rd",
                            Children = new[]
                            {
                                new Tree { Name = "2nd-3rd-1st" },
                                new Tree { Name = "2nd-3rd-2nd" },
                            },
                        },
                    },
                },
                new Tree { Name = "3rd" },
            };

            var dest = src.Flatten(e => e.Children).ToList();
            Assert.That(dest.Count, Is.EqualTo(9));
            Assert.That(dest[0].Name, Is.EqualTo("1st"));
            Assert.That(dest[1].Name, Is.EqualTo("2nd"));
            Assert.That(dest[2].Name, Is.EqualTo("3rd"));
            Assert.That(dest[3].Name, Is.EqualTo("2nd-1st"));
            Assert.That(dest[4].Name, Is.EqualTo("2nd-2nd"));
            Assert.That(dest[5].Name, Is.EqualTo("2nd-3rd"));
            Assert.That(dest[6].Name, Is.EqualTo("2nd-1st-1st"));
            Assert.That(dest[7].Name, Is.EqualTo("2nd-3rd-1st"));
            Assert.That(dest[8].Name, Is.EqualTo("2nd-3rd-2nd"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Flatten_Empty
        ///
        /// <summary>
        /// 空の配列に対して Flatten を実行した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Flatten_Empty() => Assert.That(
            new Tree[0].Flatten(e => e.Children).Count(),
            Is.EqualTo(0)
        );

        #endregion

        /* ----------------------------------------------------------------- */
        ///
        /// ToObservable
        ///
        /// <summary>
        /// IList(int) を ObservableCollection(int) に変換するテストを
        /// 実行しますます。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(100)]
        public void ToObservable(int count)
        {
            var src = Enumerable.Range(0, count);
            Assert.That(src.ToObservable(), Is.EquivalentTo(src));
        }

        #endregion

        #region Others

        /* ----------------------------------------------------------------- */
        ///
        /// Tree
        ///
        /// <summary>
        /// テスト用のツリー構造を持つクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        class Tree
        {
            public string Name { get; set; }
            public IEnumerable<Tree> Children { get; set; }
        }

        #endregion
    }
}
