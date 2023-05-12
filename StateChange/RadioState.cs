/**************************************************************************
 *                                                                        *
 *  File:        RadioState.cs                                            *
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
using AxWMPLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace StateChange
{
    /// <summary>
    /// Clasa folosita de context in cazul in care se asculta un post radio
    /// </summary>
   public class RadioState : IState
    {
        /// <summary>
        /// Functie in care se schimba starea contextului, daca StateNumber nu corespunde sau se inserează controalele în context dacă există corespondența.
        /// </summary>
        /// <param name="context">Contextul asupra caruia se vor aplica operatiile</param>
        /// <returns>Returneaza true daca starea este valida sau false daca starea necesita o schimbare</returns>
        public bool Handle(Context context)
        {
            // Verificăm dacă starea curentă este "RadioState"
            if (context.StateNumber == MP3PlayerStates.RadioState)
            {
                // Curățăm controlerele și adăugăm noi controale pentru redarea radio-ului și afișarea listei de stații
                context.Controls.Clear();
                context.Controls.Add(new AxWindowsMediaPlayer());
                context.Controls.Add(new ListBox());
                return true;
            }

            // Dacă starea curentă nu este "RadioState", trecem la o altă stare în funcție de numărul stării curente
            switch (context.StateNumber)
            {
                case MP3PlayerStates.SingleFileState: context.State = new SingleFileState(); break;
                case MP3PlayerStates.MakePlaylistState: context.State = new MakePlaylistState(); break;
                case MP3PlayerStates.EditPlaylistState: context.State = new EditPlaylistState(); break;
                case MP3PlayerStates.PlaylistState: context.State = new PlaylistState(); break;
            }
            return false;
        }
    }
}
