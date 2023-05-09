/**************************************************************************
 *                                                                        *
 *  File:        PlaylistState.cs                                         *
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
using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StateChange
{
    public class PlaylistState : State
    {
        public bool Handle(Context context)
        {
            if (context.StateNumber == MP3PlayerStates.PlaylistState)
            {
                context.Controls.Clear();
                context.Controls.Add(new AxWindowsMediaPlayer());
                context.Controls.Add(new ListBox());
                context.Controls.Add(new CheckBox());
                context.Controls.Add(new CheckBox());
                return true;
            }
            switch (context.StateNumber)
            {
                case MP3PlayerStates.SingleFileState:context.State = new SingleFileState(); break;
                case MP3PlayerStates.MakePlaylistState:context.State = new MakePlaylistState(); break;
                case MP3PlayerStates.EditPlaylistState: context.State = new EditPlaylistState(); break;
                case MP3PlayerStates.RadioState: context.State = new RadioState(); break;
            }
            return false;
        }
    }
}
