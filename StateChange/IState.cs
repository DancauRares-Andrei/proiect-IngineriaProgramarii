/**************************************************************************
 *                                                                        *
 *  File:        State.cs                                                 *
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
    /// Interfata folosita de context pentru a-și schimba comportamentul
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Functie implementata in clasele derivate
        /// <param name="context">Contextul asupra caruia se vor aplica operatiile</param>
        /// <returns>Returneaza true daca starea este valida sau false daca starea necesita o schimbare</returns>
        /// </summary>
        bool Handle(Context context);
    }
}
