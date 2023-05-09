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
namespace StateChange
{
    public enum MP3PlayerStates
    {
        NoState = 0,
        SingleFileState = 1,
        PlaylistState = 2,
        MakePlaylistState = 3,
        EditPlaylistState = 4,
        RadioState = 5
    }
}
