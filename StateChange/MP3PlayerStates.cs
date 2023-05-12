/**************************************************************************
 *                                                                        *
 *  File:        MP3PlayerStates.cs                                       *
 *  Copyright:   (c) 2023, Dancău Rareș-Andrei                            *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/
// Enum-ul MP3PlayerStates defineste starile posibile ale obiectului Context
// Fiecare stare are o valoare numerica asociata, incepand cu 0 pentru NoState
// Aceste valori numerice sunt utilizate in logica de schimbare a starilor din clasa Context

namespace StateChange
{
    /// <summary>
    /// Enum-ul MP3PlayerStates defineste starile posibile ale obiectului Context
    /// Fiecare stare are o valoare numerica asociata, incepand cu 0 pentru NoState
    /// Aceste valori numerice sunt utilizate in logica de schimbare a starilor din clasa Context
    /// </summary>
    public enum MP3PlayerStates
    {
        /// <summary>
        /// Stare inexistenta, folosita inainte de a se face initializarea
        /// </summary>
        NoState = 0,
        /// <summary>
        /// Stare pentru redarea unui singur fisier audio
        /// </summary>
        SingleFileState = 1,
        /// <summary>
        /// Stare pentru redarea unei liste de fisiere audio
        /// </summary>
        PlaylistState = 2,
        /// <summary>
        /// Stare pentru crearea unei liste de fisiere audio
        /// </summary>
        MakePlaylistState = 3,
        /// <summary>
        /// Stare pentru editarea unei liste de fisiere audio
        /// </summary>
        EditPlaylistState = 4,
        /// <summary>
        /// Stare pentru redarea unui post de radio
        /// </summary>
        RadioState = 5
    }
}

