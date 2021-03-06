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
using Cube.DataContract;
using Microsoft.Win32;
using NUnit.Framework;
using System;
using System.IO;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// DataContractTest
    ///
    /// <summary>
    /// DataContract に関するテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class DataContractTest : RegistryFixture
    {
        #region Tests

        #region Serialize

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_File
        ///
        /// <summary>
        /// ファイルにシリアライズするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Xml,  "Person.xml")]
        [TestCase(Format.Json, "Person.json")]
        public void Serialize_File(Format format, string filename)
        {
            var dest = GetResultsWith(filename);
            format.Serialize(dest, Person.CreateDummy());
            Assert.That(new FileInfo(dest).Length, Is.AtLeast(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry
        ///
        /// <summary>
        /// レジストリにシリアライズするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry()
        {
            var name = GetKeyName(nameof(Serialize_Registry));

            Format.Registry.Serialize(name, Person.CreateDummy());
            Format.Registry.Serialize(name, default(Person)); // ignore

            using (var k = OpenSubKey(nameof(Serialize_Registry)))
            {
                var time = new DateTime(2014, 12, 31, 23, 25, 30).ToUniversalTime();

                Assert.That(k.GetValue("Name"),     Is.EqualTo("山田花子"));
                Assert.That(k.GetValue("Age"),      Is.EqualTo(15));
                Assert.That(k.GetValue("Sex"),      Is.EqualTo(1));
                Assert.That(k.GetValue("Reserved"), Is.EqualTo(1));
                Assert.That(k.GetValue("Creation"), Is.EqualTo(time.ToString("o")));
                Assert.That(k.GetValue("ID"),       Is.EqualTo(123));
                Assert.That(k.GetValue("Secret"),   Is.Null);

                using (var sk = k.OpenSubKey("Contact", false))
                {
                    Assert.That(sk.GetValue("Type"),  Is.EqualTo("Phone"));
                    Assert.That(sk.GetValue("Value"), Is.EqualTo("080-9876-5432"));
                }

                using (var sk = k.OpenSubKey("Others", false))
                {
                    using (var ssk = sk.OpenSubKey("0", false))
                    {
                        Assert.That(ssk.GetValue("Type"),  Is.EqualTo("PC"));
                        Assert.That(ssk.GetValue("Value"), Is.EqualTo("pc@example.com"));
                    }

                    using (var ssk = sk.OpenSubKey("1", false))
                    {
                        Assert.That(ssk.GetValue("Type"),  Is.EqualTo("Mobile"));
                        Assert.That(ssk.GetValue("Value"), Is.EqualTo("mobile@example.com"));
                    }
                }

                using (var sk = k.OpenSubKey("Messages", false))
                {
                    using (var ssk = sk.OpenSubKey("0", false)) Assert.That(ssk.GetValue(""), Is.EqualTo("1st message"));
                    using (var ssk = sk.OpenSubKey("1", false)) Assert.That(ssk.GetValue(""), Is.EqualTo("2nd message"));
                    using (var ssk = sk.OpenSubKey("2", false)) Assert.That(ssk.GetValue(""), Is.EqualTo("3rd message"));
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Remove
        ///
        /// <summary>
        /// リストの項目を削除して再シリアライズした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Remove()
        {
            var name = GetKeyName(nameof(Serialize_Registry_Remove));
            var src  = Person.CreateDummy();

            Format.Registry.Serialize(name, src);
            src.Others.RemoveAt(0);
            Format.Registry.Serialize(name, src);

            var dest = Format.Registry.Deserialize<Person>(name);
            Assert.That(dest.Others.Count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Add
        ///
        /// <summary>
        /// 配列の項目を増やして再シリアライズした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Add()
        {
            var name = GetKeyName(nameof(Serialize_Registry_Add));
            var src  = Person.CreateDummy();

            Format.Registry.Serialize(name, src);

            src.Messages = new[]
            {
                " 1st change",
                " 2nd change",
                " 3rd change",
                " 4th change",
                " 5th change",
                " 6th change",
                " 7th change",
                " 8th change",
                " 9th change",
                "10th change",
            };

            Format.Registry.Serialize(name, src);

            var dest = Format.Registry.Deserialize<Person>(name);
            Assert.That(dest.Messages.Length, Is.EqualTo(10));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Serialize_Registry_Null
        ///
        /// <summary>
        /// 無効なレジストリにシリアライズした時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serialize_Registry_Null() => Assert.DoesNotThrow(
            () => default(RegistryKey).Serialize(Person.CreateDummy())
        );

        /* ----------------------------------------------------------------- */
        ///
        /// Serializee_Stream_Throws
        ///
        /// <summary>
        /// Format.Registry を指定した状態でストリームにシリアライズした
        /// 時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Serializee_Stream_Throws() => Assert.That(() =>
            {
                using (var ss = File.Create(GetResultsWith("Person.reg")))
                {
                    Format.Registry.Serialize(ss, Person.CreateDummy());
                }
            },
            Throws.TypeOf<ArgumentException>()
        );

        #endregion

        #region Deserialize

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_File
        ///
        /// <summary>
        /// ファイルからデシリアライズするテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase(Format.Xml,  "Settings.xml",     ExpectedResult = "John Lennon")]
        [TestCase(Format.Json, "Settings.json",    ExpectedResult = "Mike Davis")]
        [TestCase(Format.Xml,  "Settings.ja.xml",  ExpectedResult = "鈴木一朗")]
        [TestCase(Format.Json, "Settings.ja.json", ExpectedResult = "山田太郎")]
        public string Deserialize_File(Format format, string filename) =>
            format.Deserialize<Person>(GetExamplesWith(filename)).Name;

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_Registry_Null
        ///
        /// <summary>
        /// 無効なレジストリキーを設定した時の挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Deserialize_Registry_Null()
        {
            var dest     = default(RegistryKey).Deserialize<Person>();
            var expected = new Person();

            Assert.That(dest.Identification, Is.EqualTo(expected.Identification));
            Assert.That(dest.Name,           Is.EqualTo(expected.Name));
            Assert.That(dest.Age,            Is.EqualTo(expected.Age));
            Assert.That(dest.Sex,            Is.EqualTo(expected.Sex));
            Assert.That(dest.Reserved,       Is.EqualTo(expected.Reserved));
            Assert.That(dest.Creation,       Is.EqualTo(expected.Creation));
            Assert.That(dest.Secret,         Is.EqualTo(expected.Secret));
            Assert.That(dest.Contact.Type,   Is.EqualTo(expected.Contact.Type));
            Assert.That(dest.Contact.Value,  Is.EqualTo(expected.Contact.Value));
            Assert.That(dest.Others.Count,   Is.EqualTo(0));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Deserialize_Stream_Throws
        ///
        /// <summary>
        /// Format.Registry を指定した状態でストリームから読み込んだ時の
        /// 挙動を確認します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Deserialize_Stream_Throws() => Assert.That(
            () =>
            {
                using (var ss = File.OpenRead(GetExamplesWith("Settings.xml")))
                {
                    Format.Registry.Deserialize<Person>(ss);
                }
            },
            Throws.TypeOf<ArgumentException>()
        );

        #endregion

        #endregion
    }
}
