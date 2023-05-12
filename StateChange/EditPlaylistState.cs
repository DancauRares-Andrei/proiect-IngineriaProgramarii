/**************************************************************************
 *                                                                        *
 *  File:        EditPlaylistState.cs                                     *
 *  Copyright:   (c) 2023, Rareș-Andrei Dancău                            *
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
using System.Windows.Forms;

namespace StateChange
{
    /// <summary>
    /// Clasa folosita de context in cazul in care se modifica un playlist existent
    /// </summary>
   public class EditPlaylistState : IState
    {
        /// <summary>
        /// Functie in care se schimba starea contextului, daca StateNumber nu corespunde sau se inserează controalele în context dacă există corespondența.
        /// </summary>
        /// <param name="context">Contextul asupra caruia se vor aplica operatiile</param>
        /// <returns>Returneaza true daca starea este valida sau false daca starea necesita o schimbare</returns>
        public bool Handle(Context context)
        { 
            // Se verifică dacă starea curentă este cea dorită pentru a fi afișată
            if (context.StateNumber == MP3PlayerStates.EditPlaylistState)
            {
                // Dacă da, se elimină controalele existente și se adaugă controalele pentru starea curentă
                context.Controls.Clear();
                context.Controls.Add(new Button());
                context.Controls.Add(new Button());
                context.Controls.Add(new ListBox());
                context.Controls.Add(new Button());
                return true;
            }
            // Dacă nu, se schimbă starea curentă într-o altă stare, în funcție de starea curentă
            switch (context.StateNumber)
            {
                case MP3PlayerStates.PlaylistState: context.State = new PlaylistState(); break;
                case MP3PlayerStates.SingleFileState: context.State = new SingleFileState(); break;
                case MP3PlayerStates.MakePlaylistState: context.State = new MakePlaylistState(); break;
                case MP3PlayerStates.RadioState:context.State = new RadioState(); break;
            }
                return false;
            
        }
    }
}
