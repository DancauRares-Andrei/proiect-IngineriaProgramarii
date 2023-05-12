/**************************************************************************
 *                                                                        *
 *  File:        TestSingleFileState.cs                                   *
 *  Copyright:   (c) 2023, Rareș-Andrei Dancău                            *
 *  Description: Clasa ce testeaza SingleFileState                        *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using StateChange;
using System;

namespace TestStateChange
{
    /// <summary>
    /// Clasa ce testeaza SingleFileState
    /// </summary>
    [TestClass]
    public class TestSingleFileState : ITestState
    {
        /// <summary>
        /// Contextul a carei functionalitate va fi testata
        /// </summary>
        private Context _context;
        /// <summary>
        /// Constructorul in care se creeaza contextul si se seteaza starea dorita
        /// </summary>
        public TestSingleFileState()
        {
            _context = new Context();
            _context.StateNumber = MP3PlayerStates.SingleFileState;
        }
        /// <summary>
        /// Testam daca se returneaza true la iesirea din functia Request a contextului
        /// </summary>
        [TestMethod]
        public void TestTrue()
        {
            Assert.AreEqual(true, _context.Request());
        }
        /// <summary>
        /// Testam daca starea contextului este MakePlaylistState, si nu ar trebui sa fie
        /// </summary>
        [TestMethod]
        public void TestMakePlaylist()
        {
            _context.Request();
            Assert.AreEqual(false, typeof(MakePlaylistState) == _context.State.GetType());
        }
        /// <summary>
        /// Testam daca starea contextului este PlaylistState, si nu ar trebui sa fie
        /// </summary>
        [TestMethod]
        public void TestPlaylist()
        {
            _context.Request();
            Assert.AreEqual(false, typeof(PlaylistState) == _context.State.GetType());
        }
        /// <summary>
        /// Testam daca starea contextului este RadioState, si nu ar trebui sa fie
        /// </summary>
        [TestMethod]
        public void TestRadio()
        {
            _context.Request();
            Assert.AreEqual(false, typeof(RadioState) == _context.State.GetType());
        }
        /// <summary>
        /// Testam daca starea contextului este SingleFileState, si ar trebui sa fie
        /// </summary>
        [TestMethod]
        public void TestSingleFile()
        {
            _context.Request();
            Assert.AreEqual(true, typeof(SingleFileState) == _context.State.GetType());
        }
        /// <summary>
        /// Testam daca starea contextului este EditPlaylistState, si nu ar trebui sa fie
        /// </summary>
        [TestMethod]
        public void TestEditPlaylist()
        {
            _context.Request();
            Assert.AreEqual(false, typeof(EditPlaylistState) == _context.State.GetType());
        }
    }
}
