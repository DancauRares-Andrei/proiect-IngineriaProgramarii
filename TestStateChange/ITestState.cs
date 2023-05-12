/**************************************************************************
 *                                                                        *
 *  File:        ITestState.cs                                            *
 *  Copyright:   (c) 2023, Rareș-Andrei Dancău                            *
 *  Description: Interfata din care deriva toate clasele de test          *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStateChange
{
    /// <summary>
    /// Interfata ce va testa fiecare clasa
    /// </summary>
    interface ITestState
    {
        /// <summary>
        /// Testam daca se returneaza true la iesirea din functia Request a contextului
        /// </summary>
        void TestTrue();
        /// <summary>
        /// Testam daca starea contextului este sau nu EditPlaylistState
        /// </summary>
        void TestEditPlaylist();
        /// <summary>
        /// Testam daca starea contextului este sau nu MakePlaylistState
        /// </summary>
        void TestMakePlaylist();
        /// <summary>
        /// Testam daca starea contextului este sau nu PlaylistState
        /// </summary>
        void TestPlaylist();
        /// <summary>
        /// Testam daca starea contextului este sau nu RadioState
        /// </summary>
        void TestRadio();
        /// <summary>
        /// Testam daca starea contextului este sau nu SingleFileState
        /// </summary>
        void TestSingleFile();
    }
}
