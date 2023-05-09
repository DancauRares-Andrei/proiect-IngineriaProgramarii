/**************************************************************************
 *                                                                        *
 *  File:        Context.cs                                               *
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
    public class Context
    {
        public List<Control> Controls { get; set; }
        public State State { get; set; }

        public MP3PlayerStates StateNumber { get; set; }
        public Context(State state)
        {
            State = state;
            Controls = new List<Control>();
            StateNumber = MP3PlayerStates.NoState;
        }

        public void Request()
        {            
            bool x= State.Handle(this);
            if(!x)
            {
                x = State.Handle(this);
            }
        }
    }
}
