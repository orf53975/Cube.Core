﻿/* ------------------------------------------------------------------------- */
///
/// RegistryTester.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.Serialization;
using NUnit.Framework;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.RegistryTester
    /// 
    /// <summary>
    /// Settings クラスを用いていレジストリへの読み込みおよび保存を
    /// テストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class RegistryTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// SettingsTester
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public RegistryTester() { }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadString
        /// 
        /// <summary>
        /// レジストリから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadString()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Name, Is.EqualTo("Harry Potter"));
                    Assert.That(data.Secret, Is.EqualTo("secret message"));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadInteger
        /// 
        /// <summary>
        /// レジストリから整数値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadInteger()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Age, Is.EqualTo(11));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadEnum
        /// 
        /// <summary>
        /// レジストリから列挙体を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadEnum()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Sex, Is.EqualTo(Sex.Man));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadAlias
        /// 
        /// <summary>
        /// レジストリから別名が設定されている値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadAlias()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Identification, Is.EqualTo(1357));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadBool
        /// 
        /// <summary>
        /// レジストリから真偽値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadBool()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Reserved, Is.True);
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadDateTime
        /// 
        /// <summary>
        /// レジストリから時刻オブジェクトを読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadDateTime()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Creation.Year, Is.EqualTo(2015));
                    Assert.That(data.Creation.Month, Is.EqualTo(3));
                    Assert.That(data.Creation.Day, Is.EqualTo(16));
                    Assert.That(data.Creation.Hour, Is.EqualTo(11));
                    Assert.That(data.Creation.Minute, Is.EqualTo(32));
                    Assert.That(data.Creation.Second, Is.EqualTo(26));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestLoadClass
        /// 
        /// <summary>
        /// レジストリからクラスオブジェクトの値を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestLoadClass()
        {
            Assert.DoesNotThrow(() => {
                using (var registrar = new Registrar())
                {
                    var data = Cube.Settings.Load<Person>(registrar.TargetKey);
                    Assert.That(data, Is.Not.Null);
                    Assert.That(data.Phone.Type, Is.EqualTo("Mobile"));
                    Assert.That(data.Phone.Data, Is.EqualTo("090-1234-5678"));
                    Assert.That(data.Email.Type, Is.EqualTo("Unknown"));
                    Assert.That(data.Email.Data, Is.EqualTo("Unknown data"));
                }
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestSave
        /// 
        /// <summary>
        /// ファイルから設定を読み込むテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestSave()
        {
            var person = new Person {
                Identification = 123,
                Name = "山田太郎",
                Age = 20,
                Creation = new DateTime(2014, 12, 31, 23, 25, 30),
                Phone = new Address { Type = "Mobile", Data = "080-9876-5432" },
                Reserved = true,
                Secret = "dummy data"
            };

            var name = @"Software\CubeSoft\SettingsTester";
            using (var root = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name))
            {
                Cube.Settings.Save<Person>(person, root);

                Assert.That(root.GetValue("ID"), Is.EqualTo(123));
                Assert.That(root.GetValue("Name"), Is.EqualTo("山田太郎"));
                Assert.That(root.GetValue("Age"), Is.EqualTo(20));
                Assert.That(root.GetValue("Creation"), Is.EqualTo(1420035930));
                Assert.That(root.GetValue("Reserved"), Is.EqualTo("True"));

                using (var subkey = root.OpenSubKey("Phone"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("Mobile"));
                    Assert.That(subkey.GetValue("Data"), Is.EqualTo("080-9876-5432"));
                }

                using (var subkey = root.OpenSubKey("Email"))
                {
                    Assert.That(subkey.GetValue("Type"), Is.EqualTo("Unknown"));
                    Assert.That(subkey.GetValue("Data"), Is.EqualTo("Unknown data"));
                }
            }
        }

        #region Internal resources

        /* ----------------------------------------------------------------- */
        ///
        /// Registrar
        /// 
        /// <summary>
        /// レジストリにテスト用のデータを登録・削除するためのクラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        internal class Registrar : IDisposable
        {
            #region Constructors

            public Registrar() { Register(); }

            #endregion

            #region Properties

            public Microsoft.Win32.RegistryKey TargetKey
            {
                get { return _target; }
            }

            #endregion

            #region IDisposable

            ~Registrar() { Dispose(false); }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool diposing)
            {
                lock (this)
                {
                    if (_disposed) return;
                    _disposed = true;
                    if (diposing) Delete();
                }
            }

            #endregion

            #region Implementations

            private void Register()
            {
                var name = @"Software\CubeSoft";
                _parent = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(name);
                _target = _parent.CreateSubKey(_root);
                _target.SetValue("ID", 1357);
                _target.SetValue("Name", "Harry Potter");
                _target.SetValue("Sex", 0);
                _target.SetValue("Age", 11);
                _target.SetValue("Creation", 0x550640ba);
                _target.SetValue("Reserved", true);

                using (var child = _target.CreateSubKey("Phone"))
                {
                    child.SetValue("Type", "Mobile");
                    child.SetValue("Data", "090-1234-5678");
                }
            }

            private void Delete()
            {
                _target.Close();
                _parent.DeleteSubKeyTree(_root);
                _parent.Close();
            }

            #endregion

            #region Fields

            private Microsoft.Win32.RegistryKey _parent = null;
            private Microsoft.Win32.RegistryKey _target = null;
            private string _root = "SettingsTester";
            private bool _disposed = false;

            #endregion
        }

        #endregion
    }
}
