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
    ///<summary>
    /// Clasa Context are rolul de a stoca starea curenta si controalele asociate starii.
    ///</summary>
    public class Context
    {
        ///<summary>
        /// Lista de controale pentru starea curenta
        ///</summary>
        public List<Control> Controls { get; set; }
        ///<summary>
        /// Starea curenta a contextului
        ///</summary>
        public IState State { get; set; }
        ///<summary>
        /// Numarul starii curente
        ///</summary>
        public MP3PlayerStates StateNumber { get; set; }
        ///<summary>
        /// Constructorul Context initializeaza starea curenta, lista de controale si numarul starii cu "NoState"
        /// </summary>
        public Context()
        {
            State = new SingleFileState();
            Controls = new List<Control>();
            StateNumber = MP3PlayerStates.NoState;
        }
        /// <summary>
        /// Metoda Request este folosita pentru a solicita incarcarea controalelor
        /// </summary>
        public void Request()
        {
            //Se incearca incarcarea controalelor cu starea curenta                
            if (!State.Handle(this))
            {
                //Daca s-a revenit pe false, inseamna ca s-a schimbat starea si trebuie facuta incarcarea controalelor
                State.Handle(this);
            }
        }
    }
}
